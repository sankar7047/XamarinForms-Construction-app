using System;
using System.Globalization;
using System.IO;
using MartinPulgarConstructions.Models;
using Xamarin.Forms;

namespace MartinPulgarConstructions.Converters
{
    public class ImageStreamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var photo = value as Photo;
            if (photo != null)
            {
                if (!string.IsNullOrEmpty(photo.ImageUrl))
                {
                    return ImageSource.FromFile(photo.ImageUrl);
                }
                else
                    return ImageSource.FromStream(() => photo.ImageStream);
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not implemented as the Photo field is going to be oneway binding.
            throw new NotImplementedException();
        }
    }
}
