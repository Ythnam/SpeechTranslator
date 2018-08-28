using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core.Model
{
    public class LanguageModel
    {
        public string Language { get; }
        public string Code { get; }
        public string Text { get; set; }
        //public TYPETODEFINE Audio { get; set}

        public LanguageModel(string _language, string _code)
        {
            this.Language = _language;
            this.Code = _code;
        }

        public LanguageModel(string _language, string _text)
        {
            this.Language = _language;
            this.Text = _text;
        }
    }
}
