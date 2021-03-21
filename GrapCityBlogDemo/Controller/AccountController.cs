using Application;
using Application.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        [AllowAnonymous]
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
                     new Claim(JwtClaimTypes.Id,request.Email)
                };
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost",
                    audience: "https://localhost",
                    claims: userClaims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                //logic for token managment + refresh Token here
                return Ok(new
                {
                    IsSuccess = true,
                    EmailId = request.Email,
                    Token = tokenString,
                    RefreshToken = GenerateRefreshToken()
                });

            }
            else
            {
                return Unauthorized(new { IsSuccess = false, errors = user.Result.Errors });
            }
        }
        //Add Other methods
        //logout, refresh token,ressetpassord etc.
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }

    
}
