using Bookinist.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookinist.Services.Interfaces
{
    internal interface ISaleService
    {
        IEnumerable<Deal> Deals { get; }

        Task<Deal> MakeDeal(string bookName, Seller seller, Buyer buyer, decimal price);
    }
}
