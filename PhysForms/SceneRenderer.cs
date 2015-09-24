using System;
using System.Diagnostics;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.DXGI.Factory;

using PhysSim;

namespace PhysForms
{
    public class SceneRenderer
    {
        RenderTarget _target;
        SolidColorBrush _whiteBrush;
        Simulation _simulation;
        Factory _factory;

        public SceneRenderer(RenderTarget target, Factory factory, Simulation simulation)
        {
            _target = target;
            _factory = factory;
            _simulation = simulation;

            _whiteBrush = new SolidColorBrush(_target, Color.White);
        }

        //public void Render()
        //{
        //    _simulation.Step();

        //    _target.Clear(Color.Black);
        //    foreach (Body body in _simulation.GetState())
        //    {
        //   //     var rectangleGeometry = new RoundedRectangleGeometry(_factory, new RoundedRectangle() { RadiusX = 32, RadiusY = 32, Rect = new RectangleF(128, 128, width - 128 * 2, height - 128 * 2) });

        ////        _target.FillGeometry(rectangleGeometry, _whiteBrush, null);
        //    }
        //}
    }
}
