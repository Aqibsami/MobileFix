using Microsoft.EntityFrameworkCore;
using MobileFix.Models;

namespace MobileFix.Data
{
    public class ApplicationDbContext  : DbContext
    {
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<RepairTicket> RepairTickets { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }

    }
}
