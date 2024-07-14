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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using com.strava.v3.api.Api;

namespace com.strava.v3.api.Http
{
    /// <summary>
    /// Class to create web requests and receive a response from the server.
    /// </summary>
    public static class WebRequest
    {
        /// <summary>
        /// AsyncResponseReceived is raised when an asynchronous response is received from the server.
        /// </summary>
        public static event EventHandler<AsyncResponseReceivedEventArgs> AsyncResponseReceived;

        /// <summary>
        /// ResponseReceived is raised when a response is received from the server.
        /// </summary>
        public static event EventHandler<ResponseReceivedEventArgs> ResponseReceived;

        /// <summary>
        /// Sends a GET request to the server asynchronously.
        /// </summary>
        /// <param name="uri">The Uri where the request will be sent.</param>
        /// <returns>The server's response.</returns>
        public static async Task<String> SendGetAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Parameter uri must not be null. Please commit a valid Uri object.");
            }

            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(uri))
                {
                    if (response != null)
                    {
                        if (AsyncResponseReceived != null)
                        {
                            AsyncResponseReceived(null, new AsyncResponseReceivedEventArgs(response));
                        }

                        //Request was successful
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            //Getting the Strava API usage data.
                            KeyValuePair<String, IEnumerable<String>> usage = response.Headers.ToList().Find(x => x.Key.Equals("X-RateLimit-Usage"));

                            if (usage.Value != null)
                            {
                                //Setting the related Properties in the Limits-class.
                                Limits.Usage = new Usage(Int32.Parse(usage.Value.ElementAt(0).Split(',')[0]),
                                    Int32.Parse(usage.Value.ElementAt(0).Split(',')[1]));
                            }

                            //Getting the Strava API limits
                            KeyValuePair<String, IEnumerable<String>> limit = response.Headers.ToList().Find(x => x.Key.Equals("X-RateLimit-Limit"));

                            if (limit.Value != null)
                            {
                                //Setting the related Properties in the Limits-class.
                                Limits.Limit = new Limit(Int32.Parse(limit.Value.ElementAt(0).Split(',')[0]),
                                    Int32.Parse(limit.Value.ElementAt(0).Split(',')[1]));
                            }

                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Sends a POST request to the server asynchronously.
        /// </summary>
        /// <param name="uri">The Uri where the request will be sent.</param>
        /// <returns>The server's response.</returns>
        public static async Task<String> SendPostAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Parameter uri must not be null. Please commit a valid Uri object.");
            }

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.PostAsync(uri, null))
                {
                    if (response != null)
                    {
                        if (AsyncResponseReceived != null)
                        {
                            AsyncResponseReceived(null, new AsyncResponseReceivedEventArgs(response));
                        }

                        //Getting the Strava API usage data.
                        KeyValuePair<String, IEnumerable<String>> usage = response.Headers.ToList().Find(x => x.Key.Equals("X-RateLimit-Usage"));

                        if (usage.Value != null)
                        {
                            //Setting the related Properties in the Limits-class.
                            Limits.Usage = new Usage(Int32.Parse(usage.Value.ElementAt(0).Split(',')[0]),
                                Int32.Parse(usage.Value.ElementAt(0).Split(',')[1]));
                        }

                        //Getting the Strava API limits
                        KeyValuePair<String, IEnumerable<String>> limit = response.Headers.ToList().Find(x => x.Key.Equals("X-RateLimit-Limit"));

                        if (limit.Value != null)
                        {
                            //Setting the related Properties in the Limits-class.
                            Limits.Limit = new Limit(Int32.Parse(limit.Value.ElementAt(0).Split(',')[0]),
                                Int32.Parse(limit.Value.ElementAt(0).Split(',')[1]));
                        }

                        //Request was successful
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Sends a PUT request to the server asynchronously.
        /// </summary>
        /// <param name="uri">The Uri where the request will be sent.</param>
        /// <returns>The server's response.</returns>
        public static async Task<String> SendPutAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Parameter uri must not be null. Please commit a valid Uri object.");
            }

            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.PutAsync(uri, null))
                {
                    if (response != null)
                    {
                        if (AsyncResponseReceived != null)
                        {
                            AsyncResponseReceived(null, new AsyncResponseReceivedEventArgs(response));
                        }

                        //Getting the Strava API usage data.
                        KeyValuePair<String, IEnumerable<String>> usage = response.Headers.ToList().Find(x => x.Key.Equals("X-RateLimit-Usage"));

                        if (usage.Value != null)
                        {
                            //Setting the related Properties in the Limits-class.
                            Limits.Usage = new Usage(Int32.Parse(usage.Value.ElementAt(0).Split(',')[0]),
                                Int32.Parse(usage.Value.ElementAt(0).Split(',')[1]));
                        }

                        //Getting the Strava API limits
                        KeyValuePair<String, IEnumerable<String>> limit = response.Headers.ToList().Find(x => x.Key.Equals("X-RateLimit-Limit"));

                        if (limit.Value != null)
                        {
                            //Setting the related Properties in the Limits-class.
                            Limits.Limit = new Limit(Int32.Parse(limit.Value.ElementAt(0).Split(',')[0]),
                                Int32.Parse(limit.Value.ElementAt(0).Split(',')[1]));
                        }

                        //Request was successful
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Sends a DELETE request to the server asynchronously.
        /// </summary>
        /// <param name="uri">The Uri where the request will be sent.</param>
        /// <returns>The server's response.</returns>
        public static async Task<String> SendDeleteAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Parameter uri must not be null. Please commit a valid Uri object.");
            }

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.DeleteAsync(uri))
                {

                    if (response != null)
                    {
                        if (AsyncResponseReceived != null)
                        {
                            AsyncResponseReceived(null, new AsyncResponseReceivedEventArgs(response));
                        }

                        //Getting the Strava API usage data.
                        KeyValuePair<String, IEnumerable<String>> usage = response.Headers.ToList().Find(x => x.Key.Equals("X-RateLimit-Usage"));

                        if (usage.Value != null)
                        {
                            //Setting the related Properties in the Limits-class.
                            Limits.Usage = new Usage(Int32.Parse(usage.Value.ElementAt(0).Split(',')[0]),
                                Int32.Parse(usage.Value.ElementAt(0).Split(',')[1]));
                        }

                        //Getting the Strava API limits
                        KeyValuePair<String, IEnumerable<String>> limit = response.Headers.ToList().Find(x => x.Key.Equals("X-RateLimit-Limit"));

                        if (limit.Value != null)
                        {
                            //Setting the related Properties in the Limits-class.
                            Limits.Limit = new Limit(Int32.Parse(limit.Value.ElementAt(0).Split(',')[0]),
                                Int32.Parse(limit.Value.ElementAt(0).Split(',')[1]));
                        }

                        //Request was successful
                        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Sends a GET request to the server.
        /// </summary>
        /// <param name="uri">The Uri where the request will be sent.</param>
        /// <returns>The server's response.</returns>
        public static String SendGet(Uri uri)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            httpRequest.Method = "GET";

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                Stream responseStream = httpResponse.GetResponseStream();

                if (responseStream != null)
                {
                    if (ResponseReceived != null)
                    {
                        ResponseReceived(null, new ResponseReceivedEventArgs(httpResponse));
                    }

                    //Getting the Strava API usage data.
                    String usage = httpResponse.GetResponseHeader("X-RateLimit-Usage");

                    //Getting the Strava API limits
                    String limit = httpResponse.GetResponseHeader("X-RateLimit-Limit");

                    if (!String.IsNullOrEmpty(usage))
                    {
                        //Setting the related Properties in the Limits-class.
                        Limits.Usage = new Usage(Int32.Parse(usage.Split(',')[0]), Int32.Parse(usage.Split(',')[1]));
                    }

                    if (!String.IsNullOrEmpty(limit))
                    {
                        //Setting the related Properties in the Limits-class.
                        Limits.Limit = new Limit(Int32.Parse(limit.Split(',')[0]), Int32.Parse(limit.Split(',')[1]));
                    }

                    StreamReader reader = new StreamReader(responseStream);
                    String response = reader.ReadToEnd();

                    // Close both streams.
                    reader.Close();
                    responseStream.Close();

                    return response;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Sends a PUT request to the server.
        /// </summary>
        /// <param name="uri">The Uri where the request will be sent.</param>
        /// <returns>The server's response.</returns>
        public static String SendPut(Uri uri)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            httpRequest.Method = "PUT";

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                Stream responseStream = httpResponse.GetResponseStream();

                if (responseStream != null)
                {
                    if (ResponseReceived != null)
                    {
                        ResponseReceived(null, new ResponseReceivedEventArgs(httpResponse));
                    }

                    //Getting the Strava API usage data.
                    String usage = httpResponse.GetResponseHeader("X-RateLimit-Usage");

                    //Getting the Strava API limits
                    String limit = httpResponse.GetResponseHeader("X-RateLimit-Limit");

                    if (!String.IsNullOrEmpty(usage))
                    {
                        //Setting the related Properties in the Limits-class.
                        Limits.Usage = new Usage(Int32.Parse(usage.Split(',')[0]), Int32.Parse(usage.Split(',')[1]));
                    }

                    if (!String.IsNullOrEmpty(limit))
                    {
                        //Setting the related Properties in the Limits-class.
                        Limits.Limit = new Limit(Int32.Parse(limit.Split(',')[0]), Int32.Parse(limit.Split(',')[1]));
                    }

                    StreamReader reader = new StreamReader(responseStream);
                    String response = reader.ReadToEnd();

                    // Close both streams.
                    reader.Close();
                    responseStream.Close();

                    return response;
                }
            }

            return String.Empty;
        }
        public static String SendPost(Uri uri)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            httpRequest.Method = "Post";

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                Stream responseStream = httpResponse.GetResponseStream();

                if (responseStream != null)
                {
                    if (ResponseReceived != null)
                    {
                        ResponseReceived(null, new ResponseReceivedEventArgs(httpResponse));
                    }

                    //Getting the Strava API usage data.
                    String usage = httpResponse.GetResponseHeader("X-RateLimit-Usage");

                    //Getting the Strava API limits
                    String limit = httpResponse.GetResponseHeader("X-RateLimit-Limit");

                    if (!String.IsNullOrEmpty(usage))
                    {
                        //Setting the related Properties in the Limits-class.
                        Limits.Usage = new Usage(Int32.Parse(usage.Split(',')[0]), Int32.Parse(usage.Split(',')[1]));
                    }

                    if (!String.IsNullOrEmpty(limit))
                    {
                        //Setting the related Properties in the Limits-class.
                        Limits.Limit = new Limit(Int32.Parse(limit.Split(',')[0]), Int32.Parse(limit.Split(',')[1]));
                    }

                    StreamReader reader = new StreamReader(responseStream);
                    String response = reader.ReadToEnd();

                    // Close both streams.
                    reader.Close();
                    responseStream.Close();

                    return response;
                }
            }

            return String.Empty;
        }
    }





}
