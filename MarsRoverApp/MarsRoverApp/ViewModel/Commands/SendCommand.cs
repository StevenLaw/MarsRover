using MarsRoverApp.WebService;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarsRoverApp.ViewModel.Commands
{
    public class SendCommand : ICommand
    {
        public MarsRoverModel VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public SendCommand(MarsRoverModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var client = new RoverServiceClient();
            VM.Result = await client.MoveAsync(VM.Command);
        }
    }
}
