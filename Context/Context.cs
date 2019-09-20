using esoft.Entity;
using System.Data.Entity;

namespace esoft
{
    public class Context : DbContext
    {
        public Context() : base("DbConnection") { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<DemandFilter> DemandFilters { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<Estate> Estates { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Deal> Deals { get; set; }
    }
}
