

namespace API.Models;

public class Athlete
{

    // public Athlete(com.strava.v3.api.Athletes.Athlete athlete)
    // {
    //     this.athlete = athlete;
    // }

    public long Id { get; set; }
    /// <summary>
    /// The athletes first name.
    /// </summary>
    public String FirstName { get; set; }

    /// <summary>
    /// The athletes last name.
    /// </summary>
    public String LastName { get; set; }

    /// <summary>
    /// Url to a 62x62 pixel profile picture. You can use the ImageLoader class to load this picture.
    /// </summary>
    public String ProfileMedium { get; set; }

    /// <summary>
    /// Url to a 124x124 pixel profile picture. You can use the ImageLoader class to load this picture.
    /// </summary>
    public String Profile { get; set; }

    /// <summary>
    /// The city where the athlete lives.
    /// </summary>
    public String City { get; set; }

    /// <summary>
    /// The state where the athlete lives.
    /// </summary>
    public String State { get; set; }

    /// <summary>
    /// The state where the athlete lives.
    /// </summary>
    public String Country { get; set; }

    /// <summary>
    /// The athlete's sex.
    /// </summary>
    public String Sex { get; set; }


    /// <summary>
    /// True, if the athlete is a Strava premium member. In some cases this attribute is important, for example when leaderboards are filtered
    /// by either weight class or age group.
    /// </summary>
    public Boolean IsPremium { get; set; }

    /// <summary>
    /// The date when this athlete was created. ISO 8601 time string.
    /// </summary>
    public String CreatedAt { get; set; }

    /// <summary>
    /// The date when this athlete was updated. ISO 8601 time string.
    /// </summary>
    public String UpdatedAt { get; set; }

    /// <summary>
    /// The count of the athlete's followers.
    /// </summary>
    public int FollowerCount { get; set; }

    /// <summary>
    /// The count of the athlete's friends.
    /// </summary>
    public int FriendCount { get; set; }

    /// <summary>
    /// The date preference. ISO 8601 time string.
    /// </summary>
    public String DatePreference { get; set; }

    /// <summary>
    /// Either 'feet' or 'meters'
    /// </summary>
    public String MeasurementPreference { get; set; }

    /// <summary>
    /// The email address.
    /// </summary>
    public String ?Email { get; set; }

    /// <summary>
    /// The functional threshold power.
    /// </summary>
    public int? Ftp { get; set; }
    public static Athlete ApiAthleteToModel(com.strava.v3.api.Athletes.Athlete athlete)
    {
        Athlete returnAthlete = new Athlete
        {
            Id = athlete.Id,
            FirstName = athlete.FirstName,
            LastName = athlete.LastName,
            ProfileMedium = athlete.ProfileMedium,
            Profile = athlete.Profile,
            City = athlete.City,
            State = athlete.State,
            Country = athlete.Country,
            Sex = athlete.Sex,
            IsPremium = athlete.IsPremium,
            CreatedAt = athlete.CreatedAt,
            UpdatedAt = athlete.UpdatedAt,
            FollowerCount = athlete.FollowerCount,
            FriendCount = athlete.FriendCount,
            DatePreference = athlete.DatePreference,
            MeasurementPreference = athlete.MeasurementPreference,
            Email = athlete.Email,
            Ftp = athlete.Ftp
        };
        return returnAthlete;
    }
}