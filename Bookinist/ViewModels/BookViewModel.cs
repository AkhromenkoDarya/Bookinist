using Bookinist.DAL.Entities;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Interfaces;
using Bookinist.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace Bookinist.ViewModels
{
    internal class BookViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;

        public IEnumerable<Book> Books => _bookRepository.Items;

        #region BookFilterKeyword : string - Ключевое слово для фильтрации книг

        /// <summary>
        /// Ключевое слово для фильтрации книг.
        /// </summary>
        private string _bookFilterKeyword;

        /// <summary>
        /// Ключевое слово для фильтрации книг.
        /// </summary>
        public string BookFilterKeyword
        {
            get => _bookFilterKeyword;

            set
            {
                if (Set(ref _bookFilterKeyword, value))
                {
                    _bookViewSource.View.Refresh();
                }
            }
        }

        #endregion

        #region BookViewSource

        private readonly CollectionViewSource _bookViewSource;

        public ICollectionView BookView => _bookViewSource.View;

        #endregion

        public BookViewModel() : this(new DebugBookRepository())
        {
            if (!App.IsDesignMode)
            {
                throw new InvalidOperationException("The constructor is not intended for use " +
                                                    "in the Visual Studio designer");
            }
        }

        public BookViewModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;

            _bookViewSource = new CollectionViewSource
            {
                Source = _bookRepository.Items.ToArray(),
                SortDescriptions =
                {
                    new SortDescription(nameof(Book.Name), ListSortDirection.Ascending)
                }
            };

            _bookViewSource.Filter += OnBookFilter;
        }

        private void OnBookFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Book book) || string.IsNullOrWhiteSpace(_bookFilterKeyword))
            {
                return;
            }

            if (!book.Name.Contains(_bookFilterKeyword, StringComparison.OrdinalIgnoreCase))
            {
                e.Accepted = false;
            }
        }
    }
}
