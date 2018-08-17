using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SpeechTranslator.Core;
using SpeechTranslator.Core.Strategy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpeechTranslator.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        enum TranslationMode { ToText=1, ToSpeech=2 };
        private StrategyContext Context;

        private string textFromSpeech;
        public string TextFromSpeech
        {
            get { return this.textFromSpeech; }
            set
            {
                if (this.textFromSpeech != value)
                {
                    this.textFromSpeech = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string textTranlatedFromSpeech;
        public string TextTranlatedFromSpeech
        {
            get { return this.textTranlatedFromSpeech; }
            set
            {
                if (this.textTranlatedFromSpeech != value)
                {
                    this.textTranlatedFromSpeech = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        public MainViewModel()
        {
            this.Context = new StrategyContext();
        }

        private ICommand onStartEventCommand;
        public ICommand OnStartEventCommand
        {
            get
            {
                return onStartEventCommand ?? (onStartEventCommand = new RelayCommand(async () => await this.OnStartEvent()));
            }
        }

        private async Task OnStartEvent()
        {
            Context.Strategy = new SpeechStrategy(ConfigurationManager.AppSettings["KEY"], ConfigurationManager.AppSettings["AzureServer"]);
            List<string> l = await Context.TranslateToText("fr-FR", new List<string>() { "en", "it", "de" });
            foreach(string s in l)
            {
                Console.WriteLine(s);
            }
        }
    }
}