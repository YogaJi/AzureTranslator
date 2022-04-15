using Android.Content;
using AzureTranslator.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AzureTranslator;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace AzureTranslator.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
            }
        }
    }
}