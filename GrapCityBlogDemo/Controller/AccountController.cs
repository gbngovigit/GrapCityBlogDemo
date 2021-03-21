using Application;
using Application.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GrapCityBlogDemo.Controller
{
    public class AccountController : ApiController
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IIdentityService _identityService;
        public AccountController(ILogger<AccountController> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;

        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginCommand request)
        {
            var user = await _identityService.Login(request.Email, request.Password);
            if (user.Result.Succeeded)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var userClaims = new List<Claim>() {
                    new Claim(JwtClaimTypes.Email, request.Email),
                     new Claim(JwtClaimTypes.Id,user.UserId.ToString())
                };
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost",
                    audience: "https://localhost",
                    claims: userClaims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new
                {
                    IsSuccess = true,
                    EmailId = request.Email,
                    Token = tokenString,
                    RefreshToken = string.Empty
                });

            }
            else
            {
                return Unauthorized(new { IsSuccess = false, errors = user.Result.Errors });
            }
        }
    }
}
