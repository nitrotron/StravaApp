using com.strava.v3.api.Activities;
using Newtonsoft.Json;

namespace API.Models;

public class Activity
{
    public long Id { get; set; }

    /// <summary>
    /// The activity's name.
    /// </summary>
    public String Name { get; set; }

    /// <summary>
    /// The type of the activity.
    /// </summary>
    public String Type { get; set; }

    /// <summary>
    /// The sport type of the activity.
    /// </summary>
    public String SportType { get; set; }

    /// <summary>
    /// The distance travelled.
    /// </summary>
    public float Distance { get; set; }

    /// <summary>
    /// Time in movement in seconds.
    /// </summary>
    public int MovingTime { get; set; }

    /// <summary>
    /// The elapsed time in seconds.
    /// </summary>
    public int ElapsedTime { get; set; }

    /// <summary>
    /// The total elevation gain in meters.
    /// </summary>
    public float ElevationGain { get; set; }

    /// <summary>
    /// True if the currently authenticated athlete has kudoed this activity.
    /// </summary>
    public bool HasKudoed { get; set; }

    /// <summary>
    /// The athlete's average heartrate during this activity.
    /// </summary>
    public float AverageHeartrate { get; set; }

    /// <summary>
    /// The athlete's max heartrate.
    /// </summary>
    public float MaxHeartrate { get; set; }

    /// <summary>
    /// Only present if activity is owned by authenticated athlete, returns 0 if not truncated by privacy zones.
    /// </summary>
    public int? Truncated { get; set; }

    /// <summary>
    /// The city where this activity was started.
    /// </summary>
    // public String City { get => _city??""; set => _city = value; }
    // private String? _city;
    public String City {get; set;}

    /// <summary>
    /// The state where this activity was started.
    /// </summary>
    public String State { get; set; }

    /// <summary>
    /// The country where this activity was started.
    /// </summary>
    public String Country { get; set; }

    /// <summary>
    /// The id of the gear used.
    /// </summary>
    public String GearId { get; set; }

    /// <summary>
    /// The average speed in meters per seconds.
    /// </summary>
    public float AverageSpeed { get; set; }

    /// <summary>
    /// The max speed in meters per second.
    /// </summary>
    public float MaxSpeed { get; set; }

    /// <summary>
    /// The average cadence. Only returned if activity is a bike ride.
    /// </summary>
    public float AverageCadence { get; set; }

    /// <summary>
    /// The average temperature during this activity. Only returned if data is provided upon upload.
    /// </summary>
    public float AverageTemperature { get; set; }

    /// <summary>
    /// The average power during this activity. Only returned if data is provided upon upload.
    /// </summary>
    public float AveragePower { get; set; }

    /// <summary>
    /// Kilojoules. Rides only.
    /// </summary>
    public float Kilojoules { get; set; }

    /// <summary>
    /// True if the activity was recorded on a stationary trainer.
    /// </summary>
    public bool IsTrainer { get; set; }

    /// <summary>
    /// True if activity is a a commute.
    /// </summary>
    public bool IsCommute { get; set; }

    /// <summary>
    /// True if the ride was crated manually.
    /// </summary>
    public bool IsManual { get; set; }

    /// <summary>
    /// True if the activity is private.
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// True if the activity was flagged.
    /// </summary>
    public bool IsFlagged { get; set; }

    /// <summary>
    /// Achievement count.
    /// </summary>
    public int AchievementCount { get; set; }

    /// <summary>
    /// Activity's kudos count.
    /// </summary>
    public int KudosCount { get; set; }

    /// <summary>
    /// Activity's comment count.
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// Number of athletes on this activity.
    /// </summary>
    public int AthleteCount { get; set; }

    /// <summary>
    /// Number of photos.
    /// </summary>
    public int PhotoCount { get; set; }

    /// <summary>
    /// Start date of the activity.
    /// </summary>
    public String StartDate { get; set; }

    /// <summary>
    /// Local start date of the activity.
    /// </summary>
    public String StartDateLocal { get; set; }

    /// <summary>
    /// Timezone of the activity.
    /// </summary>
    public String TimeZone { get; set; }

    /// <summary>
    /// Coordinate where the activity was started.
    /// </summary>
    public List<double> StartPoint { get; set; }

    /// <summary>
    /// Coordinate where the activity was started.
    /// </summary>
    public double? StartLatitude { get; set; }


