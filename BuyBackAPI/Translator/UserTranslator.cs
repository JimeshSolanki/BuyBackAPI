using BuyBackAPI.Models;
using BuyBackAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BuyBackAPI.Translator
{
    public static class UserTranslator
    {
        public static UserModel TranslateUser(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
            }
            var user = new UserModel();

            if (reader.IsColumnExists("id"))
            {
                user.Id = SqlHelper.GetNullableInt32(reader, "id");
            }

            if (reader.IsColumnExists("username"))
            {
                user.Username = SqlHelper.GetNullableString(reader, "username");
            }

            if (reader.IsColumnExists("password"))
            {
                user.Password = SqlHelper.GetNullableString(reader, "password");
            }

            if (reader.IsColumnExists("firstname"))
            {
                user.Firstname = SqlHelper.GetNullableString(reader, "firstname");
            }

            if (reader.IsColumnExists("middlename"))
            {
                user.Middlename = SqlHelper.GetNullableString(reader, "middlename");
            }

            if (reader.IsColumnExists("lastname"))
            {
                user.Lastname = SqlHelper.GetNullableString(reader, "lastname");
            }

            if (reader.IsColumnExists("dateofbirth"))
            {
                user.DateOfBirth = Convert.ToDateTime(SqlHelper.GetNullableString(reader, "dateofbirth"));
            }

            if (reader.IsColumnExists("age"))
            {
                user.Age = SqlHelper.GetNullableInt32(reader, "age");
            }

            if (reader.IsColumnExists("mobileno"))
            {
                user.Mobileno = SqlHelper.GetNullableString(reader, "mobileno");
            }

            if (reader.IsColumnExists("emailaddress"))
            {
                user.Emailaddress = SqlHelper.GetNullableString(reader, "emailaddress");
            }

            if (reader.IsColumnExists("islocked"))
            {
                user.IsLocked = SqlHelper.GetNullableInt32(reader, "islocked");
            }

            if (reader.IsColumnExists("roleid"))
            {
                user.Roleid = SqlHelper.GetNullableInt32(reader, "roleid");
            }

            if (reader.IsColumnExists("profileimage"))
            {
                user.ProfileImage= SqlHelper.GetNullableString(reader, "profileimage");
            }

            return user;
        }

        public static List<UserModel> TranslateAsUsersList(this SqlDataReader reader)
        {
            var list = new List<UserModel>();
            while (reader.Read())
            {
                list.Add(TranslateUser(reader, true));
            }
            return list;
        }
    }
}
