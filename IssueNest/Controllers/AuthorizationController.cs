using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using dotenv.net.Interfaces;
using IssueNest.Controllers.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace IssueNest.Controllers
{
    [Route("api/oauth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IHttpClientFactory _clientFactory;
        private IEnvReader _env;

        public AuthorizationController(IHttpClientFactory clientFactory, IEnvReader env)
        {
            _clientFactory = clientFactory;
            _env = env;
        }

        [HttpPost("github")]
        public async Task<IActionResult> OAuth([FromBody] GithubOAuth payload)
        {
            string client_id = _env.GetStringValue("client_id");
            string client_secret = _env.GetStringValue("client_secret");

            // Initial request for token
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, 
                $"https://github.com/login/oauth/access_token?client_id={client_id}&client_secret={client_secret}&code={payload.Code}");
            request.Headers.Add("Accept", "application/json");
            
            HttpClient client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                // Extract token from response body
                JsonDocument document = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
                string token = document.RootElement.GetProperty("access_token").GetString();

                // Request user data using the token
                request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user");
                request.Headers.Add("Authorization", $"token {token}");
                request.Headers.Add("User-Agent", "IssueNest");
                response = await client.SendAsync(request);

                // Read response
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                return Ok();
            }

            return Unauthorized();
        }
    }
}
