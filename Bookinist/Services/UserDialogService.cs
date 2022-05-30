using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels;
using Bookinist.Views.Windows;
using System.Windows;

namespace Bookinist.Services
{
    internal class UserDialogService : IUserDialog
    {
        public bool Edit(Book book, IRepository<Category> categoryRepository)
        {
            var bookEditorViewModel = new BookEditorViewModel(book, categoryRepository);

            var bookEditorWindow = new BookEditorWindow
            {
                DataContext = bookEditorViewModel
            };

            if (bookEditorWindow.ShowDialog() != true)
            {
                return false;
            }

            book.Name = bookEditorViewModel.Name;
            book.Category = bookEditorViewModel.Category;

            return true;
        }

        public bool ConfirmQuestion(string information, string caption)
        {
            return MessageBox.Show(information, caption, MessageBoxButton.YesNo, 
                MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public void ConfirmWarning(string information, string caption)
        {
            MessageBox.Show(information, caption, MessageBoxButton.YesNo, 
                MessageBoxImage.Warning);
        }

        public void ConfirmError(string information, string caption)
        {
            MessageBox.Show(information, caption, MessageBoxButton.OK, 
                MessageBoxImage.Error);
        }
    }
}
