using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using MartinPulgarConstructions.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace MartinPulgarConstructions.Droid.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null && Element != null)
                Control.Background = AddPickerStyles();
        }

        public LayerDrawable AddPickerStyles()
        {
            // Initialize new color drawables
            ColorDrawable borderColorDrawable = new ColorDrawable(Android.Graphics.Color.LightGray);
            ColorDrawable backgroundColorDrawable = new ColorDrawable(Android.Graphics.Color.White);

            // Initialize a new array of drawable objects
            Drawable[] drawables = new Drawable[]{
                borderColorDrawable,
                backgroundColorDrawable
        };

            // Initialize a new layer drawable instance from drawables array
            LayerDrawable layerDrawable = new LayerDrawable(drawables);

            // Set padding for background color layer
            layerDrawable.SetLayerInset(1, 0, 0, 0, 5);

            return layerDrawable;
        }
    }
}
