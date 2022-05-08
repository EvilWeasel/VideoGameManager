using Microsoft.EntityFrameworkCore;

namespace PflegeboxAPI.DataAccess
{
    public class DataModelContext : DbContext
    {
        public DataModelContext(DbContextOptions<DataModelContext> options) : base(options)
        {

        }

        public DbSet<Adresse> Adressen { get; set; }
        public DbSet<PflegeboxAntrag> PflegeboxAntraege { get; set; }

    }
}
