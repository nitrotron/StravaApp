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

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StravaUserController(DataContext context) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<Athlete>> SyncUser()
        {
            try
            {
                List<Models.Oauth> oauths = await context.Oauths.ToListAsync();
                if (oauths.Count != 1)
                {
                    throw new Exception("wrong number oAuth rows");
                }
                Models.Oauth oauth = oauths[0];

                if (DateTimeOffset.FromUnixTimeSeconds(oauth.ExpiresAtEpoch).DateTime.AddHours(-1) < DateTime.UtcNow)
                {
                    StaticAuthentication refresh = new StaticAuthentication(oauth.RefreshToken);
                    RefreshClient refreshClient = new RefreshClient(refresh);
                    AccessToken token = await refreshClient.RefreshAccessTokenAsync(oauth.RefreshToken, oauth.ClientID, oauth.ClientSecret);

                    oauth.AccessToken = token.Token;
                    oauth.RefreshToken = token.RefreshToken;
                    long.TryParse(token.ExpiresAt, out long tempEpoch); //hacky
                    oauth.ExpiresAtEpoch = tempEpoch;
                    context.SaveChanges();
                }



                StaticAuthentication auth = new StaticAuthentication(oauth.AccessToken);
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
                return Ok(athlete);
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }

        }
    }
}
