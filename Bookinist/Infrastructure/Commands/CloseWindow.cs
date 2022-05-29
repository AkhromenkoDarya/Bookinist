using Bookinist.Infrastructure.Commands.Base;
using System.Windows;

namespace Bookinist.Infrastructure.Commands
{
    internal class CloseWindow : Command
    {
        private static Window GetWindow(object parameter) => parameter as Window ?? 
                                                             App.FocusedWindow ?? App.ActiveWindow;

        protected override bool CanExecute(object parameter) => GetWindow(parameter) != null;

        protected override void Execute(object parameter) => GetWindow(parameter)?.Close();
    }
}
