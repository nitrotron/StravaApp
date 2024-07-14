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

namespace com.strava.v3.api.Authentication
{
    /// <summary>
    /// Used to specify what data from Strava can be received by your application.
    /// </summary>
    public enum Scope
    {
        /// <summary>
        /// Only public data can be received but is READ ONLY
        /// </summary>
        Public,
        /// <summary>
        /// Public and Private data can be received, but is READ ONLY
        /// </summary>
        ViewPrivate,
        /// <summary>
        /// Data can be written. This scope is needed if you want to upload activities, but does not allow write access to the user profile
        /// </summary>
        Write,
        /// <summary>
        /// Private and public data can be received and write permissions are granted for both profile and activity data
        /// </summary>
        Full
    }
}