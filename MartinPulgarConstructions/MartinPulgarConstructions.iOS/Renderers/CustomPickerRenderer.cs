using System;
using CoreAnimation;
using MartinPulgarConstructions.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace MartinPulgarConstructions.iOS.Renderers
{
    public class CustomPickerRenderer : PickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if(this.Control != null && this.Element != null)
            {
                var img = UIImage.FromFile("downarrow.png");
                Control.RightViewMode = UITextFieldViewMode.Always;
                var uiImageView = new UIImageView(img);
                uiImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                uiImageView.Frame = new CGRect(0, 0, 6, 10);
                Control.RightView = uiImageView;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (Control != null && Element != null)
            {
                var view = new UIView();
                view.Frame = new CGRect(rect.X, (rect.Y + rect.Height - 1), rect.Width, 1);
                
                view.BackgroundColor = UIColor.LightGray;
                Control.AddSubview(view);
            }
        }

    }
}
