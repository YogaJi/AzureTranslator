using AzureTranslator.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace AzureTranslator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TranslatorPage : ContentPage, INotifyPropertyChanged
    {
        ISpeechService SpeechService;
        ISpellCheckService spellCheckService;
        ITextTranslationService textTranslationService;
        private string afterStringProperty;
        private string transStringProperty;
        public string trans;
        public string stringToCheck;
        public string spellCheckResult;
        private string fromStringKey;
        private string toStringKey;
        private string fromString;
        private string toString;
        bool isRecording = false;
        Dictionary<string, string> languageDic = new Dictionary<string, string>();
       
        //public Dictionary<string,string> languageDic;
        public string AfterStringProperty
        {
            get { return afterStringProperty; }
            set
            {
                afterStringProperty = value;
                OnPropertyChanged(nameof(AfterStringProperty)); // Notify that there was a change on this property
            }
        }
        public string TransStringProperty
        {
            get { return transStringProperty; }
            set
            {
                transStringProperty = value;
                OnPropertyChanged(nameof(TransStringProperty)); // Notify that there was a change on this property
            }
        }

        public static readonly BindableProperty IsProcessingProperty =
        BindableProperty.Create("IsProcessing", typeof(bool), typeof(TranslatorPage), false);
        public bool IsProcessing
        {
            get { return (bool)GetValue(IsProcessingProperty); }
            set { SetValue(IsProcessingProperty, value); }
        }

        public TranslatorPage()
        {
            InitializeComponent();
            BindingContext = this;
            textTranslationService = new TextTranslationService(new AuthenticationService(Constants.TextTranslatorApiKey));
            spellCheckService = new SpellCheckService();
            //SpeechService = new SpeechService(new AuthenticationService(Constants.SpeechApiKey), Device.RuntimePlatform);
            languageDic.Add("English", "en");
            languageDic.Add("Chinese", "zh-Hans");
            languageDic.Add("French", "fr-ca");
            languageDic.Add("German", "de");
            languageDic.Add("Armenian", "hy");     
            languageDic.Add("Spanish", "es");
            languageDic.Add("Italian", "it");
            languageDic.Add("Japanese", "ja"); 
            languageDic.Add("Korean", "ko");    
            languageDic.Add("Polish", "pl");
            languageDic.Add("Hindi", "hi");
            languageDic.Add("Maori", "mi");
        }


        private async void OnSelectedIndexChangedFrom(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedItem = (string)picker.SelectedItem;
            fromStringKey = (string)selectedItem;
            Console.WriteLine(fromStringKey);

            foreach (var item in languageDic)
            {
                if (item.Key == fromStringKey) {
                Console.WriteLine("Key " + item.Key +" Value " +item.Value );
                    fromString = item.Value;
                }
            }
        }
        private async void OnSelectedIndexChangedTo(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedItem = (string)picker.SelectedItem;
            toStringKey = (string)selectedItem;
            Console.WriteLine(toStringKey);

            foreach (var item in languageDic)
            {
                if (item.Key == toStringKey)
                {
                    Console.WriteLine("Key " + item.Key + " Value " + item.Value);
                    toString = item.Value;
                }
            }

        }

        async void ClickToCopyAsync(object sender, EventArgs e)
        {
            var hasText = Clipboard.HasText;
            await Clipboard.SetTextAsync(transStringProperty); 
             var text = await Clipboard.GetTextAsync();
            Console.WriteLine(text);
            await DisplayAlert("Copied!", "Your text has been successfully copied.", "OK");
        }

        async void ClickToCopyTransAsync(object sender, EventArgs e)
        {
            var hasText = Clipboard.HasText;
            await Clipboard.SetTextAsync(afterStringProperty);
            var textAfter = await Clipboard.GetTextAsync();
            Console.WriteLine(textAfter);
            await DisplayAlert("Copied!", "Your translation has been successfully copied.", "OK");
        }
        
        async void OnTranslateButtonClicked(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(transStringProperty) && toString != null && fromString != null)
                {
                    //IsProcessing = true;
                    Console.WriteLine("not null");
                    AfterStringProperty = await textTranslationService.TranslateTextAsync(transStringProperty, fromString, toString);
                    trans = await textTranslationService.TranslateTextAsync(transStringProperty, fromString, toString);
                    AfterStringProperty = trans;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async void ClickToSpeakAsync(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AfterStringProperty))
            {
                await TextToSpeech.SpeakAsync(AfterStringProperty);
            }
        }


        async void OnSpellCheckButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(transStringProperty))
                {
                    IsProcessing = true;
                    //stringToCheck = transStringProperty.ToString();
                    var spellCheckResult = await spellCheckService.SpellCheckTextAsync(transStringProperty);
                    foreach (var flaggedToken in spellCheckResult.FlaggedTokens)
                    {
                        transStringProperty = transStringProperty.Replace(flaggedToken.Token, flaggedToken.Suggestions.FirstOrDefault().Suggestion);
                    }
                    OnPropertyChanged("transStringProperty");

                    IsProcessing = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*        async void OnRecognizeSpeechButtonClicked(object sender, EventArgs e)
                {
                    try
                    {
                        var audioRecordingService = DependencyService.Get<IAudioRecorderService>();
                        if (!isRecording)
                        {
                            audioRecordingService.StartRecording();

                            ((Button)sender).ImageSource = "recording.png";
                            IsProcessing = true;
                        }
                        else
                        {
                            audioRecordingService.StopRecording();
                        }

                        isRecording = !isRecording;
                        Console.WriteLine(isRecording);
                        if (!isRecording)
                        {
                            var speechResult = await SpeechService.RecognizeSpeechAsync(Constants.AudioFilename);
                            Debug.WriteLine("Name111: " + speechResult.DisplayText);
                            Debug.WriteLine("Recognition Status 111: " + speechResult.RecognitionStatus);

                            if (!string.IsNullOrWhiteSpace(speechResult.DisplayText))
                            {
                                transwords = char.ToUpper(speechResult.DisplayText[0]) + speechResult.DisplayText.Substring(1);
                                Console.WriteLine(transwords);
                                //OnPropertyChanged("TransStringProperty");
                                TransStringProperty = transwords;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message +"error! J");
                    }
                    finally
                    {
                        if (!isRecording)
                        {
                            ((Button)sender).ImageSource = "record.png";
                            IsProcessing = false;

                        }
                    }
                }*/

    }
}
