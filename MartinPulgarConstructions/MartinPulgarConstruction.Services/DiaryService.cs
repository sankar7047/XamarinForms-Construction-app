using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MartinPulgarConstruction.SDKs;
using MartinPulgarConstructions.Models;
using Newtonsoft.Json;

namespace MartinPulgarConstruction.Service
{
    public class DiaryService : IDiaryService
    {
        Random random = new Random();

        /// <summary>
        /// Gets the areas
        /// </summary>
        /// <returns>List of Areas</returns>
        public IList<string> GetAreas()
        {
            return new List<string>() { "Select Area", "Strathfield", "North Strathffield", "Parramatta", "Fairfield", "Sydney", "Chatswood", "Ponds", "PendleHill", "Westmead", "Northmead" };
        }

        /// <summary>
        /// Gets the categories
        /// </summary>
        /// <returns>List of categories</returns>
        public IList<string> GetCategories()
        {
            return new List<string>() { "Task Category", "Carpentering", "Plumbing", "Estimating", "Planning" };
        }

        /// <summary>
        /// Gets the events
        /// </summary>
        /// <returns>List of events</returns>
        public IList<string> GetEvents()
        {
            return new List<string>() { "Select an event", "Completed", "Ongoing", "Hold" };
        }

        /// <summary>
        /// Gets the Location Id
        /// </summary>
        /// <returns>Location id</returns>
        public string GetLocationId()
        {
            return random.Next(1000000, 9999999).ToString();
        }

        /// <summary>
        /// Gets Location Name
        /// </summary>
        /// <returns>Location Name</returns>
        public string GetLocationName()
        {
            var locations = new List<string>() { "Strathfield", "North Strathffield", "Parramatta", "Fairfield", "Sydney", "Chatswood", "Ponds", "PendleHill", "Westmead", "Northmead" };
            return locations[random.Next(0, 9)];
        }

        /// <summary>
        /// Post the Diary data to the API
        /// </summary>
        /// <param name="diary"></param>
        /// <returns>Response</returns>
        public async Task<bool> SaveNewDiary(Diary diary)
        {
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://reqres.in/api/users"))
                {
                    var body = JsonConvert.SerializeObject(diary);
                    request.Content = new StringContent(body);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    response = await httpClient.SendAsync(request);

                }
            }

            return response != null ? response.IsSuccessStatusCode : false;
        }
    }
}
