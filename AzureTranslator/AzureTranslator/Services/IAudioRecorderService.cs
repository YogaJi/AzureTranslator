using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTranslator.Services
{
    public interface IAudioRecorderService
    {
        void StartRecording();
        void StopRecording();
    }
}
