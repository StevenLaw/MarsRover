using MarsRoverApp.ViewModel;
using MarsRoverApp.WebService;
using SkiaSharp;
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
            {
                model.Navigation = Navigation;
                model.RedrawCanvas += (object sender, EventArgs e) =>
                {
                    canvas.InvalidateSurface();
                };
            }
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

        private void canvas_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            if (BindingContext is MarsRoverModel model)
            {
                if (model.UpperBoundX > 0 && model.UpperBoundY > 0)
                {
                    SKImageInfo info = e.Info;
                    SKSurface surface = e.Surface;
                    SKCanvas canvas = surface.Canvas;

                    SKPaint gridLinePaint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Gray,
                        StrokeWidth = 2
                    };

                    for (int i = 0; i <= model.UpperBoundX; i++)
                    {
                        canvas.DrawLine(i / info.Height, 0, i / info.Height, info.Width, gridLinePaint);
                    }

                    for (int i = 0; i <= model.UpperBoundY; i++)
                    {
                        canvas.DrawLine(0, i / info.Width, info.Height, i / info.Width, gridLinePaint);
                    }
                }
            }
        }
    }
}
