using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bookinist.Infrastructure.DebugServices
{
    internal class DebugBookRepository : IRepository<Book>
    {
        public IQueryable<Book> Items { get; }

        public DebugBookRepository()
        {
            Book[] books = Enumerable.Range(1, 100)
                .Select(i => new Book
                {
                    Id = i,
                    Name = $"Book {i}",
                })
                .ToArray();

            Category[] categories = Enumerable.Range(1, 100)
                .Select(i => new Category()
                {
                    Id = i,
                    Name = $"Category {i}"
                })
                .ToArray();

            foreach (Book book in books)
            {
                Category category = categories[book.Id % categories.Length];
                category.Books.Add(book);
                book.Category = category;
            }

            Items = books.AsQueryable();
        }

        public Book Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Book> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Book Add(Book item)
        {
            throw new System.NotImplementedException();
        }

        public Task<Book> AddAsync(Book item, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Book item)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Book item, CancellationToken cancellationToken = default)
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
