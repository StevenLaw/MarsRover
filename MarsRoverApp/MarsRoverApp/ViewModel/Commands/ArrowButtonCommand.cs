using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarsRoverApp.ViewModel.Commands
{
    public class ArrowButtonCommand : ICommand
    {
        public MarsRoverModel VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public ArrowButtonCommand(MarsRoverModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is string arrow)
            {
                switch (arrow)
                {
                    case "←":
                        VM.Command += "L";
                        break;
                    case "↑":
                        VM.Command += "M";
                        break;
                    case "→":
                        VM.Command += "R";
                        break;
                }
            }
        }
    }
}
