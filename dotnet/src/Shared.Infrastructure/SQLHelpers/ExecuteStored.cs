using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Common;
using System.Data.Common;
using System.Reflection;
using System.Text.Json;

namespace Shared.Infrastructure.SQLHelpers
{
    public static class ExecuteStored
    {

        public static List<SqlParameter> GetSearchParams(BaseOpts options)
        {
            var sqlParams = SqlParamConvert.ConvertObject(options);
            var filter = sqlParams.SingleOrDefault(x => x.ParameterName == "@filter");
            if (filter != null)
                sqlParams.Remove(filter);
            if (!options.filter.IsEmpty())
            {
                var filterObj = JsonSerializer.Deserialize<Dictionary<string, string>>(options.filter);
                var filterParams = SqlParamConvert.ConvertObject(filterObj);

                sqlParams = sqlParams.Concat(filterParams).ToList();
            }
            return sqlParams;
        }
        public static DbCommand LoadStoredProc(this DbContext context, string storedName)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }
        public static DbCommand WithSqlParam(
         this DbCommand cmd, List<SqlParameter> parameters)
        {
            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException(
                  "Call LoadStoredProc before using this method");

            cmd.Parameters.AddRange(parameters.ToArray());
            return cmd;
        }

        private static List<T> MapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
              .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
              .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        if (prop.PropertyType.IsArray)
                            prop.SetValue(obj, val == DBNull.Value ? null : val.ToString().Split(',').Select(x => int.Parse(x)).ToArray());
                        else
                            prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static async Task<List<T>> ExecuteStoredProc<T>(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return reader.MapToList<T>();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
        public static async Task<int> ExecuteStoredProcCount(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        string totalCount = reader["totalCount"].ToString();
                        return int.Parse(totalCount);
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
    }
}
