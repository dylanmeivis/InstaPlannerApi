using InstaPlannerApi.Interfaces;
using InstaPlannerApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstaplannerApi.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IConfiguration _configuration;
        public AuthorizationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetOAuthUrl()
        {
            var appId = _configuration["ClientID"];
            var redirectUrl = _configuration["RedirectUrl"];
            IList<string> strings = new List<string> { "user_profile", "user_media" };
            string scope = string.Join(",", strings);

            return $"https://api.instagram.com/oauth/authorize?client_id={appId}&redirect_uri={redirectUrl}&scope={scope}&response_type=code";
        }
        public async Task<AccessTokenResult> GetAccessToken(string code)
        {
            using (var client = new HttpClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Post, _configuration["AccessTokenUrl"]);

                req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", _configuration["ClientID"]},
                    { "client_secret", _configuration["ClientSecret"]},
                    { "code", code },
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", _configuration["RedirectUrl"] }
                });
                var response = await client.SendAsync(req);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AccessTokenResult>(content);
            }
        }
    }
}