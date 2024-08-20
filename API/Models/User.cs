namespace API.Models;

public class User
{
    public int Id { get; set; }
    public long AthleteId {get;set;} = 0;
    public required string Name { get; set; } = "";
    public required string AccessToken { get; set; } = "";
    public required string RefreshToken { get; set; } = "";
    public required string AccessCode { get; set; } = "";
    public required long ExpiresAtEpoch { get; set; } = 0;
    public required string Scope { get; set; } = "";
}