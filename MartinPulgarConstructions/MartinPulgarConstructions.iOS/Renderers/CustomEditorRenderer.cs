using System;
using CoreGraphics;
using MartinPulgarConstructions.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace MartinPulgarConstructions.iOS.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        
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
