using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nidan.Entity;

namespace Nidan.Models
{
    public class SubjectViewModel : BaseViewModel
    {
        public SubjectViewModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Trainers { get; set; }
        public SelectList CourseTypes { get; set; }
        [Required]
        public List<HttpPostedFileBase> Files { get; set; }
    }
}