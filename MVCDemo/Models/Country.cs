﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCDemo.Models
{
    [Table("Country")]
    public class Country
    {
        [Key]

        public int CountryID { get; set; }
        public string Countryname { get; set; }
       
    }
}
