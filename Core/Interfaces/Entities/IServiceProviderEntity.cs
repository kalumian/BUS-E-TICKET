using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces.Actors_Interfaces
{
    public interface IServiceProviderEntity : IFollowableAccount
    {
        string BusinessName { get; set; }
        string LogoURL { get; set; }
    }
}
