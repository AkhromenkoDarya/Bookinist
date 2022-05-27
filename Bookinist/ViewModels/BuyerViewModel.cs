using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    internal class BuyerViewModel : ViewModel
    {
        private readonly IRepository<Buyer> _buyerRepository;

        public BuyerViewModel(IRepository<Buyer> buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }
    }
}
