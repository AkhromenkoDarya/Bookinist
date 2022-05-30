using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

namespace Bookinist.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool Edit(Book book, IRepository<Category> categoryRepository);

        bool ConfirmQuestion(string information, string caption);

        void ConfirmWarning(string information, string caption);

        void ConfirmError(string information, string caption);
    }
}
