using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces.Actors_Interfaces
{
    internal interface IPerson
    {
        int PersonID { get; set; }
        string NationalID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime BirthDate { get; set; }
        EnGender Gender { get; set; }
    }
}
