using AzureTranslator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureTranslator.Services
{
	public interface ISpellCheckService
	{
		Task<SpellCheckResult> SpellCheckTextAsync(string text);
	}
}
