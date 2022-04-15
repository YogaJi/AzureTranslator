using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureTranslator.Services
{
	public interface ITextTranslationService
	{
		Task<string> TranslateTextAsync(string text, string from, string to);
	}
}
