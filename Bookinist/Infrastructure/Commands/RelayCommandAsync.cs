using Bookinist.Infrastructure.Commands.Base;
using System;

namespace Bookinist.Infrastructure.Commands
{
    internal class RelayCommandAsync : Command
    {
        private readonly ActionAsync<object> _execute;
        
        private readonly Func<object, bool> _canExecute;

        public RelayCommandAsync(ActionAsync execute, Func<bool> canExecute = null)
            : this(async p => await execute(), canExecute is null ? 
                (Func<object, bool>)null : p => canExecute())
        {

        }

        public RelayCommandAsync(ActionAsync<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        protected override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? 
                                                                true;

        protected override void Execute(object parameter) => _execute(parameter);
    }
}