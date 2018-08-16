using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core
{
    public static class SpeechManager
    {
        private static string key = "96026169606d4ed49d1cec6bccc69dd8";
        private static string server = "northeurope";

        public static async Task<string> RecognizeSpeechAsync(string _language)
        {
            var factory = SpeechFactory.FromSubscription(key, server);

            using (var recognizer = factory.CreateSpeechRecognizer(_language))
            {
                // Performs recognition.
                // RecognizeAsync() returns when the first utterance has been recognized, so it is suitable 
                // only for single shot recognition like command or query. For long-running recognition, use
                // StartContinuousRecognitionAsync() instead.
                var result = await recognizer.RecognizeAsync();

                // Checks result.
                if (result.RecognitionStatus != RecognitionStatus.Recognized)
                {
                    if (result.RecognitionStatus == RecognitionStatus.Canceled)
                        return "There was an error, reason:" + result.RecognitionFailureReason;
                    return "No speech could be recognized.\n";
                }
                else
                    return result.Text;
            }
        }

        public static async Task<string> TranslateSpeechAsync(string _fromLanguage, List<string> _toLanguage)
        {
            // Creates an instance of a speech factory with specified
            // subscription key and service region. Replace with your own subscription key
            // and service region (e.g., "westus").
            var factory = SpeechFactory.FromSubscription(key, server);

            using (TranslationRecognizer trecognizer = factory.CreateTranslationRecognizer(_fromLanguage, _toLanguage))
            {
                var result = await trecognizer.RecognizeAsync();

                if (result.RecognitionStatus != RecognitionStatus.Recognized)
                {
                    if (result.RecognitionStatus == RecognitionStatus.Canceled)
                        return "There was an error, reason:" + result.RecognitionFailureReason;
                    return "No speech could be recognized.\n";
                }
                else
                {
                    if (result.TranslationStatus == TranslationStatus.Success)
                    {
                        string tresult = "";
                        foreach (var element in result.Translations)
                            tresult += element.Value;
                        return tresult;
                    }
                    return "Translation error";
                }
            }
        }
    }
}
