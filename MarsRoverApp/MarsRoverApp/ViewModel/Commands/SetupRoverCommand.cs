using MarsRoverApp.WebService;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarsRoverApp.ViewModel.Commands
{
    public class SetupRoverCommand : ICommand
    {
        public MarsRoverModel VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public SetupRoverCommand(MarsRoverModel vm)
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
            VM.Result = await client.SetupRoverAsync(VM.InitialPositionX, VM.InitialPositionY, VM.HeadingChar);

            //var client = DependencyService.Get<IRoverService>();
            //if (client != null)
            //{
            //    var result = await client.SetupRover(VM.InitialPositionX, VM.InitialPositionY, VM.HeadingChar);
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
