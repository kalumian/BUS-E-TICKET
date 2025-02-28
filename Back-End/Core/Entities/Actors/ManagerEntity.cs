using Core_Layer.Enums;
using Core_Layer.Interfaces.Actors_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Actors
{
    public  class ManagerEntity : IManagerEntity
    {
        [Key]
        public int ManagerID { get; set; }
        #region Foreign Keys

        [ForeignKey("Account")]
        public required string AccountID { get; set; }

        [ForeignKey("CreatedBy")]
        public required int CreatedByID { get; set; }

        #endregion

        #region Navigation Properties

        public AuthoUser? Account { get; set; }

        public ManagerEntity? CreatedBy { get; set; }

        public IEnumerable<ManagerEntity>? Managers { get; set; }

        #endregion


    }
}
