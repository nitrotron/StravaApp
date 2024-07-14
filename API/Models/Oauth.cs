using Microsoft.IdentityModel.Tokens;

namespace API.Models;

public class Oauth
{
public int Id {get;set;}
public required int ClientID { get; set; } = 0;
public required string ClientSecret { get; set; } ="";
public required string AccessToken { get; set; } = "";
public required string RefreshToken {get;set;} ="";
public required string AccessCode { get; set; } = "";
public required long ExpiresAtEpoch { get; set; } = 0;
public required string Scope { get; set; } = "";
public required string Salt {get;set;}="";
}
