using MarsRoverApp.WebService;
using System;
using System.Windows.Input;

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
            return VM.CurrentPlateau != null && VM.CurrentRover != null;
        }

        public async void Execute(object parameter)
        {
            var client = new RoverServiceClient();
            VM.Result = await client.MoveAsync(VM.Command);
            VM.CurrentRover.Move(VM.Command, VM.CurrentPlateau);
            VM.Path = VM.CurrentRover.Path;
            VM.Command = "";
            VM.RaiseRedrawCanvas();
        }

        public void RaiseCanExecute()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
