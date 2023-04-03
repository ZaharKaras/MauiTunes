using AndroidX.Browser.Trusted;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTunes.Services
{
    public interface ISpotifyAccountService
    {
        Task<AuthorizationToken> GetToken(string clientId, string clientSecret);
    }
}
