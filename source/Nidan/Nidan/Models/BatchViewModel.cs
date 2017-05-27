using Nidan.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Nidan.Models
{
    public class BatchViewModel : BaseViewModel
    {
        public Batch Batch { get; set; }
        public CourseInstallment CourseInstallment { get; set; }
        public Course Course { get; set; }
        public BatchDay BatchDay { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Rooms { get; set; }
        public SelectList Trainers { get; set; }
        public SelectList CourseInstallments { get; set; }
        public IEnumerable<SelectListItem> HoursList { get; set; }
        public IEnumerable<SelectListItem> MinutesList { get; set; }
        public List<int> SelectedTrainerIds
        {
            get
            {
                return JsonConvert.DeserializeObject<List<int>>(SelectedTrainerIdsJson);
            }
            set
            {
                SelectedTrainerIdsJson = JsonConvert.SerializeObject(value);
            }
        }

        public string SelectedTrainerIdsJson { get; set; }

        public List<HoursType> HoursType => new List<HoursType>()
        {
            new HoursType() { Id = 01},
            new HoursType() { Id = 02},
            new HoursType() { Id = 03},
            new HoursType() { Id = 04},
            new HoursType() { Id = 05},
            new HoursType() { Id = 06},
            new HoursType() { Id = 07},
            new HoursType() { Id = 08},
            new HoursType() { Id = 09},
            new HoursType() { Id = 10},
            new HoursType() { Id = 11},
            new HoursType() { Id = 12}
        };

        public List<MinutesType> MinutesType => new List<MinutesType>()
        {
            new MinutesType() {Id = 00},
            new MinutesType() {Id = 01},
            new MinutesType() {Id = 02},
            new MinutesType() {Id = 03},
            new MinutesType() {Id = 04},
            new MinutesType() {Id = 05},
            new MinutesType() {Id = 06},
            new MinutesType() {Id = 07},
            new MinutesType() {Id = 08},
            new MinutesType() {Id = 09},
            new MinutesType() {Id = 10},
            new MinutesType() {Id = 11},
            new MinutesType() {Id = 12},
            new MinutesType() {Id = 13},
            new MinutesType() {Id = 14},
            new MinutesType() {Id = 15},
            new MinutesType() {Id = 16},
            new MinutesType() {Id = 17},
            new MinutesType() {Id = 18},
            new MinutesType() {Id = 19},
            new MinutesType() {Id = 20},
            new MinutesType() {Id = 21},
            new MinutesType() {Id = 22},
            new MinutesType() {Id = 23},
            new MinutesType() {Id = 24},
            new MinutesType() {Id = 25},
            new MinutesType() {Id = 26},
            new MinutesType() {Id = 27},
            new MinutesType() {Id = 28},
            new MinutesType() {Id = 29},
            new MinutesType() {Id = 30},
            new MinutesType() {Id = 31},
            new MinutesType() {Id = 32},
            new MinutesType() {Id = 33},
            new MinutesType() {Id = 34},
            new MinutesType() {Id = 35},
            new MinutesType() {Id = 36},
            new MinutesType() {Id = 37},
            new MinutesType() {Id = 38},
            new MinutesType() {Id = 39},
            new MinutesType() {Id = 40},
            new MinutesType() {Id = 41},
            new MinutesType() {Id = 42},
            new MinutesType() {Id = 43},
            new MinutesType() {Id = 44},
            new MinutesType() {Id = 45},
            new MinutesType() {Id = 46},
            new MinutesType() {Id = 47},
            new MinutesType() {Id = 48},
            new MinutesType() {Id = 49},
            new MinutesType() {Id = 50},
            new MinutesType() {Id = 51},
            new MinutesType() {Id = 52},
            new MinutesType() {Id = 53},
            new MinutesType() {Id = 54},
            new MinutesType() {Id = 55},
            new MinutesType() {Id = 56},
            new MinutesType() {Id = 57},
            new MinutesType() {Id = 58},
            new MinutesType() {Id = 59},
            new MinutesType() {Id = 60}

        };
    }
    public class HoursType
    {
        public int Id { get; set; }
    }

    public class MinutesType
    {
        public int Id { get; set; }
    }
}
