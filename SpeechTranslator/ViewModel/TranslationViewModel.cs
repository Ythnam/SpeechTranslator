using GalaSoft.MvvmLight;
using SpeechTranslator.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTranslator.ViewModel
{
    public class TranslationViewModel : ViewModelBase
    {
        private List<LanguageModel> translations;
        public List<LanguageModel> Translations
        {
            get { return this.translations; }
            set
            {
                if (this.translations != value)
                {
                    this.translations = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TranslationViewModel()
        {
            this.Translations = new List<LanguageModel>();
            MessengerInstance.Register<List<LanguageModel>>(this, "Translations", (action) => ReceiveTranslations(action));
        }

        private void ReceiveTranslations(List<LanguageModel> action)
        {
            if (this.Translations.Any())
                this.Translations.Clear();
            
            Translations = action;
        }
    }
}
