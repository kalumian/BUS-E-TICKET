using Core_Layer.Entities.Actors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class CustomerRepository : GeneralRepository<CustomerEntity>
    {
        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
