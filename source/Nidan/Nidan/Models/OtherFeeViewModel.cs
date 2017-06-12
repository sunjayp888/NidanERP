﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Nidan.Entity;

namespace Nidan.Models
{
    public class OtherFeeViewModel:BaseViewModel
    {
        public OtherFee OtherFee { get; set; }
        public SelectList ExpenseHeaders { get; set; }
        public SelectList PaymentModes { get; set; }
        public SelectList Projects { get; set; }

        public List<int> SelectedProjectIds
        {
            get
            {
                return JsonConvert.DeserializeObject<List<int>>(SelectedProjectIdsJson);
            }
            set
            {
                SelectedProjectIdsJson = JsonConvert.SerializeObject(value);
            }
        }

        public string SelectedProjectIdsJson { get; set; }

    }
}