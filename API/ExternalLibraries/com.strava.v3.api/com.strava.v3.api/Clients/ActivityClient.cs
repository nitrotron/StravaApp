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
using System.Collections.Generic;
using System.Threading.Tasks;
using com.strava.v3.api.Activities;
using com.strava.v3.api.Api;
using com.strava.v3.api.Athletes;
using com.strava.v3.api.Authentication;
using com.strava.v3.api.Common;
using com.strava.v3.api.Utilities;

namespace com.strava.v3.api.Clients
{
    /// <summary>
    /// Used to get activity data from Strava.
    /// </summary>
    public class ActivityClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the ActivityClient class.
        /// </summary>
        /// <param name="auth">A IAuthenticator object that contains a valid Strava access token.</param>
        public ActivityClient(IAuthentication auth) : base(auth) { }

        #region Events

        /// <summary>
        /// ActivityReceived is raised whenever an activity is received from Strava.
        /// </summary>
        public event EventHandler<ActivityReceivedEventArgs> ActivityReceived;

        #endregion

        #region Async

        /// <summary>
        /// Gets a single activity from Strava asynchronously.
        /// </summary>
        /// <param name="id">The Strava activity id.</param>
        /// <param name="includeEfforts">Used to include all segment efforts in the result.</param>
        /// <returns>The activity with the specified id.</returns>
        public async Task<Activity> GetActivityAsync(String id, bool includeEfforts)
        {
            String getUrl = String.Format("{0}/{1}?include_all_efforts={2}&access_token={3}", Endpoints.Activity, id, includeEfforts, Authentication.AccessToken);
            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<Activity>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the activities recorded before the specified date from Strava asynchronously.
        /// </summary>
        /// <param name="before">The date.</param>
        /// <returns>A list of activities recorded before the specified date.</returns>
        public async Task<List<ActivitySummary>> GetActivitiesBeforeAsync(DateTime before)
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = await GetActivitiesBeforeAsync(before, page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    activities.Add(activity);

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets all the activities recorded before the specified date from Strava asynchronously.
        /// </summary>
        /// <param name="before">The date.</param>
        /// <param name="page">The page of the list of activities.</param>
        /// <param name="perPage">The amount of activities per page.</param>
        /// <returns>A list of activities recorded before the specified date.</returns>
        public async Task<List<ActivitySummary>> GetActivitiesBeforeAsync(DateTime before, int page, int perPage)
        {
            //Calculate the UNIX epoch
            long secondsBefore = DateConverter.GetSecondsSinceUnixEpoch(before);

            String getUrl = String.Format("{0}?before={1}&page={2}&per_page={3}&access_token={4}",
                Endpoints.Activities,
                secondsBefore,
                page,
                perPage,
                Authentication.AccessToken
                );

            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the activities recorded after the specified date from Strava asynchronously.
        /// </summary>
        /// <param name="after">The date.</param>
        /// <returns>A list of activities recorded after the specified date.</returns>
        public async Task<List<ActivitySummary>> GetActivitiesAfterAsync(DateTime after)
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = await GetActivitiesAfterAsync(after, page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    activities.Add(activity);

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets all the activities recorded after the specified date from Strava asynchronously.
        /// </summary>
        /// <param name="after">The date.</param>
        /// <param name="page">The page of the list of activities.</param>
        /// <param name="perPage">The amount of activities per page.</param>
        /// <returns>A list of activities recorded after the specified date.</returns>
        public async Task<List<ActivitySummary>> GetActivitiesAfterAsync(DateTime after, int page, int perPage)
        {
            //Calculate the UNIX epoch
            long secondsAfter = DateConverter.GetSecondsSinceUnixEpoch(after);

            String getUrl = String.Format("{0}?after={1}&page={2}&per_page={3}&access_token={4}",
                Endpoints.Activities,
                secondsAfter,
                page,
                perPage,
                Authentication.AccessToken
                );

            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the comments of an activity asynchronously.
        /// </summary>
        /// <param name="activityId">The Strava Id of the activity.</param>
        /// <returns>A list of comments.</returns>
        public async Task<List<Comment>> GetCommentsAsync(String activityId)
        {
            String getUrl = String.Format("{0}/{1}/comments?access_token={2}", Endpoints.Activity, activityId, Authentication.AccessToken);
            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<Comment>>.Unmarshal(json);
        }

        /// <summary>
        /// Deletes an activity on Strava.
        /// </summary>
        /// <param name="activityId">The Strava Id of the activity to delete.</param>
        public async void DeleteActivity(String activityId)
        {
            String deleteUrl = String.Format("{0}/{1}?access_token={2}", Endpoints.Activities, activityId, Authentication.AccessToken);

            await Http.WebRequest.SendDeleteAsync(new Uri(deleteUrl));
        }

        /// <summary>
        /// Gets a list of athletes that kudoed the specified activity asynchronously.
        /// </summary>
        /// <param name="activityId">The Strava Id of the activity.</param>
        /// <returns>A list of athletes that kudoed the specified activity.</returns>
        public async Task<List<Athlete>> GetKudosAsync(String activityId)
        {
            String getUrl = String.Format("{0}/{1}/kudos?access_token={2}", Endpoints.Activity, activityId, Authentication.AccessToken);
            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<Athlete>>.Unmarshal(json);
        }

        /// <summary>
        /// Retrieves the zones of an activity asynchronously.
        /// </summary>
        /// <param name="activityId">The Strava activity Id.</param>
        /// <returns>A list of activity zones of an activity.</returns>
        public async Task<List<ActivityZone>> GetActivityZonesAsync(String activityId)
        {
            String getUrl = String.Format("{0}/{1}/zones?access_token={2}", Endpoints.Activity, activityId, Authentication.AccessToken);
            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<ActivityZone>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the activities asynchronously. Pagination is supported.
        /// </summary>
        /// <param name="page">The page of activities.</param>
        /// <param name="perPage">The amount of activities that are loaded per page.</param>
        /// <returns>A list of activities.</returns>
        public async Task<List<ActivitySummary>> GetActivitiesAsync(int page, int perPage)
        {
            String getUrl = String.Format("{0}?page={1}&per_page={2}&access_token={3}", Endpoints.Activities, page, perPage, Authentication.AccessToken);
            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets the latest activities of the currently authenticated athletes followers asynchronously.
        /// </summary>
        /// <param name="page">The page of activities.</param>
        /// <param name="perPage">The amount of activities per page.</param>
        /// <returns>A list of activities from your followers.</returns>
        public async Task<List<ActivitySummary>> GetFollowersActivitiesAsync(int page, int perPage)
        {
            String getUrl = String.Format("{0}?page={1}&per_page={2}&access_token={3}", Endpoints.ActivitiesFollowers, page, perPage, Authentication.AccessToken);
            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets the latest activities of the currently authenticated athletes friends asynchronously.
        /// </summary>
        /// <param name="count">Specifies how many activities should be loaded.</param>
        /// <returns>A list of activities from your friends.</returns>
        public async Task<List<ActivitySummary>> GetFriendsActivitiesAsync(int count)
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = await GetFollowersActivitiesAsync(page++, 20);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    if (activities.Count < count)
                    {
                        activities.Add(activity);
                    }
                    else
                    {
                        return activities;
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Retrieves all the currently authenticated athletes' activities asynchronously.
        /// </summary>
        /// <returns>All the activities of the currently authenticated athlete.</returns>
        public async Task<List<ActivitySummary>> GetAllActivitiesAsync()
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = await GetActivitiesAsync(page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    activities.Add(activity);

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets the total activity count of the currently authenticated athlete asynchronously.
        /// </summary>
        /// <returns>The total number of activities.</returns>
        public async Task<int> GetTotalActivityCountAsync()
        {
            List<ActivitySummary> activities = await GetAllActivitiesAsync();
            return activities.Count;
        }

        /// <summary>
        /// Updates an activity. Requires write permissions.
        /// </summary>
        /// <param name="activityId">The Strava id of the activity that will be updated.</param>
        /// <param name="parameter">The parameter that will be updated.</param>
        /// <param name="value">The value the parameter is updated to.</param>
        /// <returns>A detailed representation of the updated activity.</returns>
        public async Task<Activity> UpdateActivityAsync(String activityId, ActivityParameter parameter, String value)
        {
            String param = String.Empty;

            switch (parameter)
            {
                case ActivityParameter.Commute:
                    param = "name";
                    break;
                case ActivityParameter.Description:
                    param = "description";
                    break;
                case ActivityParameter.GearId:
                    param = "gear_id";
                    break;
                case ActivityParameter.Name:
                    param = "name";
                    break;
                case ActivityParameter.Private:
                    param = "private";
                    break;
                case ActivityParameter.Trainer:
                    param = "trainer";
                    break;
            }

            String putUrl = String.Format("{0}/{1}?{2}={3}&access_token={4}",
                Endpoints.Activity,
                activityId,
                param,
                value,
                Authentication.AccessToken);

            String json = await Http.WebRequest.SendPutAsync(new Uri(putUrl));

            return Unmarshaller<Activity>.Unmarshal(json);
        }

        /// <summary>
        /// Updates the type of an activity. Requires write permissions.
        /// </summary>
        /// <param name="activityId">The Strava id of the activity.</param>
        /// <param name="type">The type you want to change the activity to.</param>
        /// <returns>A detailed object of the activity that was updated. Remember that the changed type won't be updated immediately.</returns>
        public async Task<Activity> UpdateActivityTypeAsync(String activityId, ActivityType type)
        {
            String putUrl = String.Format("{0}/{1}?type={2}&access_token={3}",
                Endpoints.Activity,
                activityId,
                type,
                Authentication.AccessToken);

            String json = await Http.WebRequest.SendPutAsync(new Uri(putUrl));

            return Unmarshaller<Activity>.Unmarshal(json);
        }

        /// <summary>
        /// Gets the currently authenticated athletes progress for the current week.
        /// </summary>
        /// <returns>The weekly progress.</returns>
        public async Task<Summary> GetWeeklyProgressAsync()
        {
            Summary progress = new Summary();
            DateTime now = DateTime.Now;
            int days = 0;

            // What day is it today?
            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    days = 1;
                    break;
                case DayOfWeek.Tuesday:
                    days = 2;
                    break;
                case DayOfWeek.Wednesday:
                    days = 3;
                    break;
                case DayOfWeek.Thursday:
                    days = 4;
                    break;
                case DayOfWeek.Friday:
                    days = 5;
                    break;
                case DayOfWeek.Saturday:
                    days = 6;
                    break;
                case DayOfWeek.Sunday:
                    days = 7;
                    break;
            }

            // Calculate the date
            DateTime date = DateTime.Now - new TimeSpan(days, 0, 0, 0);
            progress.Start = date;
            progress.End = now;

            List<ActivitySummary> activities = await GetActivitiesAfterAsync(date);

            float rideDistance = 0F;
            float runDistance = 0f;
            
            foreach (ActivitySummary activity in activities)
            {
                if (activity.Type == ActivityType.Ride)
                {
                    progress.AddRide(activity);
                    rideDistance += activity.Distance;
                }
                else if (activity.Type == ActivityType.Run)
                {
                    progress.AddRun(activity);
                    runDistance += activity.Distance;
                }
                else
                {
                    progress.AddActivity(activity);
                }

                progress.AddTime(TimeSpan.FromSeconds(activity.MovingTime));
            }

            progress.RideDistance = rideDistance;
            progress.RunDistance = runDistance;

            return progress;
        }

        /// <summary>
        /// Gets all the activities recorded in a specified period of time.
        /// </summary>
        /// <param name="after">Date after the activity was recorded.</param>
        /// <param name="before">Date before the activity was recorded.</param>
        /// <returns>A list of activities that was recorded between 'after' and 'before'.</returns>
        public async Task<List<ActivitySummary>> GetActivitiesAsync(DateTime after, DateTime before)
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = await GetActivitiesAsync(after, before, page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    activities.Add(activity);

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets all the activities recorded in a specified period of time.
        /// </summary>
        /// <param name="after">Date after the activity was recorded.</param>
        /// <param name="before">Date before the activity was recorded.</param>
        /// <param name="page">Page of activities. Default value is 30.</param>
        /// <param name="perPage">Number of activities per page.</param>
        /// <returns>A list of activities that was recorded between 'after' and 'before'.</returns>
        public async Task<List<ActivitySummary>> GetActivitiesAsync(DateTime after, DateTime before, int page, int perPage)
        {
            String getUrl = String.Format("{0}?after={1}&before={2}&page={3}&per_page={4}&access_token={5}",
                Endpoints.Activities,
                DateConverter.GetSecondsSinceUnixEpoch(after),
                DateConverter.GetSecondsSinceUnixEpoch(before),
                page,
                perPage,
                Authentication.AccessToken);
            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets the summary of a certain date range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>A summary of the date range.</returns>
        public async Task<Summary> GetSummaryAsync(DateTime start, DateTime end)
        {
            Summary summary = new Summary
            {
                Start = start,
                End = end
            };

            int page = 1;
            bool hasEntries = true;
            float rideDistance = 0F;
            float runDistance = 0F;

            while (hasEntries)
            {
                List<ActivitySummary> request = await GetActivitiesAsync(start, end, page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (Activity activity in request)
                {
                    if (activity.Type == ActivityType.Ride)
                    {
                        summary.AddRide(activity);
                        rideDistance += activity.Distance;
                    }
                    else if (activity.Type == ActivityType.Run)
                    {
                        summary.AddRun(activity);
                        runDistance += activity.Distance;
                    }
                    else
                    {
                        summary.AddActivity(activity);
                    }

                    summary.AddTime(TimeSpan.FromSeconds(activity.MovingTime));

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            summary.RideDistance = rideDistance;
            summary.RunDistance = runDistance;

            return summary;
        }

        /// <summary>
        /// Get the summary of the last year.
        /// </summary>
        /// <returns>A summary of the last year.</returns>
        public async Task<Summary> GetSummaryLastYearAsync()
        {
            return await GetSummaryAsync(new DateTime(DateTime.Now.Year - 1, 1, 1),
                new DateTime(DateTime.Now.Year - 1, 12, 31, 23, 59, 59, DateTimeKind.Local));
        }

        /// <summary>
        /// Get the summary of this year.
        /// </summary>
        /// <returns>A summary of this year.</returns>
        public async Task<Summary> GetSummaryThisYearAsync()
        {
            return await GetSummaryAsync(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
        }

        /// <summary>
        /// Returns a list of recent photos.
        /// </summary>
        /// <returns>A list of Photos.</returns>
        public async Task<List<Photo>> GetLatestPhotosAsync(DateTime time)
        {
            List<Photo> photos = new List<Photo>();

            List<ActivitySummary> activities = await GetActivitiesAfterAsync(time);
            foreach (ActivitySummary a in activities)
            {
                if (a.PhotoCount > 0)
                {
                    List<Photo> p = await GetPhotosAsync(a.Id.ToString());
                    photos.AddRange(p);
                }

                if (photos.Count >= 4)
                    return photos;
            }

            return photos;
        }

        /// <summary>
        /// Returns a list of photos linked to the specified activity.
        /// </summary>
        /// <param name="activityId">The activity</param>
        /// <returns>A list of photos.</returns>
        public async Task<List<Photo>> GetPhotosAsync(String activityId)
        {
            // https://www.strava.com/api/v3/activities/:id/photos
            String getUrl = String.Format("{0}/{1}/photos?access_token={2}",
                Endpoints.Activity,
                activityId,
                Authentication.AccessToken);

            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<Photo>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets a list of all laps of an activity.
        /// </summary>
        /// <param name="activityId">The Strava id of the activity.</param>
        /// <returns>A list of laps of the activity.</returns>
        public async Task<List<ActivityLap>> GetActivityLapsAsync(String activityId)
        {
            // https://www.strava.com/api/v3/activities/:id/laps
            String getUrl = String.Format("{0}/{1}/laps?access_token={2}",
                Endpoints.Activity,
                activityId,
                Authentication.AccessToken);

            String json = await Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<ActivityLap>>.Unmarshal(json);
        }

        /// <summary>
        /// Give kudos for the specified activity.
        /// </summary>
        /// <param name="activityId">The activity you want to give kudos for.</param>
        public async void GiveKudos(String activityId)
        {
            String postUrl = String.Format("{0}/{1}/kudos?access_token={2}",
                Endpoints.Activity,
                activityId,
                Authentication.AccessToken);

            await Http.WebRequest.SendPostAsync(new Uri(postUrl));
        }

        /// <summary>
        /// Post a comment on the specified activity.
        /// </summary>
        /// <param name="activityId">The Strava ID of the activity you want to comment.</param>
        /// <param name="text">The text that will be posted.</param>
        public async void PostComment(String activityId, String text)
        {
            String postUrl = String.Format("{0}/{1}/comments?text={2}&access_token={3}",
                Endpoints.Activity,
                activityId,
                text,
                Authentication.AccessToken);

            await Http.WebRequest.SendPostAsync(new Uri(postUrl));
        }

        #endregion

        #region Sync

        /// <summary>
        /// Gets a single activity from Strava.
        /// </summary>
        /// <param name="id">The Strava activity id.</param>
        /// <param name="includeEfforts">Indicates whether efforts are included in the result or not.</param>
        /// <returns>The activity with the specified id.</returns>
        public Activity GetActivity(String id, bool includeEfforts)
        {
            String getUrl = String.Format("{0}/{1}?include_all_efforts={2}&access_token={3}", Endpoints.Activity, id, includeEfforts, Authentication.AccessToken);
            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<Activity>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the activities recorded before the specified date from Strava.
        /// </summary>
        /// <param name="before">The date.</param>
        /// <returns>A list of activities recorded before the specified date.</returns>
        public List<ActivitySummary> GetActivitiesBefore(DateTime before)
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = GetActivitiesBefore(before, page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    activities.Add(activity);

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets all the activities recorded before the specified date from Strava.
        /// </summary>
        /// <param name="before">The date.</param>
        /// <param name="page">The page of the list of activities.</param>
        /// <param name="perPage">The amount of activities per page.</param>
        /// <returns>A list of activities recorded before the specified date.</returns>
        public List<ActivitySummary> GetActivitiesBefore(DateTime before, int page, int perPage)
        {
            //Calculate the UNIX epoch
            long secondsBefore = DateConverter.GetSecondsSinceUnixEpoch(before);

            String getUrl = String.Format("{0}?before={1}&page={2}&per_page={3}&access_token={4}",
                Endpoints.Activities,
                secondsBefore,
                page,
                perPage,
                Authentication.AccessToken
                );

            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the activities recorded after the specified date from Strava.
        /// </summary>
        /// <param name="after">The date.</param>
        /// <returns>A list of activities recorded after the specified date.</returns>
        public List<ActivitySummary> GetActivitiesAfter(DateTime after)
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = GetActivitiesAfter(after, page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    activities.Add(activity);

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets all the activities recorded after the specified date from Strava.
        /// </summary>
        /// <param name="after">The date.</param>
        /// <param name="page">The page of the list of activities.</param>
        /// <param name="perPage">The amount of activities per page.</param>
        /// <returns>A list of activities recorded after the specified date.</returns>
        public List<ActivitySummary> GetActivitiesAfter(DateTime after, int page, int perPage)
        {
            //Calculate the UNIX epoch
            long secondsAfter = DateConverter.GetSecondsSinceUnixEpoch(after);

            String getUrl = String.Format("{0}?after={1}&page={2}&per_page={3}&access_token={4}",
                Endpoints.Activities,
                secondsAfter,
                page,
                perPage,
                Authentication.AccessToken
                );

            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the comments of an activity.
        /// </summary>
        /// <param name="activityId">The Strava Id of the activity.</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetComments(String activityId)
        {
            String getUrl = String.Format("{0}/{1}/comments?access_token={2}", Endpoints.Activity, activityId, Authentication.AccessToken);
            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<Comment>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets a list of athletes that kudoed the specified activity.
        /// </summary>
        /// <param name="activityId">The Strava Id of the activity.</param>
        /// <returns>A list of athletes that kudoed the specified activity.</returns>
        public List<Athlete> GetKudos(String activityId)
        {
            String getUrl = String.Format("{0}/{1}/kudos?access_token={2}", Endpoints.Activity, activityId, Authentication.AccessToken);
            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<Athlete>>.Unmarshal(json);
        }

        /// <summary>
        /// Retrieves the zones of an activity.
        /// </summary>
        /// <param name="activityId">The Strava activity Id.</param>
        /// <returns>A list of activity zones of an activity.</returns>
        public List<ActivityZone> GetActivityZones(String activityId)
        {
            String getUrl = String.Format("{0}/{1}/zones?access_token={2}", Endpoints.Activity, activityId, Authentication.AccessToken);
            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<ActivityZone>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the activities. Pagination is supported.
        /// </summary>
        /// <param name="page">The page of activities.</param>
        /// <param name="perPage">The amount of activities that are loaded per page.</param>
        /// <returns>A list of activities.</returns>
        public List<ActivitySummary> GetActivities(int page, int perPage)
        {
            String getUrl = String.Format("{0}?page={1}&per_page={2}&access_token={3}", Endpoints.Activities, page, perPage, Authentication.AccessToken);
            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets all the activities recorded in a specified period of time.
        /// </summary>
        /// <param name="after">Date after the activity was recorded.</param>
        /// <param name="before">Date before the activity was recorded.</param>
        /// <returns>A list of activities that was recorded between 'after' and 'before'.</returns>
        public List<ActivitySummary> GetActivities(DateTime after, DateTime before)
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = GetActivities(after, before, page++, 10);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    activities.Add(activity);

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets all the activities recorded in a specified period of time.
        /// </summary>
        /// <param name="after">Date after the activity was recorded.</param>
        /// <param name="before">Date before the activity was recorded.</param>
        /// <param name="page">Page of activities. Default value is 30.</param>
        /// <param name="perPage">Number of activities per page.</param>
        /// <returns>A list of activities that was recorded between 'after' and 'before'.</returns>
        public List<ActivitySummary> GetActivities(DateTime after, DateTime before, int page, int perPage)
        {
            String getUrl = String.Format("{0}?after={1}&before={2}&page={3}&per_page={4}&access_token={5}",
                Endpoints.Activities,
                DateConverter.GetSecondsSinceUnixEpoch(after),
                DateConverter.GetSecondsSinceUnixEpoch(before),
                page,
                perPage,
                Authentication.AccessToken);
            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets the latest activities of the currently authenticated athletes followers.
        /// </summary>
        /// <param name="page">The page of activities.</param>
        /// <param name="perPage">The amount of activities per page.</param>
        /// <returns>A list of activities from your followers.</returns>
        public List<ActivitySummary> GetFollowersActivities(int page, int perPage)
        {
            String getUrl = String.Format("{0}?page={1}&per_page={2}&access_token={3}", Endpoints.ActivitiesFollowers, page, perPage, Authentication.AccessToken);
            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
        }

        /// <summary>
        /// Retrieves all the currently authenticated athletes' activities.
        /// </summary>
        /// <returns>All the activities of the currently authenticated athlete.</returns>
        public List<ActivitySummary> GetAllActivities()
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = GetActivities(page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    activities.Add(activity);

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets the latest activities of the currently authenticated athletes friends.
        /// </summary>
        /// <param name="count">Specifies how many activities should be loaded.</param>
        /// <returns>A list of activities from your friends.</returns>
        public List<ActivitySummary> GetFriendsActivities(int count)
        {
            List<ActivitySummary> activities = new List<ActivitySummary>();
            int page = 1;
            bool hasEntries = true;

            while (hasEntries)
            {
                List<ActivitySummary> request = GetFollowersActivities(page++, 20);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    if (activities.Count < count)
                    {
                        activities.Add(activity);
                    }
                    else
                    {
                        return activities;
                    }
                }
            }

            return activities;
        }

        /// <summary>
        /// Gets the total activity count of the currently authenticated athlete.
        /// </summary>
        /// <returns>The number of activities.</returns>
        public int GetTotalActivityCount()
        {
            return GetAllActivities().Count;
        }

        /// <summary>
        /// Updates the type of an activity. Requires write permissions.
        /// </summary>
        /// <param name="activityId">The Strava id of the activity.</param>
        /// <param name="type">The type you want to change the activity to.</param>
        /// <returns>A detailed object of the activity that was updated. Remember that the changed type won't be updated immediately.</returns>
        public Activity UpdateActivityType(String activityId, ActivityType type)
        {
            String putUrl = String.Format("{0}/{1}?type={2}&access_token={3}",
                Endpoints.Activity,
                activityId,
                type,
                Authentication.AccessToken);

            String json = Http.WebRequest.SendPut(new Uri(putUrl));

            return Unmarshaller<Activity>.Unmarshal(json);
        }

        /// <summary>
        /// Updates an activity. Requires write permissions.
        /// </summary>
        /// <param name="activityId">The Strava id of the activity that will be updated.</param>
        /// <param name="parameter">The parameter that will be updated.</param>
        /// <param name="value">The value the parameter is updated to.</param>
        /// <returns>A detailed representation of the updated activity.</returns>
        public Activity UpdateActivity(String activityId, ActivityParameter parameter, String value)
        {
            String param = String.Empty;

            switch (parameter)
            {
                case ActivityParameter.Commute:
                    param = "name";
                    break;
                case ActivityParameter.Description:
                    param = "description";
                    break;
                case ActivityParameter.GearId:
                    param = "gear_id";
                    break;
                case ActivityParameter.Name:
                    param = "name";
                    break;
                case ActivityParameter.Private:
                    param = "private";
                    break;
                case ActivityParameter.Trainer:
                    param = "trainer";
                    break;
            }

            String putUrl = String.Format("{0}/{1}?{2}={3}&access_token={4}",
                Endpoints.Activity,
                activityId,
                param,
                value,
                Authentication.AccessToken);

            String json = Http.WebRequest.SendPut(new Uri(putUrl));

            return Unmarshaller<Activity>.Unmarshal(json);
        }

        /// <summary>
        /// Gets the currently authenticated athletes progress for the current week.
        /// </summary>
        /// <returns>The weekly progress.</returns>
        public Summary GetWeeklyProgress()
        {
            Summary progress = new Summary();

            DateTime now = DateTime.Now;
            int days = 0;

            // What day is it today?
            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    days = 1;
                    break;
                case DayOfWeek.Tuesday:
                    days = 2;
                    break;
                case DayOfWeek.Wednesday:
                    days = 3;
                    break;
                case DayOfWeek.Thursday:
                    days = 4;
                    break;
                case DayOfWeek.Friday:
                    days = 5;
                    break;
                case DayOfWeek.Saturday:
                    days = 6;
                    break;
                case DayOfWeek.Sunday:
                    days = 7;
                    break;
            }

            // Calculate the date
            DateTime date = DateTime.Now - new TimeSpan(days, 0, 0, 0);
            progress.Start = date;
            progress.End = now;

            List<ActivitySummary> activities = GetActivitiesAfter(date);

            float rideDistance = 0F;
            float runDistance = 0f;

            foreach (ActivitySummary activity in activities)
            {
                if (activity.Type == ActivityType.Ride)
                {
                    progress.AddRide(activity);
                    rideDistance += activity.Distance;
                }
                else if (activity.Type == ActivityType.Run)
                {
                    progress.AddRun(activity);
                    runDistance += activity.Distance;
                }
                else
                {
                    progress.AddActivity(activity);
                }

                progress.AddTime(TimeSpan.FromSeconds(activity.MovingTime));

                if (ActivityReceived != null)
                {
                    ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                }
            }

            progress.RideDistance = rideDistance;
            progress.RunDistance = runDistance;

            return progress;
        }

        /// <summary>
        /// Gets the summary of a certain date range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>A summary of the date range.</returns>
        public Summary GetSummary(DateTime start, DateTime end)
        {
            Summary summary = new Summary
            {
                Start = start,
                End = end
            };

            int page = 1;
            bool hasEntries = true;
            float rideDistance = 0F;
            float runDistance = 0F;

            while (hasEntries)
            {
                List<ActivitySummary> request = GetActivities(start, end, page++, 200);

                if (request.Count == 0)
                {
                    hasEntries = false;
                }

                foreach (ActivitySummary activity in request)
                {
                    if (activity.Type == ActivityType.Ride)
                    {
                        summary.AddRide(activity);
                        rideDistance += activity.Distance;
                    }
                    else if (activity.Type == ActivityType.Run)
                    {
                        summary.AddRun(activity);
                        runDistance += activity.Distance;
                    }
                    else
                    {
                        summary.AddActivity(activity);
                    }

                    summary.AddTime(TimeSpan.FromSeconds(activity.MovingTime));

                    if (ActivityReceived != null)
                    {
                        ActivityReceived(null, new ActivityReceivedEventArgs(activity));
                    }
                }
            }

            summary.RideDistance = rideDistance;
            summary.RunDistance = runDistance;

            return summary;
        }

        /// <summary>
        /// Get the summary of the last year.
        /// </summary>
        /// <returns>A summary of the last year.</returns>
        public Summary GetSummaryLastYear()
        {
            return GetSummary(new DateTime(DateTime.Now.Year - 1, 1, 1),
                new DateTime(DateTime.Now.Year - 1, 12, 31, 23, 59, 59, DateTimeKind.Local));
        }

        /// <summary>
        /// Get the summary of this year.
        /// </summary>
        /// <returns>A summary of this year.</returns>
        public Summary GetSummaryThisYear()
        {
            return GetSummary(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
        }

        /// <summary>
        /// Returns a list of recent photos.
        /// </summary>
        /// <returns>A list of Photos.</returns>
        public List<Photo> GetLatestPhotos(DateTime time)
        {
            List<Photo> photos = new List<Photo>();

            List<ActivitySummary> activities = GetActivitiesAfter(time);
            foreach (ActivitySummary a in activities)
            {
                if (a.PhotoCount > 0)
                {
                    List<Photo> p = GetPhotos(a.Id.ToString());
                    photos.AddRange(p);
                }
            }

            return photos;
        }

        /// <summary>
        /// Returns a list of photos linked to the specified activity.
        /// </summary>
        /// <param name="activityId">The activity</param>
        /// <returns>A list of photos.</returns>
        public List<Photo> GetPhotos(String activityId)
        {
            // https://www.strava.com/api/v3/activities/:id/photos
            String getUrl = String.Format("{0}/{1}/photos?access_token={2}",
                Endpoints.Activity,
                activityId,
                Authentication.AccessToken);

            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<Photo>>.Unmarshal(json);
        }

        /// <summary>
        /// Gets a list of all laps of an activity.
        /// </summary>
        /// <param name="activityId">The Strava id of the activity.</param>
        /// <returns>A list of laps of the activity.</returns>
        public List<ActivityLap> GetActivityLaps(String activityId)
        {
            String getUrl = String.Format("{0}/{1}/laps?access_token={2}",
                Endpoints.Activity,
                activityId,
                Authentication.AccessToken);

            String json = Http.WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller<List<ActivityLap>>.Unmarshal(json);
        }

        #endregion
    }
}
