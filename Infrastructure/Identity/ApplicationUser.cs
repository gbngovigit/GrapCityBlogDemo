


namespace Infrastructure.Identity
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class ApplicationUser : IdentityUser
    {
      
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
