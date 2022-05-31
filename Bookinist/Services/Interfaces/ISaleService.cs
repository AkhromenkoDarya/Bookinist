using System.Collections.Generic;
using System.Threading.Tasks;
using Bookinist.DAL.Entities;

namespace Bookinist.Services.Interfaces
{
    internal interface ISaleService
    {
        IEnumerable<Deal> Deals { get; }

        Task<Deal> MakeDeal(string bookName, Seller seller, Buyer buyer, decimal price);
    }
}
