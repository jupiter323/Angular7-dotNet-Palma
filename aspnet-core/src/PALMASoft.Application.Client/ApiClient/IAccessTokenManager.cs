using System;
using System.Threading.Tasks;
using PALMASoft.ApiClient.Models;

namespace PALMASoft.ApiClient
{
    public interface IAccessTokenManager
    {
        string GetAccessToken();

        Task<AbpAuthenticateResultModel> LoginAsync();

        Task<string> RefreshTokenAsync();

        void Logout();

        bool IsUserLoggedIn { get; }

        bool IsRefreshTokenExpired { get; }

        AbpAuthenticateResultModel AuthenticateResult { get; set; }

        DateTime AccessTokenRetrieveTime { get; set; }
    }
}