using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    internal class StatisticViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;

        private readonly IRepository<Buyer> _buyerRepository;

        private readonly IRepository<Seller> _sellerRepository;

        public StatisticViewModel(IRepository<Book> bookRepository, 
            IRepository<Buyer> buyerRepository, IRepository<Seller> sellerRepository)
        {
            _bookRepository = bookRepository;
            _buyerRepository = buyerRepository;
            _sellerRepository = sellerRepository;
        }
    }
}
