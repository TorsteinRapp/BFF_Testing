using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;

namespace Host.Controller
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IAccessTokenManagementService _accessTokenManagementService;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IAccessTokenManagementService accessTokenManagementService, IHttpClientFactory httpClientFactory)
        {
            _accessTokenManagementService = accessTokenManagementService;
            _httpClientFactory = httpClientFactory;
        }

        [Route("info")]
        public IActionResult GetUser()
        {
            var user = new
            {
                name = User.Identity.Name,
                claims = User.Claims.Select(c => new {type = c.Type, value = c.Value}).ToArray()
            };

            return new JsonResult(user);
        }

        [Route("infoFromExternal")]
        public async Task<IActionResult> GetTokens()
        {
            var token = await _accessTokenManagementService.GetUserAccessTokenAsync();

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:44356/");
            httpClient.SetBearerToken(token);

            var result = await httpClient.GetAsync("test/identity");

            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<dynamic>(jsonString);
                return new JsonResult(user);
            }

            switch (result.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                case HttpStatusCode.Forbidden:
                    return Forbid();
                default:
                    throw new Exception();
            }
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            return SignOut("cookies", "oidc");
        }
    }
}