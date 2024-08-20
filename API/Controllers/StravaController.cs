using System.Globalization;
using API.Data;
using Models = API.Models;
using com.strava.v3.api.Activities;
// using com.strava.v3.api.Athletes;
using com.strava.v3.api.Authentication;
using com.strava.v3.api.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Athlete = com.strava.v3.api.Athletes.Athlete;
using API.Models;
using com.strava.v3.api.Common;

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StravaController(DataContext context) : ControllerBase
    {


        [HttpGet]
        [Route("GetClientId")]
        public async Task<ActionResult<int>> GetClientId()
        {
            Models.Oauth? oauth = await context.Oauths.FirstOrDefaultAsync();
            return oauth == null ? NotFound(0) : oauth.ClientID;
        }

        public class AccessModel
        {
            public string code { get; set; }
            public string scope { get; set; }
            public string state { get; set; }
        }
        [HttpPost]
        [Route("UpdateAccessToken")]
        // public async Task<ActionResult<Models.Athlete>> UpdateAccessToken([FromBody] string code, [FromBody] string scope, [FromBody] string state)
        public async Task<ActionResult<Models.Athlete>> UpdateAccessToken([FromBody] AccessModel accessModel)
        {
            Models.User? user = await context.Users.FirstOrDefaultAsync();
            string code = accessModel.code;
            string scope = accessModel.scope;
            string state = accessModel.state;
            if (user == null)
            {
                user = new Models.User
                {
                    AccessCode = code,
                    Name = "",
                    AccessToken = "",
                    RefreshToken = "",
                    ExpiresAtEpoch = 0,
                    Scope = scope
                };
            }
            user.AccessCode = code;
            user.Scope = scope;

            List<Models.Oauth> oauths = await context.Oauths.ToListAsync();
            if (oauths.Count != 1)
            {
                throw new Exception("wrong number oAuth rows");
            }
            Models.Oauth oauth = oauths[0];

            String url = String.Format("https://www.strava.com/oauth/token?client_id={0}&client_secret={1}&code={2}&grant_type=authorization_code", oauth.ClientID, oauth.ClientSecret, code);
            String json = await com.strava.v3.api.Http.WebRequest.SendPostAsync(new Uri(url));

            AccessToken accessToken = Unmarshaller<AccessToken>.Unmarshal(json);
            long.TryParse(accessToken.ExpiresAt, out long tmp);
            user.ExpiresAtEpoch = tmp;
            user.AccessToken = accessToken.Token;
            user.RefreshToken = accessToken.RefreshToken;
            context.SaveChanges();

            StaticAuthentication auth = new StaticAuthentication(user.AccessToken);
            // AthleteClient athleteClient = new AthleteClient(auth);

            // Athlete athlete = await athleteClient.GetAthleteAsync();
            StravaClient client = new StravaClient(auth);

            Athlete athlete = client.Athletes.GetAthlete();
            Models.Athlete? dbAthlete = context.Athletes.Where(a => a.Id == athlete.Id).FirstOrDefault();
            if (dbAthlete == null ||
                DateTime.Parse(dbAthlete.UpdatedAt, CultureInfo.InvariantCulture) < DateTime.Parse(athlete.UpdatedAt, CultureInfo.InvariantCulture))
            {
                bool doInsert = dbAthlete == null;
                dbAthlete = Models.Athlete.ApiAthleteToModel(athlete);
                if (doInsert)
                    context.Athletes.Add(dbAthlete);
                context.SaveChanges();
            }


            return Ok(dbAthlete);
        }

        [HttpGet]
        [Route("SyncAll/{athleteId:long}")]
        public async Task<ActionResult<Models.Athlete>> SyncAll(long athleteId)
        {
            try
            {
                // Models.Athlete? dbAthlete = await context.Athletes.FirstOrDefaultAsync();

                // Models.User? user =  await context.Users.FirstOrDefaultAsync();
                Models.User? user = await context.Users.Where(x => x.AthleteId == athleteId).FirstOrDefaultAsync<User>();
                if (user == null)
                {
                    throw new Exception("User not found.");
                }


                List<Models.Oauth> oauths = await context.Oauths.ToListAsync();
                if (oauths.Count != 1)
                {
                    throw new Exception("wrong number oAuth rows");
                }
                Models.Oauth oauth = oauths[0];

                if (DateTimeOffset.FromUnixTimeSeconds(user.ExpiresAtEpoch).DateTime.AddHours(-1) < DateTime.UtcNow)
                {
                    StaticAuthentication refresh = new StaticAuthentication(user.RefreshToken);
                    RefreshClient refreshClient = new RefreshClient(refresh);
                    AccessToken token = await refreshClient.RefreshAccessTokenAsync(user.RefreshToken, oauth.ClientID, oauth.ClientSecret);

                    user.AccessToken = token.Token;
                    user.RefreshToken = token.RefreshToken;
                    long.TryParse(token.ExpiresAt, out long tempEpoch); //hacky
                    user.ExpiresAtEpoch = tempEpoch;
                    context.SaveChanges();
                }



                StaticAuthentication auth = new StaticAuthentication(user.AccessToken);
                StravaClient client = new StravaClient(auth);

                Athlete athlete = client.Athletes.GetAthlete();
                Models.Athlete? dbAthlete = context.Athletes.Where(a => a.Id == athlete.Id).FirstOrDefault();
                if (dbAthlete == null ||
                    DateTime.Parse(dbAthlete.UpdatedAt, CultureInfo.InvariantCulture) < DateTime.Parse(athlete.UpdatedAt, CultureInfo.InvariantCulture))
                {
                    bool doInsert = dbAthlete == null;
                    dbAthlete = Models.Athlete.ApiAthleteToModel(athlete);
                    if (doInsert)
                        context.Athletes.Add(dbAthlete);
                    context.SaveChanges();
                }




                List<ActivitySummary> activities = client.Activities.GetAllActivities();
                List<Models.Activity> dbActivities = context.Activities.Where(a => a.AthleteId == athlete.Id).ToList();

                foreach (ActivitySummary activitySum in activities)
                {
                    Models.Activity? matchActivity = dbActivities.FirstOrDefault(x => x.Id == activitySum.Id);

                    if (matchActivity == null)
                    {
                        context.Activities.Add(Models.Activity.ApiActivityToModel(activitySum));
                    }

                }
                context.SaveChanges();


                // Activity activity = client.Activities.GetActivity("11841281294", true);
                return Ok(dbAthlete);
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }

        }
        [HttpGet]
        [Route("GetAthlete")]
        public async Task<ActionResult<Models.Athlete>> GetAthlete()
        {
            Models.Athlete? dbAthlete = await context.Athletes.FirstOrDefaultAsync();

            return dbAthlete == null ? NotFound(dbAthlete) : Ok(dbAthlete);
        }

        [HttpGet]
        [Route("GetActivities/{athleteId:long}")]
        public async Task<ActionResult<IEnumerable<Models.Athlete>>> GetActivities(long athleteId)
        {
            List<Models.Activity> activities = await context.Activities.Where(x => x.AthleteId == athleteId).ToListAsync();

            return activities == null ? NotFound("Activities not found. Run Sync") : Ok(activities);
        }
    }
}
