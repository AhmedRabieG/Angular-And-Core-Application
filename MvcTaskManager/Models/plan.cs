﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTaskManager.Models
{
    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        [DisplayFormat(DataFormatString = "d/M/yyyy")]
        public DateTime DateOfStart { get; set; }
        public int TeamSize { get; set; }
    }

    
}

