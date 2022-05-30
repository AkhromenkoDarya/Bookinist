using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bookinist.Infrastructure.DebugServices
{
    internal class DebugCategoryRepository : IRepository<Category>
    {
        public IQueryable<Category> Items { get; }

        public DebugCategoryRepository()
        {
            Category[] categories = Enumerable.Range(1, 100)
                .Select(i => new Category()
                {
                    Id = i,
                    Name = $"Category {i}"
                })
                .ToArray();

            Book[] books = Enumerable.Range(1, 100)
                .Select(i => new Book
                {
                    Id = i,
                    Name = $"Book {i}",
                })
                .ToArray();

            foreach (Category category in categories)
            {
                Book book = books[category.Id % books.Length];
                book.Category = category;
                category.Books.Add(book);
            }

            Items = categories.AsQueryable();
        }

        public Category Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Category Add(Category item)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> AddAsync(Category item, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Category item)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Category item, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
