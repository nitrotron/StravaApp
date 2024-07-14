#region Copyright (C) 2014 Sascha Simon

//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see http://www.gnu.org/licenses/.
//
//  Visit the official homepage at http://www.sascha-simon.com

#endregion

using System;
using Newtonsoft.Json;

namespace com.strava.v3.api.Authentication
{
    /// <summary>
    /// This class holds an access token.
    /// </summary>
    public class AccessToken
    {
        private String token, expiresat, expiresin, refreshtoken;

        /// <summary>
        /// The access token.
        /// </summary>
        [JsonProperty("access_token")]
        public String Token
        {
            get { return token; }
            set
            {
                token = value;
                Common.Global.Token = value;
            }
        }

        [JsonProperty("expires_at")]
        public String ExpiresAt
        {
            get { return expiresat; }
            set
            {
                expiresat = value;
                Common.Global.ExpiresAt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(long.Parse(value));
            }
        }

        [JsonProperty("expires_in")]
        public String ExpiresIn
        {
            get { return expiresin; }
            set
            {
                expiresin = value;
                Common.Global.ExpiresIn = value;
            }
        }

        [JsonProperty("refresh_token")]
        public String RefreshToken
        {
            get { return refreshtoken; }
            set
            {
                refreshtoken = value;
                Common.Global.RefreshToken = value;
            }
        }
    }
}
