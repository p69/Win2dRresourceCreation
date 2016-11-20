using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.Geometry;
using Windows.UI;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Win2dTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageThree : Page
    {
        private readonly List<CanvasGeometry> _maskGeometry = new List<CanvasGeometry>();

        public PageThree()
        {
            this.InitializeComponent();
        }

        private void Canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            var opacityMask = CreateOpacityMask(sender, 800, 800);
            var bytes = opacityMask.GetPixelBytes();
            var bmp = CanvasBitmap.CreateFromBytes(sender.Device, bytes, 800, 800, Windows.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized);
            var brush = new CanvasImageBrush(sender, bmp);
            using (var layer = args.DrawingSession.CreateLayer(brush))
            {
                args.DrawingSession.FillRectangle(new Rect(100, 0, 500, 350), Colors.BlueViolet);
            }
            Canvas.Paused = true;
        }

        private CanvasRenderTarget CreateOpacityMask(ICanvasResourceCreator resourceCreator, double width, double height)
        {
            var device = CanvasDevice.GetSharedDevice();
            var target = new CanvasRenderTarget(device, (float)width, (float)height, 96);
            using (var session = target.CreateDrawingSession())
            {
                session.Clear(Colors.Transparent);

                _maskGeometry.Add(ResourcesFactory.CreateCloud(session));
                _maskGeometry.Add(ResourcesFactory.CreateCircle(session));

                foreach (var geometry in _maskGeometry)
                {
                    session.FillGeometry(geometry, Colors.White);
                }
            }
            return target;
        }

        private void Canvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
        }
    }
}
