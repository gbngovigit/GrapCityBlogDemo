using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infrastructure
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
           // UserId = int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue("userid"));
            
        }

        public int UserId { get; }
        public int OrgId { get; set; }
        public string OrgCode { get; set; }
        public int FinYearNo { get; set; }
        public string FinYearCode { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

    }
}
