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
        Task TranslateToText(LanguageModel _fromLanguage, List<LanguageModel> _toLanguages);
        void TranslateToSpeech(LanguageModel _fromLanguage, List<LanguageModel> _toLanguages);
    }
}
