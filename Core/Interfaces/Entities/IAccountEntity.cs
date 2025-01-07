using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Enums;

namespace Core_Layer.Interfaces.Actors_Interfaces
{
    public interface IAccountEntity
    {
        int AccountID { get; set;}
        DateTime RegisterationDate { get; set; }
        DateTime? LastLoginDate { get; set; }
        EnAccountStatus AccountStatus {  get; set; }
        int Permission {  get; set; }
    }
}
