using System;
using System.IO;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using MartinPulgarConstruction.SDKs;
using MartinPulgarConstructions.iOS.Services;
using MartinPulgarConstructions.Models;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace MartinPulgarConstructions.iOS.Services
{
    public class PhotoPickerService : IPhotoPickerService
    {
        TaskCompletionSource<Photo> taskCompletionSource;
        UIImagePickerController imagePicker;
        UIViewController viewController;

        public Task<Photo> GetImageStreamAsync()
        {
            // Create and define UIImagePickerController
            imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            // Set event handlers
            imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            imagePicker.Canceled += OnImagePickerCancelled;

            // Present UIImagePickerController;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            viewController = window.RootViewController;
            viewController.PresentViewController(imagePicker, true, null);

            // Return Task object
            taskCompletionSource = new TaskCompletionSource<Photo>();
            return taskCompletionSource.Task;

        }

        void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
        {
            

            UIImage image = args.EditedImage ?? args.OriginalImage;

            if (image != null)
            {
                // Convert UIImage to .NET Stream object
                NSData data;
                if (args.ReferenceUrl.PathExtension.Equals("PNG") || args.ReferenceUrl.PathExtension.Equals("png"))
                {
                    data = image.AsPNG();
                }
                else
                {
                    data = image.AsJPEG(1);
                }
                Stream stream = data.AsStream();
                
                UnregisterEventHandlers();

                // Set the Stream as the completion of the Task
                taskCompletionSource.SetResult(new Photo()
                {
                    ImageFormat = args.ReferenceUrl.PathExtension,
                    ImageStream = stream,
                    ImageUrl = args.ImageUrl.Path
                });
            }
            else
            {
                UnregisterEventHandlers();
                taskCompletionSource.SetResult(null);
            }
            viewController.DismissModalViewController(true);
        }

        void OnImagePickerCancelled(object sender, EventArgs args)
        {
            UnregisterEventHandlers();
            taskCompletionSource.SetResult(null);
            viewController.DismissModalViewController(true);
        }

        void UnregisterEventHandlers()
        {
            imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
            imagePicker.Canceled -= OnImagePickerCancelled;
        }
    }
}
