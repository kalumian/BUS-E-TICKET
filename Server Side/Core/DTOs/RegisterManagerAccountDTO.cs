﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class RegisterManagerAccountDTO
    {
        public required RegisterAccountDTO Account { get; set; }    
        public required string CreatedByID { get; set; }
    }
}
