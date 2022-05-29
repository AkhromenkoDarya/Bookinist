using Bookinist.Infrastructure.Commands.Base;
using System;
using System.Windows;

namespace Bookinist.Infrastructure.Commands
{
    internal class DialogResultCommand : Command
    {
        public bool? DialogResult { get; set; }

        protected override bool CanExecute(object parameter) => App.CurrentWindow != null;

        protected override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            Window window = App.CurrentWindow;

            bool? dialogResult = DialogResult;

            if (parameter != null)
            {
                dialogResult = Convert.ToBoolean(parameter);
            }

            window.DialogResult = dialogResult;
            window.Close();
        }
    }
}
