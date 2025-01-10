using Core_Layer;
using Core_Layer.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class UserRepository<T> : GeneralRepository<T>, IUserRepository<T> where T : class
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
        
        public async Task<EnUserRole> GetUserRole(string UserId)
        {
            using(var connection = new SqlConnection(DatabaseConnectionSettings.DatabaseStringConnection))
            {
                using(var command = new SqlCommand("SP_GetUserType", connection))
                {
                    /*
                    Alter PROCEDURE SP_GetUserType
                        @UserId NVARCHAR(450)
                    AS
                    BEGIN
                        IF EXISTS (SELECT 1 FROM Customers WHERE AccountID = @UserId)
                        BEGIN
                            SELECT 3 AS UserType;
                            RETURN;
                        END
                        IF EXISTS (SELECT 1 FROM Managers WHERE AccountID = @UserId)
                        BEGIN
                            SELECT 1 AS UserType;
                            RETURN;
                        END
                        IF EXISTS (SELECT 1 FROM ServiceProviders WHERE AccountID = @UserId)
                        BEGIN
                            SELECT 2 AS UserType;
                            RETURN;
                        END
                        SELECT -1 AS UserType;
                     END
                     */
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);

                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    
                    if(result != null && int.TryParse(result.ToString(),out int Role)){
                        if (Enum.IsDefined(typeof(EnUserRole), Role)) return (EnUserRole)Role;
                    }

                    return EnUserRole.Unkown;
                }
            }
        }
    }
}
