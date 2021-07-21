using System;
using System.IO;
using System.Threading.Tasks;
using MartinPulgarConstructions.Models;

namespace MartinPulgarConstruction.SDKs
{
    public interface IPhotoPickerService
    {
        Task<Photo> GetImageStreamAsync();
    }
}
