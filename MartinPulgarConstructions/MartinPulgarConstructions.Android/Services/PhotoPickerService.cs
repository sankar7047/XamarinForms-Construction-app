using System;
using MartinPulgarConstruction.SDKs;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using MartinPulgarConstructions.Droid.Services;
using Xamarin.Forms;
using MartinPulgarConstructions.Models;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace MartinPulgarConstructions.Droid.Services
{
    public class PhotoPickerService : IPhotoPickerService
    {
        public Task<Photo> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Photo"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Photo>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}
