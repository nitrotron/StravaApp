﻿#region Copyright (C) 2014 Sascha Simon

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
using com.strava.v3.api.Common;

namespace com.strava.v3.api.Authentication
{
    /// <summary>
    /// This class contains information about an auth token received from Strava.
    /// </summary>
    public class AuthCodeReceivedEventArgs
    {
        /// <summary>
        /// the auth token received from Strava.
        /// </summary>
        public String AuthCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the AuthCodeReceivedEventArgs class.
        /// </summary>
        /// <param name="code">The auth code received from Strava.</param>
        public AuthCodeReceivedEventArgs(string code)
        {
            AuthCode = code;
            Global.Code = code;
        }
    }
}
