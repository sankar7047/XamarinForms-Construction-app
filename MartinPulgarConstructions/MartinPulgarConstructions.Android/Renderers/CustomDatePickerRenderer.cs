using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using MartinPulgarConstructions.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePickerRenderer))]
namespace MartinPulgarConstructions.Droid.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context _context) : base(_context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
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
                backgroundColorDrawable,
                GetDrawable()
        };

            // Initialize a new layer drawable instance from drawables array
            LayerDrawable layerDrawable = new LayerDrawable(drawables);

            // Set padding for background color layer
            layerDrawable.SetLayerInset(1, 0, 0, 0, 5);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable()
        {
            int resID = Resources.GetIdentifier("downarrow", "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 35, 30, true));
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }
    }
}
