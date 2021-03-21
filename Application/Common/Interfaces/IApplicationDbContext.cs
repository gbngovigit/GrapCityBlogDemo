namespace Application.Interfaces
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    public interface IApplicationDbContext
    {
        DbSet<Article> Articles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
