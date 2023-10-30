using System;
using DbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DbContext
{
    public class LaborExchangeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<JobArea> JobAreas { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Удалённая Db
            optionsBuilder.UseNpgsql(
                "Host=dpg-cjtcnbfhdsdc739lf3og-a.frankfurt-postgres.render.com;" +
                "Port=5432;" +
                "Database=dashacoursework;" +
                "Username=dashacoursework;" +
                "Password=Ag5vLnFgaLjcC4TfeRK2jxFGz5tXZrOT;" +
                "Pooling = false;" +
                "CommandTimeout = 300;" +
                "Timeout = 300;" +
                "Connection Idle Lifetime = 300;" +
                "Tcp Keepalive = true;");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            //optionsBuilder.UseNpgsql(
            //  "Host=dpg-ck864kfsasqs73d33kfg-a.frankfurt-postgres.render.com;" +
            //  "Port=5432;" +
            //  "Database=dashacoursework_eax4;" +
            //  "Username=dashacoursework_eax4_user;" +
            //  "Password=1BgmXbHAaCa1lI3heiXM8Qnb36XLmT7p;" +
            //  "Pooling = false;" +
            //  "CommandTimeout = 300;" +
            //  "Timeout = 300;" +
            //  "Connection Idle Lifetime = 300;" +
            //  "Tcp Keepalive = true;");
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            //Виртуальная Db
            //optionsBuilder.UseInMemoryDatabase("db");
            //optionsBuilder.UseSqlite("Data Source=LaborExchange.db");

            //Локальная Db
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LaborExchange;Username=postgres;Password=6969");
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

      


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveConversion<UtcValueConverter>();
            configurationBuilder.Properties<DateTime?>().HaveConversion<UtcValueConverter>();
            base.ConfigureConventions(configurationBuilder);
        }

        private class UtcValueConverter : ValueConverter<DateTime, DateTime>
        {
            public UtcValueConverter()
                : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            {
            }
        }
    }
}