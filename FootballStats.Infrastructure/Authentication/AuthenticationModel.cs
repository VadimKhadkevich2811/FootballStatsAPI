using Newtonsoft.Json;

namespace FootballStats.Infrastructure.Authentication;

public class AuthenticationModel
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonProperty("token_type")]
    public string? TokenType { get; set; }
}