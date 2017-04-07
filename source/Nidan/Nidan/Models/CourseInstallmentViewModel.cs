using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class CourseInstallmentViewModel : BaseViewModel
    {
        public CourseInstallment CourseInstallment { get; set; }
        public int CourseFeeBreakUpId { get; set; }
        public string CourseFeeBreakUpName { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Centres { get; set; }
        public int NoOfInstallment { get; set; }
        public IEnumerable<SelectListItem> NoOfInstallmentList { get; set; }
        public List<NoOfInstallmentType> NoOfInstallmentType => new List<NoOfInstallmentType>()
        {
            new NoOfInstallmentType() {Id=1,Name="1" },
            new NoOfInstallmentType() {Id=2,Name="2" },
            new NoOfInstallmentType() {Id=3,Name="3" },
            new NoOfInstallmentType() {Id=4,Name="4" },
            new NoOfInstallmentType() {Id=5,Name="5" },
            new NoOfInstallmentType() {Id=6,Name="6" },
            new NoOfInstallmentType() {Id=7,Name="7" },
            new NoOfInstallmentType() {Id=8,Name="8" },
            new NoOfInstallmentType() {Id=9,Name="9" },
            new NoOfInstallmentType() {Id=10,Name="10" },
            new NoOfInstallmentType() {Id=11,Name="11" },
            new NoOfInstallmentType() {Id=12,Name="12" }
        };
    }
    public class NoOfInstallmentType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}