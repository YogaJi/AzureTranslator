using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTranslator
{
    class Constants
    {
        public static readonly string AuthenticationTokenEndpoint = "https://canadacentral.api.cognitive.microsoft.com/sts/v1.0";
        
        //public static readonly string AuthenticationTokenEndpoint = "https://api.cognitive.microsoft.com/sts/v1.0";
 
        public static readonly string SpeechApiKey = "9ba1e1c34bf04e6aaaf9dfb31f316752";
        public static readonly string SpeechRecognitionEndpoint = "https://speech.platform.bing.com/speech/recognition/";
        
        //public static readonly string SpeechRecognitionEndpoint = "https://canadacentral.stt.speech.microsoft.com/speech/recognition/conversation/";
        public static readonly string AudioContentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";
        
        public static readonly string SpellCheckApiKey = "<INSERT_API_KEY_HERE>";
        public static readonly string SpellCheckEndpoint = "https://api.cognitive.microsoft.com/bing/v7.0/SpellCheck";
        
        public static readonly string TextTranslatorApiKey = "dd2967c461cf43548f3da6f0739ce3d5";
        public static readonly string TextTranslatorEndpoint = "https://api.microsofttranslator.com/v2/http.svc/translate";

        /*      public static readonly string FaceApiKey = "<INSERT_API_KEY_HERE>";
                public static readonly string FaceEndpoint = "https://INSERT_REGION_HERE.api.cognitive.microsoft.com/face/v1.0";
        */
        public static readonly string AudioFilename = "Todo.wav";
    }
}
