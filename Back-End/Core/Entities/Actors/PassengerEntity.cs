using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Actors
{
    public class PassengerEntity
    {
        [Key]
        public int PassengerID { get; set; }
        [ForeignKey("Person")]
        public int PersonID { get; set; }
        public PersonEntity? Person { get; set; }

    }
}
