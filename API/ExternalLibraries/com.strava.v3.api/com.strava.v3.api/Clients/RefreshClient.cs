
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.strava.v3.api.Activities;
using com.strava.v3.api.Api;
using com.strava.v3.api.Athletes;
using com.strava.v3.api.Authentication;
using com.strava.v3.api.Clubs;
using com.strava.v3.api.Common;
using com.strava.v3.api.Http;
using Microsoft.AspNetCore.Mvc;

namespace com.strava.v3.api.Clients
{
    /// <summary>
    /// Used to receive information about clubs from Strava.
    /// </summary>
    public class RefreshClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the RefreshClient class.
        /// </summary>
        /// <param name="auth">IAuthentication object that contains a valid access token.</param>
        public RefreshClient(IAuthentication auth) : base(auth) { }

        #region Async

        /// <summary>
        /// Gets the club which the specified id.
        /// </summary>
        /// <param name="clubId">The id of the club.</param>
        /// <returns>The Club object containing detailed information about the club.</returns>
        public async Task<AccessToken> RefreshAccessTokenAsync(string refreshToken, int clientID, string ClientSecret)
        {


            String postUrl = String.Format("{0}?client_id={1}&client_secret={2}&grant_type=refresh_token&refresh_token={3}",
                         Endpoints.Auth, clientID, ClientSecret, refreshToken);
            String json = await WebRequest.SendPostAsync(new Uri(postUrl));

            return Unmarshaller<AccessToken>.Unmarshal(json);
        }


        #endregion

        #region Sync

        /// <summary>
        /// Gets the club which the specified id.
        /// </summary>
        /// <param name="clubId">The id of the club.</param>
        /// <returns>The Club object containing detailed information about the club.</returns>
        public AccessToken RefreshAccessToken(string refreshToken, int clientID, string ClientSecret)
        {
            String getUrl = String.Format("{0}/?client_id={1}&client_secret={2}&grant_type=refresh_token&refresh_token={3}",
                         Endpoints.Auth, clientID, ClientSecret, refreshToken);
            String json = WebRequest.SendPost(new Uri(getUrl));

            return Unmarshaller<AccessToken>.Unmarshal(json);
        }


        #endregion
    }
}
