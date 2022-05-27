using Bookinist.DAL.Context;
using Bookinist.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bookinist.DAL.Repositories.RelatedEntityRepositories
{
    internal class BookRepository : DatabaseRepository<Book>
    {
        public override IQueryable<Book> Items => base.Items.Include(item => item.Category);

        public BookRepository(BookinistDb database) : base(database)
        {

        }
    }
}
