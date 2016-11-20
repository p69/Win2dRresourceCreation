using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Win2dTest
{
    public static class ResourcesFactory
    {
        public static CanvasRenderTarget CreateFromSharedDevice(ICanvasResourceCreator resourceCreator, double width, double height)
        {
            var device = CanvasDevice.GetSharedDevice();
            var target = new CanvasRenderTarget(device, (float)width, (float)height, 96);
            using (var session = target.CreateDrawingSession())
            {
                session.Clear(Colors.Transparent);
                var pathBuilder = new CanvasPathBuilder(session);
                pathBuilder.BeginFigure(170, 90, CanvasFigureFill.Default);
                pathBuilder.AddCubicBezier(new Vector2(130, 100), new Vector2(130, 150), new Vector2(230, 150));
                pathBuilder.AddCubicBezier(new Vector2(250, 180), new Vector2(320, 180), new Vector2(340, 150));
                pathBuilder.AddCubicBezier(new Vector2(420, 150), new Vector2(420, 120), new Vector2(390, 100));
                pathBuilder.AddCubicBezier(new Vector2(430, 40), new Vector2(370, 30), new Vector2(340, 50));
                pathBuilder.AddCubicBezier(new Vector2(320, 5), new Vector2(250, 20), new Vector2(250, 50));
                pathBuilder.AddCubicBezier(new Vector2(200, 5), new Vector2(150, 20), new Vector2(170, 80));
                pathBuilder.EndFigure(CanvasFigureLoop.Closed);
                var cloudFigure = CanvasGeometry.CreatePath(pathBuilder);

                session.FillGeometry(cloudFigure, Color.FromArgb((byte)(255 * 0.7), 0, 255, 0));
                var strokeColor = new CanvasSolidColorBrush(session, Color.FromArgb(255, 0, 0, 0));
                session.DrawGeometry(cloudFigure, strokeColor, 12);
                session.FillCircle(500, 20, 20, Colors.White);
            }
            return target;
        }

        public static CanvasGeometry CreateCloud(ICanvasResourceCreator resourceCreator)
        {
            var pathBuilder = new CanvasPathBuilder(resourceCreator);
            pathBuilder.BeginFigure(170, 90, CanvasFigureFill.Default);
            pathBuilder.AddCubicBezier(new Vector2(130, 100), new Vector2(130, 150), new Vector2(230, 150));
            pathBuilder.AddCubicBezier(new Vector2(250, 180), new Vector2(320, 180), new Vector2(340, 150));
            pathBuilder.AddCubicBezier(new Vector2(420, 150), new Vector2(420, 120), new Vector2(390, 100));
            pathBuilder.AddCubicBezier(new Vector2(430, 40), new Vector2(370, 30), new Vector2(340, 50));
            pathBuilder.AddCubicBezier(new Vector2(320, 5), new Vector2(250, 20), new Vector2(250, 50));
            pathBuilder.AddCubicBezier(new Vector2(200, 5), new Vector2(150, 20), new Vector2(170, 80));
            pathBuilder.EndFigure(CanvasFigureLoop.Closed);
            var cloudFigure = CanvasGeometry.CreatePath(pathBuilder);
            return cloudFigure;
        }

        public static CanvasGeometry CreateCircle (ICanvasResourceCreator resourceCreator)
        {
            return CanvasGeometry.CreateCircle(resourceCreator, 500, 20, 20);
        }
    }
}
