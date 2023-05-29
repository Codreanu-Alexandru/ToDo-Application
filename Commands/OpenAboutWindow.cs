using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema_2_MVP.Views;

namespace Tema_2_MVP.Commands
{
    public class OpenAboutWindow : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return (parameter as AboutCommandParamters) != null;
        }

        public void Execute(object parameter)
        {
            AboutWindow aboutWindow = new AboutWindow(parameter as AboutCommandParamters);
            aboutWindow.Show();
        }
    }
}
