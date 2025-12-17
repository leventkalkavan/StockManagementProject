using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace DataAccessLayer.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var appSettingsFile = Path.Combine(basePath, "appsettings.json");

            if (!File.Exists(appSettingsFile))
            {
                var webUiPath = Path.Combine(basePath, "ApplicationWebUI");
                if (!File.Exists(Path.Combine(webUiPath, "appsettings.json")))
                {
                    var parent = Directory.GetParent(basePath)?.FullName;
                    if (parent is not null)
                    {
                        webUiPath = Path.Combine(parent, "ApplicationWebUI");
                    }
                }

                if (File.Exists(Path.Combine(webUiPath, "appsettings.json")))
                {
                    basePath = webUiPath;
                }
            }

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
