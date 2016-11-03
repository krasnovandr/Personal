
%% Step 1: Detect Candidate Text Regions Using MSER
colorImage = imread('poster.jpg');
I = rgb2gray(colorImage);


[mserRegions, mserConnComp] = detectMSERFeatures(I, ...
    'RegionAreaRange',[100 9000],'ThresholdDelta',4);

figure
imshow(I)
hold on
plot(mserRegions)
title('MSER regions')
hold off

%% Step 2: Remove Non-Text Regions Based On Basic Geometric Properties

mserStats = regionprops(mserConnComp, 'BoundingBox', 'Eccentricity', ...
    'Solidity', 'Extent', 'Euler', 'Image');

% Compute the aspect ratio using bounding box data.
bbox = vertcat(mserStats.BoundingBox);
w = bbox(:,3);
h = bbox(:,4);
aspectRatio = w./h;


filterIdx = aspectRatio' > 3; 
filterIdx = filterIdx | [mserStats.Eccentricity] > .995 ;
filterIdx = filterIdx | [mserStats.Solidity] < .3;
filterIdx = filterIdx | [mserStats.Extent] < 0.2 | [mserStats.Extent] > 0.9;
filterIdx = filterIdx | [mserStats.EulerNumber] < -4;


mserStats(filterIdx) = [];
mserRegions(filterIdx) = [];


figure
imshow(I)
hold on
plot(mserRegions)
title('After Removing Non-Text Regions Based On Geometric Properties')
hold off


regionImage = mserStats(6).Image;
regionImage = padarray(regionImage, [1 1]);


distanceImage = bwdist(~regionImage); 
skeletonImage = bwmorph(regionImage, 'thin', inf);

strokeWidthImage = distanceImage;
strokeWidthImage(~skeletonImage) = 0;


figure
subplot(1,2,1)
imagesc(regionImage)
title('Region Image')

subplot(1,2,2)
imagesc(strokeWidthImage)
title('Stroke Width Image')


strokeWidthValues = distanceImage(skeletonImage);   
strokeWidthMetric = std(strokeWidthValues)/mean(strokeWidthValues);


strokeWidthThreshold = 0.4;
strokeWidthFilterIdx = strokeWidthMetric > strokeWidthThreshold; 


for j = 1:numel(mserStats)
    
    regionImage = mserStats(j).Image;
    regionImage = padarray(regionImage, [1 1], 0);
    
    distanceImage = bwdist(~regionImage);
    skeletonImage = bwmorph(regionImage, 'thin', inf);
    
    strokeWidthValues = distanceImage(skeletonImage);
    
    strokeWidthMetric = std(strokeWidthValues)/mean(strokeWidthValues);
    
    strokeWidthFilterIdx(j) = strokeWidthMetric > strokeWidthThreshold;
    
end


mserRegions(strokeWidthFilterIdx) = [];
mserStats(strokeWidthFilterIdx) = [];


figure
imshow(I)
hold on
plot(mserRegions)
title('After Removing Non-Text Regions Based On Stroke Width Variation')
hold off

%% Step 4: Merge Text Regions For Final Detection Result

bboxes = vertcat(mserStats.BoundingBox);


xmin = bboxes(:,1);
ymin = bboxes(:,2);
xmax = xmin + bboxes(:,3) - 1;
ymax = ymin + bboxes(:,4) - 1;


expansionAmount = 0.02;
xmin = (1-expansionAmount) * xmin;
ymin = (1-expansionAmount) * ymin;
xmax = (1+expansionAmount) * xmax;
ymax = (1+expansionAmount) * ymax;


xmin = max(xmin, 1);
ymin = max(ymin, 1);
xmax = min(xmax, size(I,2));
ymax = min(ymax, size(I,1));


expandedBBoxes = [xmin ymin xmax-xmin+1 ymax-ymin+1];
IExpandedBBoxes = insertShape(colorImage,'Rectangle',expandedBBoxes,'LineWidth',3);

figure
imshow(IExpandedBBoxes)
title('Expanded Bounding Boxes Text')


overlapRatio = bboxOverlapRatio(expandedBBoxes, expandedBBoxes);


n = size(overlapRatio,1); 
overlapRatio(1:n+1:n^2) = 0;


g = graph(overlapRatio);


componentIndices = conncomp(g);


xmin = accumarray(componentIndices', xmin, [], @min);
ymin = accumarray(componentIndices', ymin, [], @min);
xmax = accumarray(componentIndices', xmax, [], @max);
ymax = accumarray(componentIndices', ymax, [], @max);


textBBoxes = [xmin ymin xmax-xmin+1 ymax-ymin+1];


numRegionsInGroup = histcounts(componentIndices);
textBBoxes(numRegionsInGroup == 1, :) = [];

% Show the final text detection result.
ITextRegion = insertShape(colorImage, 'Rectangle', textBBoxes,'LineWidth',3);

figure
imshow(ITextRegion)

title('Detected Text')

%% Step 5: Recognize Detected Text Using OCR

ocrtxt = ocr(I, textBBoxes);
recognizedText = ocrtxt.Text;
figure;
imshow(colorImage);
   text(600, 150, recognizedText, 'BackgroundColor', [1 1 1]);
% [ocrtxt.Text];



displayEndOfDemoMessage(mfilename)
