using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public interface IUserRepository<T> : IRepository<T> where T : class
    {
         Task<EnUserRole> GetUserRole(string userId);
    }
}
