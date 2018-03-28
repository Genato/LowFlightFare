using LowFlightFare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LowFlightFare.DbContexts
{
    public class LowFlightFareDbContext : DbContext
    {
        public LowFlightFareDbContext() : base("LowFlightFareConnectionString") { }

        public DbSet<Airport_IATA_Code> AirportIATACodes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<SearchParameters> SearchParameters { get; set; }
        public DbSet<SearchResults> SearchResults { get; set; }
        public DbSet<Locale> Locale { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Airport_IATA_Code>().ToTable("airport_IATA_codes");
            modelBuilder.Entity<Currency>().ToTable("currencies");
            modelBuilder.Entity<SearchParameters>().ToTable("search_parameters");
            modelBuilder.Entity<SearchResults>().ToTable("search_results");

            //modelBuilder.Entity<Locale>().ToTable("Localization").Property(p => p._Localization).HasColumnName("Localization");
        }
    }
}