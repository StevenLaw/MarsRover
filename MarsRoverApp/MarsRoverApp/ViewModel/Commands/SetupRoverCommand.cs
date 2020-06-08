using MarsRoverApp.WebService;
using System;
using System.Windows.Input;

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
            VM.CurrentRover = new MarsRoverApiModel.Rover(VM.InitialPositionX, VM.InitialPositionY, VM.HeadingChar);
        }
    }
}
