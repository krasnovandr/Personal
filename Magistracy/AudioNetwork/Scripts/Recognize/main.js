

//function RecognizeStart() {
//    var leftchannel = [];
//    var rightchannel = [];
//    var recorder = null;
//    var recording = false;
//    var recordingLength = 0;
//    var volume = null;
//    var audioInput = null;
//    var sampleRate = null;
//    var audioContext = null;

//    var context = null;
//    var outputElement = document.getElementById('output');
//    var outputString;
//    var rafID = null;
//    var analyserContext = null;
//    var canvasWidth, canvasHeight;
//    var analyserNode;
//    var bufferSize = 1024;

//    var recorderStarted;
//    // feature detection 
//    if (!navigator.getUserMedia)
//        navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia ||
//            navigator.mozGetUserMedia || navigator.msGetUserMedia;
//    if (!window.requestAnimationFrame)
//        window.requestAnimationFrame = navigator.webkitRequestAnimationFrame || navigator.mozRequestAnimationFrame;

//    if (navigator.getUserMedia) {
//        navigator.getUserMedia({ audio: true }, success, function (e) {
//            alert('Error capturing audio.');
//        });
//    } else alert('getUserMedia not supported in this browser.');


//    // when key is down


   
//}