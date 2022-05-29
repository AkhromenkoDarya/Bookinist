using Bookinist.DAL.Entities;
using Bookinist.ViewModels.Base;
using System;

namespace Bookinist.ViewModels
{
    internal class BookEditorViewModel : ViewModel
    {
        public int Id { get; }

        #region Name : string - Название книги

        /// <summary>
        /// Название книги.
        /// </summary>
        private string _name;

        /// <summary>
        /// Название книги.
        /// </summary>
        public string Name
        {
            get => _name;

            set => Set(ref _name, value);
        }

        #endregion

        public BookEditorViewModel() : this(
            new Book
            {
                Id = 1,
                Name = "Dictionary!"
            })
        {
            if (!App.IsDesignMode)
            {
                throw new InvalidOperationException("The constructor is not intended for use " +
                                                    "in the Visual Studio designer");
            }
        }

        public BookEditorViewModel(Book book)
        {
            Id = book.Id;
            Name = book.Name;
        }
    }
}
