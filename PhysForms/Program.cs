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
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var form = new RenderForm("FUN STUFF PHYSXS");
            
            // SwapChain description
            var desc = new SwapChainDescription()
                           {
                               BufferCount = 1,
                               ModeDescription = 
                                   new ModeDescription(form.ClientSize.Width, form.ClientSize.Height,
                                                       new Rational(60, 1), Format.R8G8B8A8_UNorm),
                               IsWindowed = true,
                               OutputHandle = form.Handle,
                               SampleDescription = new SampleDescription(1, 0),
                               SwapEffect = SwapEffect.Discard,
                               Usage = Usage.RenderTargetOutput
                           };

            // Create Device and SwapChain
            Device device;
            SwapChain swapChain;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, new SharpDX.Direct3D.FeatureLevel[] { SharpDX.Direct3D.FeatureLevel.Level_10_0 }, desc, out device, out swapChain);

            var d2dFactory = new SharpDX.Direct2D1.Factory();
                        
            // Ignore all windows events
            Factory factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            // New RenderTargetView from the backbuffer
            Texture2D backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            var renderView = new RenderTargetView(device, backBuffer);

            Surface surface = backBuffer.QueryInterface<Surface>();


            var d2dRenderTarget = new RenderTarget(d2dFactory, surface,
                                                            new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));

            var solidColorBrush = new SolidColorBrush(d2dRenderTarget, Color.White);

            var sim = new Simulation();

            // Main loop
            RenderLoop.Run(form, () =>
                                      {
                                          d2dRenderTarget.BeginDraw();

                                          int width = form.ClientSize.Width;
                                          int height = form.ClientSize.Height;
                                          var scaleX = sim.SimBounds.MaxX - sim.SimBounds.MinX;
                                          var scaleY = sim.SimBounds.MaxY - sim.SimBounds.MinY;

                                          d2dRenderTarget.Clear(Color.Black);
                                          foreach (Body body in sim.State)
                                          {
                                              var x = Convert.ToSingle((body.Pos.X - sim.SimBounds.MinX) * width / scaleX);
                                              var y = Convert.ToSingle((body.Pos.Y - sim.SimBounds.MinY) * height / scaleY);
                                              var xRad = Convert.ToSingle((body.Radius) * width / scaleX);
                                              var yRad = Convert.ToSingle((body.Radius) * height / scaleY);
                                              var elipse = new EllipseGeometry(d2dFactory, new Ellipse(new Vector2(x, y), xRad, yRad));
                                              d2dRenderTarget.FillGeometry(elipse, solidColorBrush, null);
                                          }


                                          d2dRenderTarget.EndDraw();

                                          sim.Step();
                                          swapChain.Present(0, PresentFlags.None);
                                      });

            // Release all resources
            renderView.Dispose();
            backBuffer.Dispose();
            device.ImmediateContext.ClearState();
            device.ImmediateContext.Flush();
            device.Dispose();
            device.Dispose();
            swapChain.Dispose();
            factory.Dispose();
        }
    }
}
