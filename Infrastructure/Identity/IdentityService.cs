namespace Infrastructure.Identity
{
    using Application.Common;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public IdentityService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<(Result Result,string UserId)> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                       return  (Result.Success(), user.Id);                                     
            }
            else
            {
                return (Result.Failure(new string[] { "Invalid Credentials"}),"");
            }
             
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password,string phoneNo, byte userType)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
                PhoneNumber = phoneNo,
                Type = userType
            };

            var result = await _userManager.CreateAsync(user, password);
          
            return (result.ToApplicationResult(), user.Id);
        }
        
        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

    }
}
