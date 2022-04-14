using AzureTranslator.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        bool isRecording = false;
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
        }
        public void ClickToCopy(object sender, EventArgs e)
        {

        }
        async void OnTranslateButtonClicked(object sender, EventArgs e)
        {
            //var translateText = TranslateText.Text;
            //var translatedText = TranslatedText.Text;
            try
            {
                if (!string.IsNullOrWhiteSpace(transStringProperty))
                {
                    //IsProcessing = true;
                    Console.WriteLine("not null");
                    AfterStringProperty = await textTranslationService.TranslateTextAsync(transStringProperty);
                    trans = await textTranslationService.TranslateTextAsync(transStringProperty);
                    AfterStringProperty = trans;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
