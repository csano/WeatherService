using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication1
{
    public class SimpleAsyncCommand : ICommand
    {
        public Func<Task> AsyncOperation { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await AsyncOperation();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
