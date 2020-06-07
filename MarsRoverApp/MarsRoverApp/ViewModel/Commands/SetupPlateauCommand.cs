using MarsRoverApp.WebService;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarsRoverApp.ViewModel.Commands
{
    public class SetupPlateauCommand : ICommand
    {
        public MarsRoverModel VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public SetupPlateauCommand(MarsRoverModel vm)
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
            VM.Result = await client.SetupPlateauAsync(VM.UpperBoundX, VM.UpperBoundY);

            //var client = DependencyService.Get<IRoverService>();
            //if (client != null)
            //{
            //    var result = await client.SetupPlateau(VM.UpperBoundX, VM.UpperBoundY);
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
