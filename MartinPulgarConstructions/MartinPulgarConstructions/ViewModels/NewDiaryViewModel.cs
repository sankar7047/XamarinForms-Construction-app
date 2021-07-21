using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using MartinPulgarConstruction.Models;
using MartinPulgarConstruction.SDKs;
using MartinPulgarConstructions.Models;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;

namespace MartinPulgarConstructions.ViewModels
{
    public class NewDiaryViewModel : BaseNotify
    {
        #region Fields

        private readonly IDiaryService diaryService;

        #endregion

        public NewDiaryViewModel()
        {
            diaryService = (IDiaryService)App.ServiceProvider.GetService(typeof(IDiaryService));
            Initilize();
        }

        #region Properties

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetPropertyChanged(ref _isBusy, value);
        }

        private Diary _activeDiary;
        public Diary ActiveDiary
        {
            get => _activeDiary;
            set => SetPropertyChanged(ref _activeDiary, value);
        }

        ObservableCollection<Photo> images = new ObservableCollection<Photo>();
        public ObservableCollection<Photo> SiteDiaryPhotos
        {
            get { return images; }
        }

        private string _locationId;
        public string LocationId
        {
            get =>  _locationId;
            set => SetPropertyChanged(ref _locationId, value);
        }

        private string _locationName;
        public string LocationName
        {
            get => _locationName;
            set => SetPropertyChanged(ref _locationName, value);
        }

        private IList<string> _areas;
        public IList<string> Areas
        {
            get => _areas;
            set => SetPropertyChanged(ref _areas, value);
        }

        private IList<string> _categories;
        public IList<string> Categories
        {
            get => _categories;
            set => SetPropertyChanged(ref _categories, value);
        }

        private IList<string> _events;
        public IList<string> Events
        {
            get => _events;
            set => SetPropertyChanged(ref _events, value);
        }

        private bool _canLinkEvent = true;
        public bool CanLinkEvent
        {
            get => _canLinkEvent;
            set => SetPropertyChanged(ref _canLinkEvent, value);
        }

        #endregion

        #region Commands

        public ICommand AddPhotoCommand => new Command(async (o) =>
        {
            var photo = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (photo != null)
            {
                SiteDiaryPhotos.Add(photo);
            }
        });

        public ICommand RemovePhotoCommand => new Command((o) =>
        {
            var photo = o as Photo;
            if (photo != null)
                SiteDiaryPhotos.Remove(photo);
        });

        public ICommand SaveNewDiaryCommand => new Command(async (o) =>
        {
            try
            {
                IsBusy = true;

                if (ActiveDiary.Area != Areas[0] && ActiveDiary.Category != Categories[0] && (!CanLinkEvent || (CanLinkEvent && ActiveDiary.Event != Events[0])))
                {
                    if(Device.RuntimePlatform == Device.iOS)
                    {
                        foreach (var item in SiteDiaryPhotos)
                        {
                            string imageData = string.Empty;
                            using (var memoryStream = new MemoryStream())
                            {
                                item.ImageStream.CopyTo(memoryStream);
                                var bytes = memoryStream.ToArray();
                                item.ImageData = Convert.ToBase64String(bytes);
                            }
                        }
                    }

                    ActiveDiary.SiteDiaryPhotos = SiteDiaryPhotos;
                    var res = await diaryService.SaveNewDiary(ActiveDiary);

                    Application.Current.MainPage.DisplayToastAsync(res ? AppResources.PostDataSuccess : AppResources.PostDataFailure);
                }
                else
                {
                    // Invalid input
                    Application.Current.MainPage.DisplayToastAsync(AppResources.PostDataInvalidInput);
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayToastAsync(AppResources.PostDataException);
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
            
        });

        #endregion

        #region Methods

        private void Initilize()
        {
            Areas = diaryService.GetAreas();
            Categories = diaryService.GetCategories();
            Events = diaryService.GetEvents();

            ActiveDiary = new Diary
            {
                LocationId = diaryService.GetLocationId(),
                LocationName = diaryService.GetLocationName(),
                Area = Areas[0],
                Category = Categories[0],
                Event = Events[0]
            };
        }

        #endregion
    }
}
