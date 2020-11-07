using System.Threading;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<SMS> SMSs { get; set; }
        DbSet<Payment> Payments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}