    /// <summary>
    /// Rides with power meter data only similar to xPower or Normalized Power.
    /// </summary>
    public int WeightedAverageWatts { get; set; }

    /// <summary>
    /// Coordinate where the activity was started.
    /// </summary>
    public double? StartLongitude { get; set; }


    /// <summary>
    /// Coordinate where the activity was ended.
    /// </summary>
    public List<double> EndPoint { get; set; }

    /// <summary>
    /// Coordinate where the activity was ended.
    /// </summary>
    public double? EndLatitude { get; set; }

    /// <summary>
    /// Coordinate where the activity was ended.
    /// </summary>
    public double? EndLongitude { get; set; }

    /// <summary>
    /// True if the power data comes from a power meter, false if the data is estimated.
    /// </summary>
    public bool HasPowerMeter { get; set; }

    /// <summary>
    /// Map representing the route of the activity.
    /// </summary>
    public long MapId { get; set; }

    /// <summary>
    /// Meta object of the athlete of this activity.
    /// </summary>
    public long AthleteId { get; set; }

    /// <summary>
    /// Suffer Score value for the activity
    /// </summary>
    public Double SufferScore { get; set; }

    /// <summary>
    /// The identifier of the upload that resulted in this activity
    /// </summary>
    public long? uploadId { get; set; }

    /// <summary>
    /// The unique identifier of the upload in string format
    /// </summary>
    public string uploadIdStr { get; set; }

    /// <summary>
    /// The name of the device used to record the activity
    /// </summary>
    public string deviceName { get; set; }

    public static Activity ApiActivityToModel(com.strava.v3.api.Activities.ActivitySummary activitySummary)
    {
        Activity returnActivity = new Activity
        {
            Id = activitySummary.Id,
            Name = activitySummary.Name,
            Type =  activitySummary.Type.ToString(),
            SportType = activitySummary.SportType.ToString(),
            Distance = activitySummary.Distance,
            MovingTime = activitySummary.MovingTime,
            ElapsedTime = activitySummary.ElapsedTime,
            ElevationGain = activitySummary.ElevationGain,
            HasKudoed = activitySummary.HasKudoed,
            AverageHeartrate = activitySummary.AverageHeartrate,
            MaxHeartrate = activitySummary.MaxHeartrate,
            Truncated = activitySummary.Truncated,
            City = activitySummary.City??"",
            State = activitySummary.State??"",
            Country = activitySummary.Country??"",
            GearId = activitySummary.GearId??"",
            AverageSpeed = activitySummary.AverageSpeed,
            MaxSpeed = activitySummary.MaxSpeed,
            AverageCadence = activitySummary.AverageCadence,
            AverageTemperature = activitySummary.AverageTemperature,
            AveragePower = activitySummary.AveragePower,
            Kilojoules = activitySummary.Kilojoules,
            IsTrainer = activitySummary.IsTrainer,
            IsCommute = activitySummary.IsCommute,
            IsManual = activitySummary.IsManual,
            IsPrivate = activitySummary.IsPrivate,
            IsFlagged = activitySummary.IsFlagged,
            AchievementCount = activitySummary.AchievementCount,
            KudosCount = activitySummary.KudosCount,
            CommentCount = activitySummary.CommentCount,
            AthleteCount = activitySummary.AthleteCount,
            PhotoCount = activitySummary.PhotoCount,
            StartDate = activitySummary.StartDate,
            StartDateLocal = activitySummary.StartDateLocal,
            TimeZone = activitySummary.TimeZone,
            StartPoint = activitySummary.StartPoint,
            StartLatitude = activitySummary.StartLatitude,
            WeightedAverageWatts = activitySummary.WeightedAverageWatts,
            StartLongitude = activitySummary.StartLongitude,
            EndPoint = activitySummary.EndPoint,
            EndLatitude = activitySummary.EndLatitude,
            EndLongitude = activitySummary.EndLongitude,
            HasPowerMeter = activitySummary.HasPowerMeter,
            AthleteId = activitySummary.Athlete.Id,
            SufferScore = activitySummary.SufferScore,
            uploadId = activitySummary.uploadId,
            uploadIdStr = activitySummary.uploadIdStr,
            deviceName = activitySummary.deviceName??""
        };
        long.TryParse(activitySummary.Map.Id, out long temp);
        returnActivity.MapId = temp;

    return returnActivity;
    }
}
