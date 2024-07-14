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
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using com.strava.v3.api.Activities;
using com.strava.v3.api.Api;
using com.strava.v3.api.Authentication;
using com.strava.v3.api.Common;
using com.strava.v3.api.Http;
using com.strava.v3.api.Upload;

namespace com.strava.v3.api.Clients
{
    /// <summary>
    /// This class contains methods for uploading new activities to Strava. Use the UploadStatusCheck class to 
    /// check the status of an upload.
    /// </summary>
    public class UploadClient : BaseClient
    {
        #region Async

        /// <summary>
        /// Initializes a new instance of the UploadClient class.
        /// </summary>
        /// <param name="auth">The IAuthenticator object used to authenticate with Strava.</param>
        public UploadClient(IAuthentication auth) : base(auth) { }

        /// <summary>
        /// Uploads an activity.
        /// </summary>
        /// <param name="filePath">The path to the activity file on your local hard disk.</param>
        /// <param name="dataFormat">The format of the file.</param>
        /// <param name="activityType">The type of the activity.</param>
        /// <returns>The status of the upload.</returns>
        public async Task<UploadStatus> UploadActivityAsync(String filePath, DataFormat dataFormat, ActivityType activityType = ActivityType.Ride)
        {
            String format = String.Empty;

            switch (dataFormat)
            {
                case DataFormat.Fit:
                    format = "fit";
                    break;
                case DataFormat.FitGZipped:
                    format = "fit.gz";
                    break;
                case DataFormat.Gpx:
                    format = "gpx";
                    break;
                case DataFormat.GpxGZipped:
                    format = "gpx.gz";
                    break;
                case DataFormat.Tcx:
                    format = "tcx";
                    break;
                case DataFormat.TcxGZipped:
                    format = "tcx.gz";
                    break;
            }

            FileInfo info = new FileInfo(filePath);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", Authentication.AccessToken));

            MultipartFormDataContent content = new MultipartFormDataContent();

            content.Add(new ByteArrayContent(File.ReadAllBytes(info.FullName)), "file", info.Name);

            HttpResponseMessage result = await client.PostAsync(
                String.Format("https://www.strava.com/api/v3/uploads?data_type={0}&activity_type={1}",
                format,
                activityType.ToString().ToLower()),
                content);

            String json = await result.Content.ReadAsStringAsync();

            return Unmarshaller<UploadStatus>.Unmarshal(json);
        }

        /// <summary>
        /// Checks the status of an upload.
        /// </summary>
        /// <param name="uploadId">The id of the upload.</param>
        /// <returns>The status of the upload.</returns>
        public async Task<UploadStatus> CheckUploadStatusAsync(String uploadId)
        {
            String checkUrl = String.Format("{0}/{1}?access_token={2}", Endpoints.Uploads, uploadId, Authentication.AccessToken);
            String json = await WebRequest.SendGetAsync(new Uri(checkUrl));

            return Unmarshaller<UploadStatus>.Unmarshal(json);
        }

        #endregion

        #region Sync

        /// <summary>
        /// Checks the status of an upload.
        /// </summary>
        /// <param name="uploadId">The id of the upload.</param>
        /// <returns>The status of the upload.</returns>
        public UploadStatus CheckUploadStatus(String uploadId)
        {
            String checkUrl = String.Format("{0}/{1}?access_token={2}", Endpoints.Uploads, uploadId, Authentication.AccessToken);
            String json = WebRequest.SendGet(new Uri(checkUrl));

            return Unmarshaller<UploadStatus>.Unmarshal(json);
        }

        #endregion
    }
}
