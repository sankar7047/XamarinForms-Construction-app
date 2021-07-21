using System;
using System.Collections.Generic;
using MartinPulgarConstruction.Models;

namespace MartinPulgarConstructions.Models
{
    public class Diary : BaseNotify
    {

        private string _locationId;
        public string LocationId
        {
            get => _locationId;
            set => SetPropertyChanged(ref _locationId, value);
        }

        private string _locationName;
        public string LocationName
        {
            get => _locationName;
            set => SetPropertyChanged(ref _locationName, value);
        }

        private string _comments;
        public string Comments
        {
            get => _comments;
            set => SetPropertyChanged(ref _comments, value);
        }

        private IList<Photo> _siteDiaryPhotos;
        public IList<Photo> SiteDiaryPhotos
        {
            get => _siteDiaryPhotos;
            set => SetPropertyChanged(ref _siteDiaryPhotos, value);
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set => SetPropertyChanged(ref _date, value);
        }

        private string _area;
        public string Area
        {
            get => _area;
            set => SetPropertyChanged(ref _area, value);
        }

        private string _category;
        public string Category
        {
            get => _category;
            set => SetPropertyChanged(ref _category, value);
        }

        private string _tags;
        public string Tags
        {
            get => _tags;
            set => SetPropertyChanged(ref _tags, value);
        }

        private string _events;
        public string Event
        {
            get => _events;
            set => SetPropertyChanged(ref _events, value);
        }
    }
}
