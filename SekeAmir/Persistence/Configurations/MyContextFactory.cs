using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistence.Configurations;
using Microsoft.Extensions.Configuration.Json;
using System.IO;


namespace Data
{
    public class MyContextFactory : IDesignTimeDbContextFactory<SekeAminContext>
    {
        public SekeAminContext CreateDbContext(string[] args)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(currentDir)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SekeAminContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("Connections"));

            return new SekeAminContext(optionsBuilder.Options);
        }
    }
}
