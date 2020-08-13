using System;
using System.Collections.Generic;
using System.Text;

namespace CoCoSql.Entrance
{
    public class CoCoSqlEntrance
    {
        internal static string _ConnectionString = string.Empty;
        public  void Init(string connectionString)
        {
            _ConnectionString = connectionString;
        }
    }
}
