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

namespace com.strava.v3.api.Api
{
    /// <summary>
    /// This static class contains the Strava API endpoint Urls.
    /// </summary>
    public static class Endpoints
    {
        /// <summary>
        /// Url to the Authentication endpoint used to refresh a token
        /// </summary>
        public const String Auth = "https://www.strava.com/api/v3/oauth/token";
        /// <summary>
        /// Url to the Activity endpoint used for the currently authenticated athlete.
        /// </summary>
        public const String Activity = "https://www.strava.com/api/v3/activities";
        /// <summary>
        /// Url to the Activity endpoint used for other athletes than the currently authenticated one.
        /// </summary>
        public const String Activities = "https://www.strava.com/api/v3/athlete/activities";
        /// <summary>
        /// Url to the Followers endpoint.
        /// </summary>
        public const String ActivitiesFollowers = "https://www.strava.com/api/v3/activities/following";
        /// <summary>
        /// Url to the Athlete endpoint used for the currently authenticated athlete.
        /// </summary>
        public const String Athlete = "https://www.strava.com/api/v3/athlete";
        /// <summary>
        /// Url to the Athlete endpoint used for other athletes than the currently authenticated one.
        /// </summary>
        public const String Athletes = "https://www.strava.com/api/v3/athletes";
        /// <summary>
        /// Url to the Club endpoint used for other athletes than the currently authenticated one.
        /// </summary>
        public const String Club = "https://www.strava.com/api/v3/clubs";
        /// <summary>
        /// Url to the Club endpoint used for the currently authenticated athlete.
        /// </summary>
        public const String Clubs = "https://www.strava.com/api/v3/athlete/clubs";
        /// <summary>
        /// Url to the endpoint used for receiving the friends of the currentlx authenticated user.
        /// </summary>
        public const String Friends = "https://www.strava.com/api/v3/athlete/friends";
        /// <summary>
        /// Url to the endpoint used for receiving the followers of the currently authenticated athlete.
        /// </summary>
        public const String Follower = "https://www.strava.com/api/v3/athlete/followers";
        /// <summary>
        /// Url to the endpoint used for receiving the followers of athletes other than the currently authenticated one.
        /// </summary>
        public const String Followers = "https://www.strava.com/api/v3/athletes";
        /// <summary>
        /// Url to the endpoint used for receiving gear.
        /// </summary>
        public const String Gear = "https://www.strava.com/api/v3/gear";
        /// <summary>
        /// Url to the endpoint used for receiving segment information.
        /// </summary>
        public const String Leaderboard = "https://www.strava.com/api/v3/segments";
        /// <summary>
        /// Url to the endpoint used for receiving starred segments.
        /// </summary>
        public const String Starred = "https://www.strava.com/api/v3/segments/starred";
        /// <summary>
        /// Url to the endpoint used for uploads.
        /// </summary>
        public const String Uploads = "https://www.strava.com/api/v3/uploads/";


    }
}
