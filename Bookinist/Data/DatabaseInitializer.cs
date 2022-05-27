using Bookinist.DAL.Context;
using Bookinist.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookinist.Data
{
    internal class DatabaseInitializer
    {
        private readonly BookinistDb _database;

        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(BookinistDb database, ILogger<DatabaseInitializer> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initializing a database...");

            _logger.LogInformation("Deleting an existing database...");
            await _database.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation("Deleting an existing database completed successfully in " +
                                   $"{timer.ElapsedMilliseconds} ms");

            //_database.Database.EnsureCreated();

            _logger.LogInformation("Database migration...");
            await _database.Database.MigrateAsync().ConfigureAwait(false);
            _logger.LogInformation("Database migration completed successfully in " +
                                   $"{timer.ElapsedMilliseconds} ms");

            if (await EntityFrameworkQueryableExtensions.AnyAsync(_database.Books))
            {
                return;
            }

            await InitializeCategories();
            await InitializeBooks();
            await InitializeSellers();
            await InitializeBuyers();
            await InitializeDeals();

            _logger.LogInformation("Database initialization completed successfully in " +
                                   $"{timer.Elapsed.TotalSeconds} ms");
        }

        private const int CategoryCount = 10;

        private Category[] _categories;

        private async Task InitializeCategories()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialization of categories...");

            _categories = new Category[CategoryCount];

            for (var i = 0; i < CategoryCount; i++)
            {
                _categories[i] = new Category
                {
                    Name = $"Category {i + 1}"
                };
            }

            await _database.Categories.AddRangeAsync(_categories);
            await _database.SaveChangesAsync();

            _logger.LogInformation("Initialization of categories completed successfully in " +
                                   $"{timer.ElapsedMilliseconds} ms");
        }

        private const int BookCount = 10;

        private Book[] _books;

        private async Task InitializeBooks()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialization of books...");

            var random = new Random();

            _books = Enumerable.Range(1, BookCount)
                .Select(i => new Book()
                {
                    Name = $"Book {i}",
                    Category = random.NextItem(_categories)
                })
                .ToArray();

            await _database.Books.AddRangeAsync(_books);
            await _database.SaveChangesAsync();

            _logger.LogInformation("Initialization of books completed successfully in " +
                                   $"{timer.ElapsedMilliseconds} ms");
        }

        private const int SellerCount = 10;

        private Seller[] _sellers;

        private async Task InitializeSellers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialization of sellers...");

            _sellers = Enumerable.Range(1, SellerCount)
                .Select(i => new Seller()
                {
                    Name = $"Seller-Name {i}",
                    Surname = $"Seller-Surname {i}",
                    Patronymic = $"Seller-Patronymic {i}"
                })
                .ToArray();

            await _database.Sellers.AddRangeAsync(_sellers);
            await _database.SaveChangesAsync();

            _logger.LogInformation("Initialization of sellers completed successfully in " +
                                   $"{timer.ElapsedMilliseconds} ms");
        }

        private const int BuyerCount = 10;

        private Buyer[] _buyers;

        private async Task InitializeBuyers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialization of buyers...");

            _buyers = Enumerable.Range(1, BuyerCount)
                .Select(i => new Buyer()
                {
                    Name = $"Buyer-Name {i}",
                    Surname = $"Buyer-Surname {i}",
                    Patronymic = $"Buyer-Patronymic {i}"
                })
                .ToArray();

            await _database.Buyers.AddRangeAsync(_buyers);
            await _database.SaveChangesAsync();

            _logger.LogInformation("Initialization of buyers completed successfully in " +
                                   $"{timer.ElapsedMilliseconds} ms");
        }

        private const int DealCount = 1000;

        private async Task InitializeDeals()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initialization of deals...");

            var random = new Random();

            IEnumerable<Deal> deals = Enumerable.Range(1, DealCount)
                .Select(i => new Deal
                {
                    Books = new List<Book>
                    {
                        random.NextItem(_books)
                    },
                    Seller = random.NextItem(_sellers),
                    Buyer = random.NextItem(_buyers),
                    Price = (decimal)(random.NextDouble() * 4000 + 700)
                });

            await _database.Deals.AddRangeAsync(deals);
            await _database.SaveChangesAsync();

            _logger.LogInformation("Initialization of deals completed successfully in " +
                                   $"{timer.ElapsedMilliseconds} ms");
        }
    }
}
