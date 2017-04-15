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
        public SelectList Trainers { get; set; }
        public SelectList CourseInstallments { get; set; }
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
    }
}
