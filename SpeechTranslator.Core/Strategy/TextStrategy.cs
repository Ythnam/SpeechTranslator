using SpeechTranslator.Core.Interface;
using SpeechTranslator.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core.Strategy
{
    public class TextStrategy : Strategy, IStrategy
    {

        public TextStrategy(string _key, string _azureServer) : base(_key, _azureServer) { }

        public void TranslateToSpeech(LanguageModel _fromLanguage, List<LanguageModel> _toLanguages)
        {
            throw new NotImplementedException();
        }

        public Task<List<LanguageModel>> TranslateToText(LanguageModel _fromLanguage, List<LanguageModel> _toLanguages)
        {
            throw new NotImplementedException();
        }
    }
}
