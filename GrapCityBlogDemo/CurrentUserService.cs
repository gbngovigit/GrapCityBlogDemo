using Application.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GrapCityBlogDemo
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Id) == null ? string.Empty: httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Id).ToString();

        }

        public string UserId { get; }

    }
}
