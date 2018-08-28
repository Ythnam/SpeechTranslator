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

        public async Task<List<LanguageModel>> TranslateToText(LanguageModel _fromLanguage, List<LanguageModel> _toLanguages)
        {
            // Creates an instance of a speech factory with specified
            // subscription key and service region. Replace with your own subscription key
            // and service region (e.g., "westus").
            var factory = SpeechFactory.FromSubscription(this.key, this.azureServer);
            List<LanguageModel> languages = new List<LanguageModel>();

            using (TranslationRecognizer trecognizer = factory.CreateTranslationRecognizer(_fromLanguage.Code, _toLanguages.Select(L => L.Code).ToList()))
            {
                var result = await trecognizer.RecognizeAsync();

                if (result.RecognitionStatus != RecognitionStatus.Recognized)
                {
                    if (result.RecognitionStatus == RecognitionStatus.Canceled)
                        languages.Add(new LanguageModel("Error", "Err") { Text = "There was an error, reason:" + result.RecognitionFailureReason });
                    else
                        languages.Add(new LanguageModel("Error", "Err") { Text = "No speech could be recognized.\n" });
                }
                else
                {
                    if (result.TranslationStatus == TranslationStatus.Success)
                    {
                        languages = _toLanguages.Zip(result.Translations, (l, t) => new LanguageModel(l.Language, t.Key) { Text = t.Value }).ToList();
                        foreach (LanguageModel l in languages)
                            Console.WriteLine(l.Language + " - " + l.Code + " " + l.Text);
                        //foreach (var element in result.Translations)
                        //{
                        //    languages.Add(new LanguageModel(_toLanguages.Select(l => l.Language).Where(l => l.), element.Key) { Text = element.Value });
                        //    Console.WriteLine(element.Key);
                        //}
                    }
                }
            }
            return languages;
        }

        public void TranslateToSpeech(LanguageModel _fromLanguage, List<LanguageModel> _toLanguage) { }
    }
}
