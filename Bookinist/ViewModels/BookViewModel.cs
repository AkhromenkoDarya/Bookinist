using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    internal class BookViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;

        public BookViewModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
    }
}
