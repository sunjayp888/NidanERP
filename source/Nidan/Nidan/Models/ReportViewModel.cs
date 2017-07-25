using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nidan.Models
{
    public class ReportViewModel : BaseViewModel
    {
        public SelectList Centres { get; set; }
        public string FromMonth { get; set; }
        public string ToMonth { get; set; }
        public IEnumerable<SelectListItem> MonthList { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }
        public IEnumerable<SelectListItem> YearList { get; set; }
        public List<MonthType> MonthType => new List<MonthType>()
        {
            new MonthType() {Id = 01,Name = "January"},
            new MonthType() {Id = 02,Name = "February"},
            new MonthType() {Id = 03,Name = "March"},
            new MonthType() {Id = 04,Name = "April"},
            new MonthType() {Id = 05,Name = "May"},
            new MonthType() {Id = 06,Name = "June"},
            new MonthType() {Id = 07,Name = "July"},
            new MonthType() {Id = 08,Name = "August"},
            new MonthType() {Id = 09,Name = "September"},
            new MonthType() {Id = 10,Name = "October"},
            new MonthType() {Id = 11,Name = "November"},
            new MonthType() {Id = 12,Name = "December"}
        };
        public List<YearType> YearType => new List<YearType>()
        {
            new YearType() {Id = 2016,Name = "2016"},
            new YearType() {Id = 2017,Name = "2017"},
            new YearType() {Id = 2018,Name = "2018"},
            new YearType() {Id = 2019,Name = "2019"},
            new YearType() {Id = 2020,Name = "2020"}
        };
    }
    public class MonthType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class YearType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}