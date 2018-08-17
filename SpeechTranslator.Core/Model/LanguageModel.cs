using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core.Model
{
    public class LanguageModel
    {
        public string Language { get; set; }
        public string Text { get; set; }

        public LanguageModel(string _language, string _text)
        {
            this.Language = _language;
            this.Text = _text;
        }
    }
}
