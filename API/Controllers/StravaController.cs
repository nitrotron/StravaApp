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

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StravaController(DataContext context) : ControllerBase
    {

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

            return dbAthlete == null ? NotFound("Athlete not found. Run authorization") : Ok(dbAthlete);
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
