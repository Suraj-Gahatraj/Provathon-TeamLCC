
    // status fields and start button in UI
    var phraseDiv;
    var startRecognizeOnceAsyncButton;

    // subscription key and region for speech services.
    var subscriptionKey, serviceRegion;
    var SpeechSDK;
var recognizer;
var previousAns;

    document.addEventListener("DOMContentLoaded", function () {
        startRecognizeOnceAsyncButton = document.getElementById("startRecognizeOnceAsyncButton");
        subscriptionKey = document.getElementById("subscriptionKey");
        serviceRegion = document.getElementById("serviceRegion");
        phraseDiv = document.getElementById("phraseDiv");
        previousAns = phraseDiv.value;

      startRecognizeOnceAsyncButton.addEventListener("click", function () {
          startRecognizeOnceAsyncButton.disabled = true;
          phraseDiv.innerHTML = previousAns;

        if (subscriptionKey.value === "" || subscriptionKey.value === "subscription") {
        alert("Please enter your Microsoft Cognitive Services Speech subscription key!");
          return;
        }
        var speechConfig = SpeechSDK.SpeechConfig.fromSubscription(subscriptionKey.value, serviceRegion.value);

        speechConfig.speechRecognitionLanguage = "en-US";
        var audioConfig  = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
        recognizer = new SpeechSDK.SpeechRecognizer(speechConfig, audioConfig);

        recognizer.recognizeOnceAsync(
          function (result) {
                startRecognizeOnceAsyncButton.disabled = false;
                previousAns = phraseDiv.innerHTML + result.text;
                phraseDiv.innerHTML = previousAns;
            window.console.log(result);

            recognizer.close();
            recognizer = undefined;
          },
          function (err) {
        startRecognizeOnceAsyncButton.disabled = false;
            phraseDiv.innerHTML += err;
            window.console.log(err);

            recognizer.close();
            recognizer = undefined;
          });
      });

      if (!!window.SpeechSDK) {
        SpeechSDK = window.SpeechSDK;
        startRecognizeOnceAsyncButton.disabled = false;

        document.getElementById('myDIV').style.display = 'block';
        document.getElementById('warning').style.display = 'none';
      }
    });
 