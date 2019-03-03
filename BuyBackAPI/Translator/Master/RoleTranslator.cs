using BuyBackAPI.Models.Master;
using BuyBackAPI.Utility;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Translator.Master
{
    public static class RoleTranslator
    {
        public static RoleModel TranslateRole(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
            }
            var role = new RoleModel();

            if (reader.IsColumnExists("id"))
            {
                role.Id = SqlHelper.GetNullableInt32(reader, "id");
            }

            if (reader.IsColumnExists("rolename"))
            {
                role.RoleName = SqlHelper.GetNullableString(reader, "rolename");
            }

            return role;
        }

        public static List<RoleModel> TranslateAsRolesList(this SqlDataReader reader)
        {
            var list = new List<RoleModel>();
            while (reader.Read())
            {
                list.Add(TranslateRole(reader, true));
            }
            return list;
        }
    }
}
