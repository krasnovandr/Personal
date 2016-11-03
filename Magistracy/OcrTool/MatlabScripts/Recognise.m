function detectedText = Recognise(imagePath,language)
I = imread(imagePath);
ocrResults = ocr(I,'Language',language);

detectedText = ocrResults.Text;
end





