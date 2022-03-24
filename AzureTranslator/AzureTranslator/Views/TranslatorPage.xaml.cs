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

        ITextTranslationService textTranslationService;
        private string myStringProperty;
        public string trans;
        public string MyStringProperty
        {
            get { return myStringProperty; }
            set
            {
                myStringProperty = value;
                OnPropertyChanged(nameof(MyStringProperty)); // Notify that there was a change on this property
            }
        }

        /*        public static readonly BindableProperty TodoItemProperty =
                    BindableProperty.Create("TodoItem", typeof(TodoItem), typeof(TranslatorPage), null);

                public TodoItem TodoItem
                {
                    get { return (TodoItem)GetValue(TodoItemProperty); }
                    set { SetValue(TodoItemProperty, value); }
                }

                public static readonly BindableProperty IsProcessingProperty =
                    BindableProperty.Create("IsProcessing", typeof(bool), typeof(TranslatorPage), false);
        
        public bool IsProcessing
        {
            get { return (bool)GetValue(IsProcessingProperty); }
            set { SetValue(IsProcessingProperty, value); }
        }
        */
        public TranslatorPage()
        {
            InitializeComponent();
            BindingContext = this;
            textTranslationService = new TextTranslationService(new AuthenticationService(Constants.TextTranslatorApiKey));
            
        }

        async void OnTranslateButtonClicked(object sender, EventArgs e)
        {
            var translateText = TranslateText.Text;
            //var translatedText = TranslatedText.Text;
            try
            {
                if (!string.IsNullOrWhiteSpace(translateText))
                {
                    //IsProcessing = true;
                    Console.WriteLine("not null");
                    MyStringProperty = await textTranslationService.TranslateTextAsync(translateText);
                    trans= await textTranslationService.TranslateTextAsync(translateText);

                    //Console.WriteLine(t);
                    MyStringProperty = trans;
                    //OnPropertyChanged("TodoItem");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
