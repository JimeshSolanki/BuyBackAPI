using BuyBackAPI.Models;
using BuyBackAPI.Translator;
using BuyBackAPI.Utility;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Repository
{
    public class UserDBClient
    {
        public List<UserModel> GetAllUsers(string connectionString)
        {
            return SqlHelper.ExecuteProcedureReturnData<List<UserModel>>(connectionString, "GetAllUsers", x => x.TranslateAsUsersList());
        }

        public UserModel GetUserById(string connectionString, int id)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@Id", id) };
            return SqlHelper.ExecuteProcedureReturnData<UserModel>(connectionString, "GetUserById", x => x.TranslateUser(), sqlParameters);
        }
    }
}
