using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core.Interface
{
    interface IStrategy
    {
        Task<string> Recognize(string _language);
        Task<string> TranslateToText(string _fromLanguage, List<string> _toLanguage);
        void TranslateToSpeech(string _fromLanguage, List<string> _toLanguage);
    }
}
