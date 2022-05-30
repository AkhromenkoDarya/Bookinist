using Bookinist.DAL.Entities;
using Bookinist.Infrastructure.Commands;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Interfaces;
using Bookinist.Services;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Bookinist.ViewModels
{
    internal class BookViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;

        private readonly IRepository<Category> _categoryRepository;

        private readonly IUserDialog _userDialog;

        #region Books : ObservableCollection<Book> - Коллекция книг

        /// <summary>
        /// Коллекция книг.
        /// </summary>
        private ObservableCollection<Book> _books;

        /// <summary>
        /// Коллекция книг.
        /// </summary>
        public ObservableCollection<Book> Books
        {
            get => _books;

            set
            {
                if (!Set(ref _books, value))
                {
                    return;
                }

                _bookViewSource.Source = value;
                OnPropertyChanged(nameof(BookView));
            }
        }

        #endregion

        #region SelectedBook : Book - Выбранная книга

        /// <summary>
        /// Выбранная книга.
        /// </summary>
        private Book _selectedBook;

        /// <summary>
        /// Выбранная книга.
        /// </summary>
        public Book SelectedBook
        {
            get => _selectedBook;

            set => Set(ref _selectedBook, value);
        }

        #endregion

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

        #region Commands

        #region Command LoadDataFromRepositoryCommand - Команда загрузки данных из репозитория

        /// <summary>
        /// Команда загрузки данных из репозитория.
        /// </summary>
        private ICommand _loadDataFromRepositoryCommand;

        /// <summary>
        /// Команда загрузки данных из репозитория.
        /// </summary>
        public ICommand LoadDataFromRepositoryCommand => _loadDataFromRepositoryCommand ??=
            new RelayCommandAsync(OnLoadDataFromRepositoryCommandExecuted, 
                CanLoadDataFromRepositoryCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Команда загрузки данных из репозитория.
        /// </summary>
        private bool CanLoadDataFromRepositoryCommandExecute() => true;

        /// <summary>
        /// Логика выполнения - Команда загрузки данных из репозитория.
        /// </summary>
        private async Task OnLoadDataFromRepositoryCommandExecuted()
        {
            //Books = new ObservableCollection<Book>(await _bookRepository.Items.ToArrayAsync());
            Books = (await _bookRepository.Items.ToArrayAsync()).ToObservableCollection();
        }

        #endregion

        #region Command AddBookCommand - Команда добавления новой книги

        /// <summary>
        /// Команда добавления новой книги.
        /// </summary>
        private ICommand _addBookCommand;

        /// <summary>
        /// Команда добавления новой книги.
        /// </summary>
        public ICommand AddBookCommand => _addBookCommand ??=
            new RelayCommand(OnAddBookCommandExecuted, CanAddBookCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Команда добавления новой книги.
        /// </summary>
        private bool CanAddBookCommandExecute() => true;

        /// <summary>
        /// Логика выполнения - Команда добавления новой книги.
        /// </summary>
        private void OnAddBookCommandExecuted()
        {
            var newBook = new Book();

            if (!_userDialog.Edit(newBook, _categoryRepository))
            {
                return;
            }

            _books.Add(_bookRepository.Add(newBook));
            SelectedBook = newBook;
        }

        #endregion

        #region Command RemoveBookCommand - Команда удаления указанной книги

        /// <summary>
        /// Команда удаления указанной книги.
        /// </summary>
        private ICommand _removeBookCommand;

        /// <summary>
        /// Команда удаления указанной книги.
        /// </summary>
        public ICommand RemoveBookCommand => _removeBookCommand ??=
            new RelayCommand(OnRemoveBookCommandExecuted, CanRemoveBookCommandExecute);

        /// <summary>
        /// Проверка возможности выполнения - Команда удаления указанной книги.
        /// </summary>
        private bool CanRemoveBookCommandExecute(object p) => p != null || SelectedBook != null;

        /// <summary>
        /// Логика выполнения - Команда удаления указанной книги.
        /// </summary>
        private void OnRemoveBookCommandExecuted(object p)
        {
            var bookToRemove = (Book)(p ?? SelectedBook);

            if (!_userDialog.ConfirmQuestion("Are you sure you want to remove the book " +
                                             $"{bookToRemove.Name}?", $"Removing the book {bookToRemove.Name}"))
            {
                return;
            }

            _bookRepository.Remove(bookToRemove.Id);
            Books.Remove(bookToRemove);

            if (ReferenceEquals(SelectedBook, bookToRemove))
            {
                SelectedBook = null;
            }
        }

        #endregion

        #endregion

        public BookViewModel(): this(new DebugBookRepository(), new DebugCategoryRepository(), 
            new UserDialogService())
        {
            if (!App.IsDesignMode)
            {
                throw new InvalidOperationException("The constructor is not intended for use " +
                                                    "in the Visual Studio designer");
            }

            _ = OnLoadDataFromRepositoryCommandExecuted();
        }

        public BookViewModel(IRepository<Book> bookRepository, 
            IRepository<Category> categoryRepository, IUserDialog userDialog)
        {
            _bookRepository = bookRepository;
            _userDialog = userDialog;
            _categoryRepository = categoryRepository;

            _bookViewSource = new CollectionViewSource
            {
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
