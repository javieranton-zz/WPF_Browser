using System;
using System.Windows.Input;

namespace WPF_Rx_Browser
{
    public class SimpleCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action action;
        public SimpleCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this.action();
        }
    }
}
