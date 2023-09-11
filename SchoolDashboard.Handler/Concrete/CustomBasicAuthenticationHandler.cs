using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolDasboard.Model;
using SchoolDashboard.Service.Abstract;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SchoolDashboard.Handler.Concrete
{
    public class CustomBasicAuthenticationHandler:AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService userSevice;
        public CustomBasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService _userService) 
            : base(options, logger, encoder, clock)
        {
            userSevice = _userService;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header is missing");
            }
            User user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = System.Text.Encoding.UTF8.GetString(credentialBytes).Split(':');
                var schoolNumber = credentials[0];
                var password = credentials[1];
                user = await userSevice.ValidateUser(schoolNumber, password);
                if (user == null)
                {
                    return AuthenticateResult.Fail("Invalid credentials");
                }
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name,user.UserSchoolNumber),
                    new Claim(ClaimTypes.Role,user.UserRole)
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch(Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
            }
        }
    }
}
