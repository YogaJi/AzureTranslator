using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureTranslator.Services
{
    public interface IAuthenticationService
    {
        Task InitializeAsync();
        string GetAccessToken();
    }
}
