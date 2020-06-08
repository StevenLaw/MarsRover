using MarsRoverApp.WebService;
using System;
using System.Windows.Input;

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
            VM.CurrentPlateau = new MarsRoverApiModel.Plateau(VM.UpperBoundX, VM.UpperBoundY);
            VM.Path = null;
        }
    }
}
