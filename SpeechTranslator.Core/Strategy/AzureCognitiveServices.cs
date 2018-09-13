using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Core.Strategy
{
    public class Strategy
    {
        protected readonly string key;
        protected readonly string azureServer;

        protected Strategy(string _key, string _azureServer)
        {
            this.key = _key;
            this.azureServer = _azureServer;
        }
    }
}
