using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MartinPulgarConstructions.Models;

namespace MartinPulgarConstruction.SDKs
{
    public interface IDiaryService
    {
        /// <summary>
        /// Gets the areas
        /// </summary>
        /// <returns>List of Areas</returns>
        string GetLocationId();

        /// <summary>
        /// Gets the categories
        /// </summary>
        /// <returns>List of categories</returns>
        string GetLocationName();

        /// <summary>
        /// Gets the events
        /// </summary>
        /// <returns>List of events</returns>
        IList<string> GetAreas();

        /// <summary>
        /// Gets the Location Id
        /// </summary>
        /// <returns>Location id</returns>
        IList<string> GetCategories();

        /// <summary>
        /// Gets Location Name
        /// </summary>
        /// <returns>Location Name</returns>
        IList<string> GetEvents();

        /// <summary>
        /// Post the Diary data to the API
        /// </summary>
        /// <param name="diary"></param>
        /// <returns>Response</returns>
        Task<bool> SaveNewDiary(Diary diary);
    }
}
