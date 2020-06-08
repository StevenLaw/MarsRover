using MarsRoverApp.ViewModel;
using SkiaSharp;
using System;
using System.ComponentModel;
using System.IO;
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

        private void Canvas_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            if (BindingContext is MarsRoverModel model)
            {
                if (model.CurrentPlateau != null &&
                    model.CurrentRover != null)
                {
                    SKImageInfo info = e.Info;
                    SKSurface surface = e.Surface;
                    SKCanvas canvas = surface.Canvas;

                    canvas.Clear();

                    SKPaint gridLinePaint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.LightGray,
                        StrokeWidth = 2
                    };

                    SKPaint startPointPaint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Green,
                        StrokeWidth = 8
                    };

                    SKPaint endPointPaint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Red,
                        StrokeWidth = 8
                    };

                    SKPaint pathLinePaint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Black,
                        StrokeWidth = 2
                    };

                    float xBreak = (float)info.Width / model.CurrentPlateau.UpperX;
                    for (int i = 0; i <= model.CurrentPlateau.UpperX; i++)
                    {
                        canvas.DrawLine(xBreak * i, 0, xBreak * i, info.Width, gridLinePaint);
                    }

                    float yBreak = (float)info.Height / model.CurrentPlateau.UpperY;
                    for (int i = 0; i <= model.CurrentPlateau.UpperY; i++)
                    {
                        canvas.DrawLine(0, yBreak * i, info.Height, yBreak * i, gridLinePaint);
                    }

                    if (model.Path != null)
                    {
                        Tuple<float, float> lastPos = null;
                        foreach (var pos in model.Path)
                        {
                            float posX = pos.Item1 * xBreak;
                            float posY = pos.Item2 * yBreak;
                            if (lastPos == null)
                            {
                                canvas.DrawPoint(posX, posY, startPointPaint);
                            }
                            else
                            {
                                canvas.DrawLine(lastPos.Item1, lastPos.Item2, posX, posY, pathLinePaint);
                            }
                            lastPos = new Tuple<float, float>(posX, posY);
                        }
                        canvas.DrawPoint(lastPos.Item1, lastPos.Item2, endPointPaint);

                        using (var snapshot = surface.Snapshot())
                        using (var data = snapshot.Encode(SKEncodedImageFormat.Png, 80))
                        using (var ms = new MemoryStream())
                        {
                            data.SaveTo(ms);
                            byte[] imageBytes = ms.ToArray();

                            string base64String = Convert.ToBase64String(imageBytes);
                            model.SendImage(base64String);
                        }
                    }
                }
            }
        }
    }
}
