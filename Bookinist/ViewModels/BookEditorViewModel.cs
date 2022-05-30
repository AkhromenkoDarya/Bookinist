using Bookinist.DAL.Entities;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Interfaces;
using Bookinist.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookinist.ViewModels
{
    internal class BookEditorViewModel : ViewModel
    {
        private readonly IRepository<Category> _categories;

        public List<Category> Categories => _categories.Items.ToList();

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

        #region Category : Category - Категория книги

        /// <summary>
        /// Категория книги.
        /// </summary>
        private Category _category;

        /// <summary>
        /// Категория книги.
        /// </summary>
        public Category Category
        {
            get => _category;

            set => Set(ref _category, value);
        }

        #endregion

        public BookEditorViewModel() : this(
            new Book
            {
                Id = 1,
                Name = "Dictionary!",
            }, 
            new DebugCategoryRepository())
        {
            if (!App.IsDesignMode)
            {
                throw new InvalidOperationException("The constructor is not intended for use " +
                                                    "in the Visual Studio designer");
            }
        }

        public BookEditorViewModel(Book book, IRepository<Category> categories)
        {
            _categories = categories;

            Id = book.Id;
            Name = book.Name;
            Category = categories.Items.ToArray()[0];
        }
    }
}
