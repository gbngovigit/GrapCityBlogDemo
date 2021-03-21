using Application;
using Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class TokenManager //: ITokenManager
    {
        // private readonly IConfiguration configuration;


        private readonly IOptions<JwtTokenOptions> jwtTokenOptions;
        private readonly IIdentityService _accountService;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtTokenOptions"></param>
        public TokenManager(IIdentityService accountService, IOptions<JwtTokenOptions> jwtTokenOptions)
        {

            this.jwtTokenOptions = jwtTokenOptions;
            this._accountService = accountService;
        }


        #region Authentication

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        
        public async Task<string> GenarateToken(string username)
        {
            string tokenString = string.Empty;
            var user = new ApplicationUser();//await _accountService.GetByUserName(username);
            tokenString = GetToken(user);
            return tokenString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userClaims"></param>
        /// <param name="assignedRoles"></param>
        /// <returns></returns>
        private string GetToken(ApplicationUser user)
        {

            var utcNow = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>()
            {

                        new Claim("userid", user.Id.ToString()),
                     
                        //new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),

            };


            //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
            //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtTokenOptions.Value.SigningKey /*this.configuration["Tokens:Key"]*/));
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(this.jwtTokenOptions.Value.ExpireDays));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(5));

            var token = new JwtSecurityToken(
                this.jwtTokenOptions.Value.Issuer,
                this.jwtTokenOptions.Value.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ApplicationUser GetUserByToken(string token)
        {
            try
            {

                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var UserId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
               
                var emailId = tokenS.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value;
                //User user =  _accountService.GetByUserName("");
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        #endregion
    }
}
