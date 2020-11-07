using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class PayMeContext : DbContext, IApplicationDbContext
    {
        public PayMeContext(DbContextOptions<PayMeContext> options) : base(options) { }

        public DbSet<SMS> SMSs { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}