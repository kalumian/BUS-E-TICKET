﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces.Actors_Interfaces
{
    public interface IManagerEntity : IAccount
    {
        int CreatedByID { get; set; }
    }
}
