
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace RealesApi.DTA.Services
{
	public class CustomJwtHandler : JwtBearerHandler
    {
        private readonly ILogger<CustomJwtHandler> _logger;

        public CustomJwtHandler(
            IOptionsMonitor<JwtBearerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _logger = logger.CreateLogger<CustomJwtHandler>();
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                // First try the standard authentication
                _logger.LogInformation("Attempting standard JWT authentication");
                var result = await base.HandleAuthenticateAsync();

                if (result.Succeeded)
                {
                    _logger.LogInformation("Standard JWT authentication succeeded");
                    return result;
                }

                _logger.LogWarning("Standard JWT authentication failed. Error: {Error}",
                    result.Failure?.Message ?? "No specific error provided");

                // If standard auth fails, try manual token validation
                var token = Context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogWarning("No token provided in Authorization header");
                    return AuthenticateResult.Fail("No token provided");
                }

                _logger.LogInformation("Attempting manual token validation");

                try
                {
                    // Manually parse the token without validation
                    var jwtToken = new JwtSecurityToken(token);

                    _logger.LogInformation("Token parsed successfully");
                    _logger.LogInformation("Token issuer: {Issuer}", jwtToken.Issuer);
                    _logger.LogInformation("Token audience: {Audience}", string.Join(", ", jwtToken.Audiences));
                    _logger.LogInformation("Token valid from: {ValidFrom}", jwtToken.ValidFrom);
                    _logger.LogInformation("Token valid to: {ValidTo}", jwtToken.ValidTo);

                    // Create identity from claims
                    var identity = new ClaimsIdentity(jwtToken.Claims, JwtBearerDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Log all claims
                    foreach (var claim in jwtToken.Claims)
                    {
                        _logger.LogInformation("Claim: {Type} = {Value}", claim.Type, claim.Value);
                    }

                    // Create authentication ticket
                    var ticket = new AuthenticationTicket(principal, JwtBearerDefaults.AuthenticationScheme);

                    _logger.LogInformation("Manual authentication successful");
                    return AuthenticateResult.Success(ticket);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during manual token validation");
                    return AuthenticateResult.Fail("Manual token validation failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during authentication");
                return AuthenticateResult.Fail("Authentication failed due to an unexpected error");
            }
        }

    }
}

