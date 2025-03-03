﻿using Core_Layer.Entities.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Locations
{
    public class CountryEntity
    {
        [Key]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Country Name is required.")]
        [StringLength(100, ErrorMessage = "Country Name cannot exceed 100 characters.")]
        public required string CountryName { get; set; }

        // Navigation
        public IEnumerable<RegionEntity>? Regions { get; set; }
    }
}
