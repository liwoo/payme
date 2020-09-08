using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class PayMeContext : DbContext
    {
        public PayMeContext(DbContextOptions<PayMeContext> options) : base(options) { }

        public DbSet<SMS> SMSs { get; set; }
    }
}