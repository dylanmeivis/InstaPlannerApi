using System.Threading.Tasks;
using InstaPlannerApi.Models;

namespace InstaPlannerApi.Interfaces
{
    public interface IAuthorizationManager
    {
        string GetOAuthUrl();
        Task<AccessTokenResult> GetAccessToken(string code);
    }
}