using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.Geometry;
using Windows.UI;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Win2dTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly List<CanvasGeometry> _maskGeometry = new List<CanvasGeometry>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            var opacityMask = CreateOpacityMask(sender, 800, 800);
            var brush = new CanvasImageBrush(sender, opacityMask);
            using (var layer = args.DrawingSession.CreateLayer(brush))
            {
                args.DrawingSession.FillRectangle(new Rect(100, 0, 500, 350), Colors.BlueViolet);
            }
        }

        private ICanvasImage CreateOpacityMask(ICanvasResourceCreator resourceCreator, double width, double height)
        {
            var device = CanvasDevice.GetSharedDevice();
            var target = new CanvasRenderTarget(device, (float)width, (float)height, 96);
            using (var session = target.CreateDrawingSession())
            {
                session.Clear(Colors.Transparent);
                foreach(var geometry in _maskGeometry)
                {
                    session.FillGeometry(geometry, Colors.White);
                }
            }
            return target;
        }

        private void Canvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            _maskGeometry.Add(ResourcesFactory.CreateCloud(sender));
            _maskGeometry.Add(ResourcesFactory.CreateCircle(sender));
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(PageTwo));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(PageThree));
        }
    }
}
