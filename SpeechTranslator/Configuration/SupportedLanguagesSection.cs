using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.Configuration
{
    // Thx http://blog.danskingdom.com/adding-and-accessing-custom-sections-in-your-c-app-config/
    public class SupportedLanguagesSection : ConfigurationSection
    {
        public const string SectionName = "SupportedLanguagesSection";

        public const string Language = "Languages";

        [ConfigurationProperty(Language)]
        [ConfigurationCollection(typeof(LanguagesCollection), AddItemName = "add")]
        public LanguagesCollection Languages { get { return (LanguagesCollection)base[Language]; } }
    }

    public class LanguagesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new LanguageElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LanguageElement)element).Language;
        }
    }

    public class LanguageElement : ConfigurationElement
    {
        [ConfigurationProperty("language", IsRequired = true)]
        public string Language
        {
            get { return (string)this["language"]; }
            set { this["language"] = value; }
        }

        /// <summary>
        /// Code needed to detected input langugage
        /// Example : For french language : fr-FR
        /// </summary>
        [ConfigurationProperty("code", IsRequired = true)]
        public string Code
        {
            get { return (string)this["code"]; }
            set { this["code"] = value; }
        }
    }
}
