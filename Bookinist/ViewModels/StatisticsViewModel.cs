using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DAL.Entities;
using Bookinist.Infrastructure.Commands;
using Bookinist.Interfaces;
using Bookinist.Models;
using Bookinist.ViewModels.Base;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.ViewModels
{
    internal class StatisticsViewModel : ViewModel
    {
        private readonly IRepository<Book> _books;

        private readonly IRepository<Buyer> _buyers;

        private readonly IRepository<Seller> _sellers;

        private readonly IRepository<Deal> _deals;

        public ObservableCollection<BestsellerInfo> Bestsellers { get; } = 
            new ObservableCollection<BestsellerInfo>();

        #region Commands

        #region Command ComputeStatisticCommand - Команда для расчета статистических данных.

        /// <summary>
        /// Команда для расчета статистических данных.
        /// </summary>
        private ICommand _computeStatisticsCommand;

        /// <summary>
        /// Команда для расчета статистических данных.
        /// </summary>
        public ICommand ComputeStatisticsCommand => _computeStatisticsCommand ??=
            new RelayCommandAsync(OnComputeStatisticsCommandExecuted, 
                CanComputeStatisticsCommandExecute);
        /// <summary>
        /// Проверка возможности выполнения - Команда для расчета статистических данных.
        /// </summary>
        private bool CanComputeStatisticsCommandExecute() => true;

        /// <summary>Логика выполнения - Команда для расчета статистических данных.
        /// </summary>
        private async Task OnComputeStatisticsCommandExecuted()
        {
            await ComputeDealCountAsync();
        }

        private async Task ComputeDealCountAsync()
        {
            IQueryable<BestsellerInfo> bestsellerQuery = _deals.Items
                .GroupBy(b => b.Book.Id)
                .Select(deals => new
                {
                    BookId = deals.Key,
                    Count = deals.Count(),
                    Sum = deals.Sum(d => d.Price)
                })
                .OrderByDescending(deal => deal.Count)
                .Take(5)
                .Join(_books.Items,
                    deals => deals.BookId,
                    book => book.Id,
                    (deals, book) => new BestsellerInfo
                    {
                        Book = book,
                        SaleCount = deals.Count,
                        SumCost = deals.Sum
                    });

            Bestsellers.AddWithClear(await bestsellerQuery.ToArrayAsync());
        }

        #endregion

        #endregion

        [Obsolete]
        public StatisticsViewModel()
        {

        }

        public StatisticsViewModel(IRepository<Book> books, IRepository<Buyer> buyers, 
            IRepository<Seller> sellers, IRepository<Deal> deals)
        {
            _books = books;
            _buyers = buyers;
            _sellers = sellers;
            _deals = deals;
        }
    }
}
