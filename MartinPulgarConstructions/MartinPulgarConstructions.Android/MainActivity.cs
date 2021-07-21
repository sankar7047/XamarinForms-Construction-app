using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.IO;
using System.Threading.Tasks;
using Android.Webkit;
using MartinPulgarConstructions.Models;
using Android.Graphics;

namespace MartinPulgarConstructions.Droid
{
    [Activity(Label = "MartinPulgarConstructions", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<Photo> PickImageTaskCompletionSource { set; get; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Instance = this;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    var extension = GetFileExtension(uri);
                    Stream stream = ContentResolver.OpenInputStream(uri);
                    //var res = Uri.TryCreate(uri.ToString(), UriKind.Absolute, out Uri uri1);
                    Bitmap mBitmap = null;
                    byte[] bitmapData = null;
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
                    {
                        var source = ImageDecoder.CreateSource(Android.App.Application.Context.ContentResolver, uri);
                        mBitmap = ImageDecoder.DecodeBitmap(source);
                    }
                    else
                    {
                        mBitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(Android.App.Application.Context.ContentResolver, uri);
                    }

                    Bitmap myBitmap = null;
                    if (mBitmap != null)
                    {
                        myBitmap = Bitmap.CreateBitmap(mBitmap);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            if (extension == "png")
                                myBitmap.Compress(Bitmap.CompressFormat.Png, 50, ms);
                            else
                                myBitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, ms);
                            bitmapData = ms.ToArray();
                        }

                    }
                    myBitmap.Recycle();
                    mBitmap.Recycle();

                    var photo = new Photo()
                    {
                        ImageFormat = extension,
                        ImageStream = stream,
                        ImageData = Convert.ToBase64String(bitmapData)
                    };

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(photo);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

        public string GetFileExtension(Android.Net.Uri uri)
        {
            ContentResolver contentResolver = ApplicationContext.ContentResolver;
            MimeTypeMap mimeTypeMap = MimeTypeMap.Singleton;

            // Return file Extension
            return mimeTypeMap.GetExtensionFromMimeType(contentResolver.GetType(uri));
        }
    }
}