using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities
{
    public class CurrencyEntity
    {
        [Key]
        public int CurrencyID { get; set; }

        [Required(ErrorMessage = "CurrencyName is required.")]
        [MaxLength(50, ErrorMessage = "CurrencyName cannot exceed 50 characters.")]
        public required string CurrencyName { get; set; }

        [Required(ErrorMessage = "CurrencyCode is required.")]
        [MaxLength(10, ErrorMessage = "CurrencyCode cannot exceed 10 characters.")]
        public required string CurrencyCode { get; set; }

        [Required(ErrorMessage = "Symbol is required.")]
        [MaxLength(5, ErrorMessage = "CurrencyCode cannot exceed 10 characters.")]
        public required string Symbol { get; set; }
    }
}
