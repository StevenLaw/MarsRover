using MarsRoverApp.ViewModel;
using MarsRoverApp.WebService;
using System;
using System.ComponentModel;
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
    }
}
