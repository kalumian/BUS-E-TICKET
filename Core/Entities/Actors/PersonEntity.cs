using Core_Layer.Enums;
using Core_Layer.Interfaces.Actors_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Actors
{
    public class PersonEntity : IPerson
    {
        [Key]
        public required int PersonID { get; set; }

        [Required(ErrorMessage = "National ID is required.")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "National ID must be between 5 and 15 characters.")]
        public required string NationalID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public required string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public required DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required.")] 
        public required EnGender Gender { get; set; }

        // Navigations
        public IEnumerable<CustomerEntity>? Customers { get; set; }
        public IEnumerable<PassengerEntity>? Passengers { get; set; }

    }
}
