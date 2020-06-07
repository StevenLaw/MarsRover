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
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    var client = DependencyService.Get<IRoverService>();
        //    if (client != null)
        //    {
        //        var result = await client.SendCommand(txtComand.Text);
        //        if (client.Success)
        //        {
        //            lblResult.Text = result;
        //        }
        //        else
        //        {
        //            lblResult.Text = client.ErrorMessage;
        //        }
        //    }
        //}
    }
}
