using BuyBackAPI.Models.Master;
using BuyBackAPI.Translator.Master;
using BuyBackAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Repository.Master
{
    public class RoleDBClient
    {
        public List<RoleModel> GetAllRoles(string connectionString)
        {
            return SqlHelper.ExecuteProcedureReturnData<List<RoleModel>>(connectionString, "getallrole", x => x.TranslateAsRolesList());
        }

        public RoleModel GetRoleById(string connectionString, int id)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@Id", id) };
            return SqlHelper.ExecuteProcedureReturnData<RoleModel>(connectionString, "getrolebyid", x => x.TranslateRole(), sqlParameters);
        }

        public string ManageRole(string connectionString, RoleModel role)
        {
            var outParam = new SqlParameter("@ReturnCode", System.Data.SqlDbType.NVarChar, Int32.MaxValue)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            SqlParameter[] parameter =
            {
                new SqlParameter("@id",role.Id),
                new SqlParameter("@rolename",role.RoleName),
                outParam
            };

            SqlHelper.ExectueProcedureReturnString(connectionString, "managerole", parameter);
            return (string)outParam.Value;
        }
    }
}
