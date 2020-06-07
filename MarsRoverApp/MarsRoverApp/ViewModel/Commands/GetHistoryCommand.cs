
using MarsRoverApp.WebService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MarsRoverApp.ViewModel.Commands
{
    public class GetHistoryCommand : ICommand
    {
        public MarsRoverModel VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public GetHistoryCommand(MarsRoverModel vm)
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
            var history = await client.GetHistoryAsync();
            if (history != null)
            {
                await VM.Navigation.PushAsync(new HistoryPage
                {
                    BindingContext = history
                });
            }
        }
    }
}
