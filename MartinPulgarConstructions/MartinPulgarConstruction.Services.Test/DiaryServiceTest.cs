using System;
using MartinPulgarConstruction.SDKs;
using MartinPulgarConstruction.Service;
using MartinPulgarConstructions.Models;
using NUnit.Framework;

namespace MartinPulgarConstruction.Services.Test
{
    public class Tests
    {
        IDiaryService diaryService;

        [SetUp]
        public void Setup()
        {
            diaryService = new DiaryService();
        }

        /// <summary>
        /// Test case naming convention
        /// _GetAreas
        /// _ReturnAreas
        /// </summary>
        [Test]
        public void GetAreas_ReturnAreas()
        {
            Assert.DoesNotThrow(() => diaryService.GetAreas());
            Assert.NotNull(diaryService.GetAreas());
            Assert.IsTrue(diaryService.GetAreas().Count == 11);
        }

        /// <summary>
        /// Test case naming convention
        /// _GetCategories
        /// _ReturnCategories
        /// </summary>
        [Test]
        public void GetCategories_ReturnCategories()
        {
            Assert.DoesNotThrow(() => diaryService.GetCategories());
            Assert.NotNull(diaryService.GetCategories());
            Assert.IsTrue(diaryService.GetCategories().Count == 5);
        }

        /// <summary>
        /// Test case naming convention
        /// _GetEvents
        /// _ReturnEvents
        /// </summary>
        [Test]
        public void GetEvents_ReturnEvents()
        {
            Assert.DoesNotThrow(() => diaryService.GetEvents());
            Assert.NotNull(diaryService.GetEvents());
            Assert.IsTrue(diaryService.GetEvents().Count == 4);
        }

        /// <summary>
        /// Test case naming convention
        /// _GetLocationId
        /// _ReturnsLocationId
        /// </summary>
        [Test]
        public void GetLocationId_ReturnsLocationId()
        {
            Assert.DoesNotThrow(() => diaryService.GetLocationId());
            Assert.NotNull(diaryService.GetLocationId());
            Assert.IsTrue(int.TryParse(diaryService.GetLocationId(), out int i));
        }

        /// <summary>
        /// Test case naming convention
        /// _GetLocationName
        /// _ReturnsLocationName
        /// </summary>
        [Test]
        public void GetLocationName_ReturnsLocationName()
        {
            Assert.DoesNotThrow(() => diaryService.GetLocationName());
            Assert.NotNull(diaryService.GetLocationName());
            Assert.IsFalse(string.IsNullOrEmpty(diaryService.GetLocationName()));
        }

        /// <summary>
        /// Test case naming convention
        /// _Name of the function
        /// _Input
        /// _Expected Result
        /// Since the API is hosted publicly, directly called the API instead of mocking.
        /// </summary>
        [Test]
        public void SaveNewDiary_NewDiaryObject_Success()
        {
            var diary = new Diary()
            {
                Area = "Area1",
                Category = "Category1",
                Event = "Evnet1",
                Comments = "Comments",
                Date = DateTime.Now,
                LocationId = "123324",
                LocationName = "StrathField"
            };

            Assert.DoesNotThrow(() => diaryService.SaveNewDiary(diary));
            Assert.NotNull(diaryService.SaveNewDiary(diary));
            Assert.IsTrue(diaryService.SaveNewDiary(diary).Result);
        }
    }
}