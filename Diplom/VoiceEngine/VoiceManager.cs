using System.Windows;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Diplom.VoiceEngine;

public static class VoiceManager
{

    static string speechKey = "4fLF1vtqJVOVCTLd60BdoAFVu57dbhq6iitO5HuEeJIadElMUnl3JQQJ99BCACYeBjFXJ3w3AAAYACOG1MxA";
    static string speechRegion = "eastus";

    static void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
    {
        switch (speechRecognitionResult.Reason)
        {
            case ResultReason.RecognizedSpeech:
                MessageBox.Show($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                break;
            case ResultReason.NoMatch:
                MessageBox.Show($"NOMATCH: Speech could not be recognized.");
                break;
            case ResultReason.Canceled:
                var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                MessageBox.Show($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    MessageBox.Show($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    MessageBox.Show($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    MessageBox.Show($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
        }
    }

    static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
    {
        switch (speechSynthesisResult.Reason)
        {
            case ResultReason.SynthesizingAudioCompleted:
                MessageBox.Show($"Speech synthesized for text: [{text}]");
                break;
            case ResultReason.Canceled:
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                MessageBox.Show($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    MessageBox.Show($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    MessageBox.Show($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                    MessageBox.Show($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
            default:
                break;
        }
    }

    public async static Task TextToSpeechAsync(string text)
    {

        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

        // The neural multilingual voice can speak different languages based on the input text.
        speechConfig.SpeechSynthesisVoiceName = "ru-RU-DmitryNeural";

        /*
         * голоса которые есть
         * 
         * ru-RU-SvetlanaNeural (женский)
         * ru-RU-DmitryNeural (мужской) (норм)
         * ru-RU-DariyaNeural (женский)
         * 
         */

        using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
        {
            var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
            OutputSpeechSynthesisResult(speechSynthesisResult, text);
        }

    }

    public async static Task SpeechToTextAsync()
    {

        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
        speechConfig.SpeechRecognitionLanguage = "ru-RU";

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        MessageBox.Show("Speak into your microphone.");
        var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
        OutputSpeechRecognitionResult(speechRecognitionResult);

    }

}
