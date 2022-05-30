using Bookinist.DAL.Context;
using Bookinist.DAL.Services.Registration;
using Bookinist.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bookinist.Services.Registration
{
    internal static class DatabaseRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            string provider = configuration.GetValue("Provider", "MSSQL");

            return services.AddDbContext<BookinistDb>(
                        options => _ = provider switch
                        {
                            "MSSQL" => options.UseSqlServer(configuration
                                    .GetConnectionString("MSSQL"),
                                x => x.MigrationsAssembly("MSSQLMigrations")),

                            "SQLite" => options.UseSqlite(configuration
                                    .GetConnectionString("SQLite"),
                                x => x.MigrationsAssembly("SQLiteMigrations")),

                            "InMemory" => options.UseInMemoryDatabase("Bookinist.db"),

                            null => throw new ArgumentNullException(nameof(provider),
                                "The provider is not defined"),

                            _ => throw new NotSupportedException(
                                $"Unsupported provider: {provider}")
                        })
                    .AddTransient<DatabaseInitializer>()
                    .AddRepositoriesInDatabase()
                ;
        }
    }
}
