using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SpeechTranslator.Core;
using SpeechTranslator.Core.Model;
using SpeechTranslator.Core.Strategy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpeechTranslator.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        enum TranslationMode { ToText=1, ToSpeech=2 };
        private readonly StrategyContext Context;
        private readonly ObservableCollection<LanguageModel> Languages;

        public ObservableCollection<LanguageModel> InputLanguages { get; }
        public ObservableCollection<LanguageModel> OutputLanguages { get; }

        private LanguageModel inputLanguge;
        public LanguageModel InputLanguage
        {
            get { return this.inputLanguge; }
            set
            {
                if (this.inputLanguge != value)
                {
                    this.inputLanguge = value;
                    RaisePropertyChanged();
                }
            }
        }

        private LanguageModel outputLanguge;
        public LanguageModel OutputLanguage
        {
            get { return this.outputLanguge; }
            set
            {
                if (this.outputLanguge != value)
                {
                    this.outputLanguge = value;
                    RaisePropertyChanged();
                }
            }
        }

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
            this.Languages = new ObservableCollection<LanguageModel>();
            var supportedLanguages = ConfigurationManager.GetSection(Configuration.SupportedLanguagesSection.SectionName) as Configuration.SupportedLanguagesSection;
            if (supportedLanguages != null)
            {
                foreach (Configuration.LanguageElement langElement in supportedLanguages.Languages)
                    this.Languages.Add(new LanguageModel(langElement.Language, langElement.Code));
            }

            this.InputLanguages = new ObservableCollection<LanguageModel>(this.Languages);
            this.OutputLanguages = new ObservableCollection<LanguageModel>(this.Languages);
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

            List<LanguageModel> outputLanguageSelected = new List<LanguageModel>();
            outputLanguageSelected.Add(this.OutputLanguage);
            

            List<LanguageModel> translations = await Context.TranslateToText(this.InputLanguage, outputLanguageSelected);

            //List<LanguageModel> translations = await Context.TranslateToText("fr-FR", new List<string>() { "en-GB", "it-IT", "de-DE" });

            MessengerInstance.Send<List<LanguageModel>>(translations, "Translations");
        }
    }
}