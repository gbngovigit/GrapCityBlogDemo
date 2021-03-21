using Application.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, string UserId)> Login(string email, string password);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string phoneNo);
        Task<Result> DeleteUserAsync(string userId);
    }
}
