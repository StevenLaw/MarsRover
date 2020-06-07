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

            //var client = DependencyService.Get<IRoverService>();
            //if (client != null)
            //{
            //    var result = await client.SendCommand(VM.Command);
            //    if (client.Success)
            //    {
            //        VM.Result = result;
            //    }
            //    else
            //    {
            //        VM.Result = client.ErrorMessage;
            //    }
            //}
        }
    }
}
