using AzureTranslator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureTranslator.Services
{
    public class SpellCheckService : ISpellCheckService
    {
        HttpClient httpClient;

        public SpellCheckService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Constants.SpellCheckApiKey);
        }

        public async Task<SpellCheckResult> SpellCheckTextAsync(string text)
        {
            string requestUri = GenerateRequestUri(Constants.SpellCheckEndpoint, text, SpellCheckMode.Spell);
            var response = await SendRequestAsync(requestUri);
            var spellCheckResults = JsonConvert.DeserializeObject<SpellCheckResult>(response);
            return spellCheckResults;
        }

        string GenerateRequestUri(string spellCheckEndpoint, string text, SpellCheckMode mode)
        {
            string requestUri = spellCheckEndpoint;
            requestUri += string.Format("?text={0}", WebUtility.UrlEncode(text));   // text to spell check
            requestUri += string.Format("&mode={0}", mode.ToString().ToLower());    // spellcheck mode - proof or spell
            return requestUri;
        }

        async Task<string> SendRequestAsync(string url)
        {
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
