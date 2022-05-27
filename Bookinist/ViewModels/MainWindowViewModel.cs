using System.Linq;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;

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

        public MainWindowViewModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;

            Book[] books = bookRepository.Items.Take(10).ToArray();
        }
    }
}
