using CoCoSql.Entrance;
using CoCoSql.Mapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoCoSql.Repository
{
    internal class DBHelper
    {
        public static object ExecuteScalar(string sql)
        {
            using (SqlConnection connection = new SqlConnection(CoCoSqlEntrance._ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql,connection);
                var obj = sqlCommand.ExecuteScalar(); 
                connection.Close();
                return obj;
            }
        }
    }
}
