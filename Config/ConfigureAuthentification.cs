
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Abyster_Test_Project.Config;

public static class ConfigureAuthentification
{
    // TODO private static ILoggerManager _logger = new LoggerManager();

    public static void ConfigureJWT(this IServiceCollection service, JwtSettings jwtSettings)
    {
        // TODO service.AddTransient<IClaimsTransformation, ClaimTransformer>();
        var AuthentificationBuilder = service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        Console.WriteLine("JWTSETTINGS :"+jwtSettings.ToString());
        AuthentificationBuilder.AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidAudience = jwtSettings.validAudience,
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.validIssuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.secret)),
                ValidateLifetime = true,

            };

            options.Events = new JwtBearerEvents()
            {

                OnForbidden = state =>
                {
                    // _logger.LogInfo("User not Authentifified and forbiden occur");
                    Console.WriteLine("User not Authentifified and forbiden occur");
                    state.Response.StatusCode = StatusCodes.Status403Forbidden;
                    state.Response.ContentType = "text/plain";
                    return state.Response.WriteAsync("An error occured processing your authentication.");
                },

                OnTokenValidated = state =>
                {

                    // _logger.LogInfo("User successfully authenticated");
                    Console.WriteLine("User successfully authenticated");
                    
                    return Task.CompletedTask;
                },

                OnAuthenticationFailed = state =>
                {
                    state.NoResult();

                    state.Response.StatusCode = 500;
                    state.Response.ContentType = "text/plain";
                    return state.Response.WriteAsync("An error occured processing your authentication.");
                },

                OnChallenge = state =>
                {
                    try
                    {
                        // _logger.LogWarm("User not Authentifified ");
                        state.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        state.Response.ContentType = "text/plain";
                        return state.Response.WriteAsync("Unauthorize user request");
                    }
                    catch (Exception exception)
                    {
                        // _logger.LogWarm("User not Authentifified ");
                        state.HandleResponse();
                        state.Error = string.Format("UnAuthorize User Request {0}", StatusCodes.Status400BadRequest);
                        return Task.FromResult(0);
                    }

                }
            };

        });

    }

   
}