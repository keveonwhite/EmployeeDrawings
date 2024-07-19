using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

/// <summary>
/// Adds services and implements methods to use Microsoft Graph SDK.
/// </summary>
internal static class GraphClientExtensions
{
    /// <summary>
    /// Extension method for adding the Microsoft Graph SDK to IServiceCollection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="scopes">The MS Graph scopes to request</param>
    /// <returns></returns>
    public static IServiceCollection AddMicrosoftGraphClient(this IServiceCollection services, params string[] scopes)
    {
        services.Configure<RemoteAuthenticationOptions<MsalProviderOptions>>(options =>
        {
            foreach (var scope in scopes)
            {
                options.ProviderOptions.AdditionalScopesToConsent.Add(scope);
            }
        });

        services.AddScoped<IAuthenticationProvider, GraphAuthenticationProvider>();

        services.AddScoped<GraphServiceClient>(sp =>
        {
            var authProvider = sp.GetRequiredService<IAuthenticationProvider>();
            var httpClient = sp.GetRequiredService<HttpClient>();

            var requestAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient)
            {
                BaseUrl = "https://graph.microsoft.com/v1.0"
            };

            return new GraphServiceClient(requestAdapter);
        });

        return services;
    }

    /// <summary>
    /// Implements IAuthenticationProvider interface.
    /// Tries to get an access token for Microsoft Graph.
    /// </summary>
    public class GraphAuthenticationProvider : Microsoft.Kiota.Abstractions.Authentication.IAuthenticationProvider
    {
        private readonly Microsoft.AspNetCore.Components.WebAssembly.Authentication.IAccessTokenProvider _provider;

        public GraphAuthenticationProvider(Microsoft.AspNetCore.Components.WebAssembly.Authentication.IAccessTokenProvider provider)
        {
            _provider = provider;
        }

        public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            var result = await _provider.RequestAccessToken(new AccessTokenRequestOptions
            {
                Scopes = new[] {
                "https://graph.microsoft.com/User.Read.All",
                "https://graph.microsoft.com/Sites.Read.All"
            }
            });

            if (result.TryGetToken(out var token))
            {
                request.Headers.Add("Authorization", $"Bearer {token.Value}");
            }
        }
    }
}
