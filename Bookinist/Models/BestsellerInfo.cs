using Bookinist.DAL.Entities;

namespace Bookinist.Models
{
    internal class BestsellerInfo
    {
        public Book Book { get; set; }

        public int SaleCount { get; set; }

        public decimal SumCost { get; set; }
    }
}
