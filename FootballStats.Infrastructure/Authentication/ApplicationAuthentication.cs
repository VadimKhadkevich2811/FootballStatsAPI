using System.Text;
using FootballStats.ApplicationModule.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FootballStats.Infrastructure.Authentication;

public class ApplicationAuthentication : IAuthentication
{
    public async Task<string?> GetAuthenticationTokenAsync()
    {
        var config = new ConfigurationBuilder().AddUserSecrets<ApplicationAuthentication>().Build();
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri($"{config["FootballStatsAuthentication:Endpoint"]}"),
        };

        using StringContent jsonContent = new(
        JsonConvert.SerializeObject(new
        {
            client_id = config["FootballStatsAuthentication:ClientId"],
            client_secret = config["FootballStatsAuthentication:ClientSecret"],
            audience = config["FootballStatsAuthentication:Audience"],
            grant_type = "client_credentials"
        }),
        Encoding.UTF8,
        "application/json");

        using HttpResponseMessage response = await httpClient.PostAsync("", jsonContent);
        var jsonStringResponse = await response.Content.ReadAsStringAsync();
        var authenticationModel = JsonConvert.DeserializeObject<AuthenticationModel>(jsonStringResponse);

        return authenticationModel!.AccessToken ?? null;
    }
}