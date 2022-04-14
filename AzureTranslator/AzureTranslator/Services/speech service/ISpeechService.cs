using AzureTranslator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureTranslator.Services
{
	public interface ISpeechService
	{
		Task<SpeechResult> RecognizeSpeechAsync(string filename);
	}
}

