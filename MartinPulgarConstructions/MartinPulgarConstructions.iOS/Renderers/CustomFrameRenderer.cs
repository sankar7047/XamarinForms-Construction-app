using System;
using CoreGraphics;
using MartinPulgarConstructions.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomFrameRenderer))]
namespace MartinPulgarConstructions.iOS.Renderers
{
    public class CustomFrameRenderer : FrameRenderer
    {
        public CustomFrameRenderer()
        {

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
            }

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            var element = e.NewElement;

        }
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            Layer.ShadowRadius = 8.0f;
            Layer.ShadowColor = UIColor.LightGray.CGColor;
            Layer.ShadowOffset = new CGSize(0, 5);
            Layer.ShadowOpacity = 0.8f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }


    }
}
