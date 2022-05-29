using Bookinist.DAL.Entities;

namespace Bookinist.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool Edit(Book book);

        bool ConfirmQuestion(string information, string caption);

        void ConfirmWarning(string information, string caption);

        void ConfirmError(string information, string caption);
    }
}
