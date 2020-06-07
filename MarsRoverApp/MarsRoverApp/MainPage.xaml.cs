using MarsRoverApp.ViewModel;
using MarsRoverApp.WebService;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MarsRoverApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (BindingContext is MarsRoverModel model)
                model.Navigation = Navigation;
        }

        private async void Entry_Focused(object sender, FocusEventArgs e)
        {
            await Task.Delay(100);
            if (sender is Entry entry)
            {
                entry.CursorPosition = 0;
                entry.SelectionLength = entry.Text.Length;
            }
        }
    }
}
