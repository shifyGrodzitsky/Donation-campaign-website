using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using ChineseSaleServer.Models;
using System.Data;
using System.Linq.Expressions;

namespace ChineseSaleServer.Middleware
{
    public class BearerTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BearerTokenMiddleware> _logger;

        public BearerTokenMiddleware(RequestDelegate next, IConfiguration configuration,ILogger<BearerTokenMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var identity = context.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var userClaims = identity.Claims;
                var user = new User
                {
       

                Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                Email= userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                Role= userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,

                  

                };

                context.Items["User"] = user;
                await _next(context);

            }
            catch (Exception ex)
            {
               _logger.LogError(ex, "An error occurred in the middleware.");

            }
        }

    }
}
