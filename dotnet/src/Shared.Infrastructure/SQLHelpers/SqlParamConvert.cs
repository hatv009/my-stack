using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.SQLHelpers
{
    public class SqlParamConvert
    {
        public static List<SqlParameter> ConvertObject(object data)
        {
            var result = new List<SqlParameter>();
            if (data != null)
            {
                foreach (var propInfo in data.GetType().GetProperties())
                {
                    result.Add(new SqlParameter
                    {
                        ParameterName = "@" + propInfo.Name,
                        Value = propInfo.GetValue(data, null)
                    });
                }
            }

            return result;
        }
        public static List<SqlParameter> ConvertObject(Dictionary<string,string> data)
        {
            var result = new List<SqlParameter>();
            if (data != null)
            {
                foreach (KeyValuePair<string, string> entry in data)
                {
                    result.Add(new SqlParameter
                    {
                        ParameterName = "@" + entry.Key,
                        Value = entry.Value
                    });
                }
            }

            return result;
        }
    }
}
