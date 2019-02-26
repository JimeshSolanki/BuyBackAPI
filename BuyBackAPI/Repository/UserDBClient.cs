using BuyBackAPI.Models;
using BuyBackAPI.Translator;
using BuyBackAPI.Utility;
using System.Collections.Generic;

namespace BuyBackAPI.Repository
{
    public class UserDBClient
    {
        public List<UserModel> GetAllUsers(string connectionString)
        {
            return SqlHelper.ExecuteProcedureReturnData<List<UserModel>>(connectionString, "GetAllUsers", x => x.TranslateAsUsersList());
        }
    }
}
