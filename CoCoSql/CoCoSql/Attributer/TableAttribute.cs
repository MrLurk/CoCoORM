using System;
using System.Collections.Generic;
using System.Text;

namespace CoCoSql.Attributer
{
    public class TableAttribute : Attribute
    {
        public string TableName { get; set; }
        public TableAttribute(string tableName)
        {
            this.TableName = tableName;
        }
    }
}
