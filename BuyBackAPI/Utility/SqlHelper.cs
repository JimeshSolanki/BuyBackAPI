using System;
using System.Data.SqlClient;

namespace BuyBackAPI.Utility
{
    public static class SqlHelper
    {
        public static string ExectueProcedureReturnString(string connectionString, string procedureName, params SqlParameter[] sqlParameters)
        {
            string result = "";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procedureName;
                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }
                    sqlConnection.Open();
                    var res = command.ExecuteScalar();
                    if (res != null)
                    {
                        result = AppConstant.ToStr(res.ToString());
                    }
                }
            }
            return result;
        }

        public static TData ExecuteProcedureReturnData<TData>(string connectionString,
            string procedureName,
            Func<SqlDataReader, TData> translator,
            params SqlParameter[] sqlParameters)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procedureName;
                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }
                    sqlConnection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        TData elements = default(TData);
                        try
                        {
                            elements = translator(reader);
                        }
                        catch (Exception e)
                        {
                            e.ToString();
                        }
                        finally
                        {
                            while (reader.NextResult()) { }
                        }
                        return elements;
                    }
                }
            }
        }

        #region Get Values from Sql Data Reader
        public static string GetNullableString(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? AppConstant.ToStr("") : AppConstant.ToStr(reader[colName].ToString());
        }

        public static int? GetNullableInt32(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? 0 : Convert.ToInt32(reader[colName].ToString());
        }

        public static bool GetBoolean(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? default(bool) : Convert.ToBoolean(reader[colName].ToString());
        }

        public static bool IsColumnExists(this System.Data.IDataRecord dr, string colName)
        {
            try
            {
                return dr.GetOrdinal(colName) >= 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
