using BlazorBattle.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorBattle.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;
        private readonly IBananaService _bananaService;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http, IBananaService bananaService)
        {
            _localStorageService = localStorageService;
            _http = http;
            _bananaService = bananaService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //retrieve auth token from local storage
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            //create empty cliams and set header to null 
            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            //check if we actually go the auth token, if so create identity and set header 
            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Replace("\"",""));
                    await _bananaService.GetBananas();
                }
                catch (Exception ex)
                {
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            //create the claims principal with the identity
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;

        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            //index 1 is payload
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }
    }
}
