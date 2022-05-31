using System;
using System.Windows.Input;
using Bookinist.DAL.Entities;
using Bookinist.Infrastructure.Commands;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Category> _categories;

        private readonly IRepository<Book> _books;

        private readonly IRepository<Seller> _sellers;

        private readonly IRepository<Buyer> _buyers;

        private readonly IRepository<Deal> _deals;

        private readonly ISaleService _saleService;

        private readonly IUserDialog _userDialog;

        #region Title : string - Заголовок окна

        /// <summary>
        /// Заголовок окна
        /// </summary>
        private string _title = "Main Window";

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _title;

            set => Set(ref _title, value);
        }

        #endregion

        #region Status : string - Статус

        /// <summary>
        /// Статус
        /// </summary>
        private string _status = "Ready";

        /// <summary>
        /// Статус
        /// </summary>
        public string Status
        {
            get => _status;

            set => Set(ref _status, value);
        }

        #endregion

        #region CurrentChildViewModel : ViewModel - Текущая дочерняя модель представления

        /// <summary>
        /// Текущая дочерняя модель представления.
        /// </summary>
        private ViewModel _currentChildViewModel;

        /// <summary>
        /// Текущая дочерняя модель представления.
        /// </summary>
        public ViewModel CurrentChildViewModel
    
        {
            get => _currentChildViewModel;

            private set => Set(ref _currentChildViewModel, value);
        }

        #endregion

        #region Commands

        #region Command ShowBookViewCommand - Команда отображения представления книг

        /// <summary>
        /// Команда отображения представления книг.
        /// </summary>
        private ICommand _showBookViewCommand;

        /// <summary>
        /// Команда отображения представления книг.
        /// </summary>
        public ICommand ShowBookViewCommand => _showBookViewCommand ??=
            new RelayCommand(OnShowBookViewCommandExecuted, CanShowBookViewCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Команда отображения представления книг.
        /// </summary>
        private bool CanShowBookViewCommandExecute(object p) => true;

        /// <summary>
        /// Логика выполнения - Команда отображения представления книг.
        /// </summary>
        private void OnShowBookViewCommandExecuted(object p)
        {
            CurrentChildViewModel = new BookViewModel(_books, _categories, _userDialog);
        }

        #endregion

        #region Command ShowBuyerViewCommand - Команда отображения представления покупателей

        /// <summary>
        /// Команда отображения представления покупателей.
        /// </summary>
        private ICommand _showBuyerViewCommand;

        /// <summary>
        /// Команда отображения представления покупателей.
        /// </summary>
        public ICommand ShowBuyerViewCommand => _showBuyerViewCommand ??=
            new RelayCommand(OnShowBuyerViewCommandExecuted, CanShowBuyerViewCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Команда отображения представления покупателей.
        /// </summary>
        private bool CanShowBuyerViewCommandExecute(object p) => true;

        /// <summary>
        /// Логика выполнения - Команда отображения представления покупателей.
        /// </summary>
        private void OnShowBuyerViewCommandExecuted(object p)
        {
            CurrentChildViewModel = new BuyerViewModel(_buyers);
        }

        #endregion

        #region Command ShowStatisticsViewCommand - Команда отображения представления статистики

        /// <summary>
        /// Команда отображения представления статистики.
        /// </summary>
        private ICommand _showStatisticsViewCommand;

        /// <summary>
        /// Команда отображения представления статистики.
        /// </summary>
        public ICommand ShowStatisticsViewCommand => _showStatisticsViewCommand ??=
            new RelayCommand(OnShowStatisticsViewCommandExecuted, CanShowStatisticsViewCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Команда отображения представления статистики.
        /// </summary>
        private bool CanShowStatisticsViewCommandExecute(object p) => true;

        /// <summary>
        /// Логика выполнения - Команда отображения представления статистики.
        /// </summary>
        private void OnShowStatisticsViewCommandExecuted(object p)
        {
            CurrentChildViewModel = new StatisticsViewModel(_books, _buyers, _sellers, _deals);
        }

        #endregion

        #endregion

        [Obsolete]
        public MainWindowViewModel()
        {
            
        }

        public MainWindowViewModel(IRepository<Category> categories, IRepository<Book> books, 
            IRepository<Seller> sellers, IRepository<Buyer> buyers, IRepository<Deal> deals, 
            ISaleService saleService, IUserDialog userDialog)
        {
            _categories = categories;
            _books = books;
            _sellers = sellers;
            _buyers = buyers;
            _deals = deals;
            _saleService = saleService;
            _userDialog = userDialog;
        }
    }
}
