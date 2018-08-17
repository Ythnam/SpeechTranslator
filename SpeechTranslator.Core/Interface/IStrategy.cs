using SpeechTranslator.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core.Interface
{
    public interface IStrategy
    {
        Task<List<LanguageModel>> TranslateToText(string _fromLanguage, List<string> _toLanguages);
        void TranslateToSpeech(string _fromLanguage, List<string> _toLanguages);
    }
}
