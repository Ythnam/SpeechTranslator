using SpeechTranslator.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core
{
    public class StrategyContext
    {
        private IStrategy strategy;
        public IStrategy Strategy { set { strategy = value; } }

        public async Task<List<string>> TranslateToText(string _fromLanguage, List<string> _toLanguages)
        {
            return await strategy.TranslateToText(_fromLanguage, _toLanguages);
        }
    }
}
