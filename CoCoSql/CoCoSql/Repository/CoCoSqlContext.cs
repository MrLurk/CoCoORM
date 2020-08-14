using CoCoSql.Attributer;
using CoCoSql.ExpressionExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoCoSql.Repository
{
    public class CoCoSqlContext
    {
        public static IList<T> Where<T>(Expression<Func<T, bool>> expression)
        {
            var visit = new MyExpressionVisitor();
            visit.Visit(expression);
            var sqlWhere = visit.WhereMarkUp<T>();
            var tableAttr = GetTableAttribute<T>();
            var sql = $"Select * from {tableAttr.TableName}{ sqlWhere} ;";
            Console.WriteLine(sql);
            return null;
        }

        public static T FirstOrDefault<T>(Expression<Func<T, bool>> expression)
        {
            var visit = new MyExpressionVisitor();
            visit.Visit(expression);
            var sqlWhere = visit.WhereMarkUp<T>();
            var tableAttr = GetTableAttribute<T>();
            var sql = $"Select * from {tableAttr.TableName}{ sqlWhere} ;";
            Console.WriteLine(sql);

            return default;
        }

        public static int Count<T>(Expression<Func<T, bool>> expression)
        {
            var visit = new MyExpressionVisitor();
            visit.Visit(expression);
            var sqlWhere = visit.WhereMarkUp<T>();
            var tableAttr = GetTableAttribute<T>();
            var sql = $"Select Count(*) from {tableAttr.TableName}{ sqlWhere} ;";
            Console.WriteLine(sql);

            var result = DBHelper.ExecuteScalar(sql);
            var count = Convert.ToInt32(result);
            return count;
        }


        #region 私有函数

        private static TableAttribute GetTableAttribute<T>()
        {
            var type = typeof(T);
            var objTableAttr = type.GetCustomAttributes(false).FirstOrDefault(x => x is TableAttribute);
            if (objTableAttr == null)
                throw new Exception("无法读取表特性");
            return objTableAttr as TableAttribute;
        }



        #endregion
    }
}
