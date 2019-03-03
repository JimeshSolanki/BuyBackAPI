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
            return SqlHelper.ExecuteProcedureReturnData<List<UserModel>>(connectionString, "getalluser", x => x.TranslateAsUsersList());
        }

        public UserModel GetUserById(string connectionString, int id)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@Id", id) };
            return SqlHelper.ExecuteProcedureReturnData<UserModel>(connectionString, "getuserbyid", x => x.TranslateUser(), sqlParameters);
        }

        public string ManageUser(string connectionString, UserModel user)
        {
            var outParam = new SqlParameter("@ReturnCode", System.Data.SqlDbType.NVarChar, 20)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            SqlParameter[] parameter =
            {
                new SqlParameter("@id",user.Id),
                new SqlParameter("@username",user.Username),
                new SqlParameter("@password",user.Password),
                new SqlParameter("@firstname",user.Firstname),
                new SqlParameter("@middlename",user.Middlename),
                new SqlParameter("@lastname",user.Lastname),
                new SqlParameter("@age",user.Age),
                new SqlParameter("@dateofbirth",user.DateOfBirth),
                new SqlParameter("@gender",user.Gender),
                new SqlParameter("@bio",user.Bio),
                new SqlParameter("@emailaddress",user.Emailaddress),
                new SqlParameter("@mobile",user.Mobileno),
                new SqlParameter("@profileimage",user.ProfileImage),
                new SqlParameter("@roleid",user.Roleid),
                new SqlParameter("@islocked",user.IsLocked),
                outParam
            };

            SqlHelper.ExectueProcedureReturnString(connectionString, "manageuser", parameter);
            return (string)outParam.Value;
        }
    }
}
