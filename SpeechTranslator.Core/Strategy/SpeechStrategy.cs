using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;
using SpeechTranslator.Core.Interface;
using SpeechTranslator.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core.Strategy
{
    public class SpeechStrategy : IStrategy
    {
        private readonly string key;
        private readonly string azureServer;

        private SpeechStrategy() { }

        public SpeechStrategy(string _key, string _azureServer)
        {
            this.key = _key;
            this.azureServer = _azureServer;
        }

        public async Task<List<LanguageModel>> TranslateToText(string _fromLanguage, List<string> _toLanguages)
        {
            // Creates an instance of a speech factory with specified
            // subscription key and service region. Replace with your own subscription key
            // and service region (e.g., "westus").
            var factory = SpeechFactory.FromSubscription(this.key, this.azureServer);
            List<LanguageModel> textAndTranslations = new List<LanguageModel>();

            using (TranslationRecognizer trecognizer = factory.CreateTranslationRecognizer(_fromLanguage, _toLanguages))
            {
                var result = await trecognizer.RecognizeAsync();

                if (result.RecognitionStatus != RecognitionStatus.Recognized)
                {
                    if (result.RecognitionStatus == RecognitionStatus.Canceled)
                        textAndTranslations.Add(new LanguageModel("Error", "There was an error, reason:" + result.RecognitionFailureReason));
                    else
                        textAndTranslations.Add(new LanguageModel("Error", "No speech could be recognized.\n"));
                }
                else
                {
                    if (result.TranslationStatus == TranslationStatus.Success)
                    {
                        textAndTranslations.Add(new LanguageModel(_fromLanguage, result.Text));
                        foreach (var element in result.Translations)
                        {
                            textAndTranslations.Add(new LanguageModel(element.Key, element.Value));
                            Console.WriteLine(element.Key);
                        }
                    }
                }
            }
            return textAndTranslations;
        }

        public void TranslateToSpeech(string _fromLanguage, List<string> _toLanguage) { }
    }
}
