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
            var a = new MyExpressionVisitor();
            a.Visit(expression);
            var sql = a.MarkUp<T>();
            Console.WriteLine(sql);
            return null;
        }

        public static T FirstOrDefault<T>(Expression<Func<T, bool>> expression)
        {
            var a = new MyExpressionVisitor();
            a.Visit(expression);
            var sql = a.MarkUp<T>();
            Console.WriteLine(sql);

            var r = DBHelper.ExecuteScalar<int>(sql);
            return default;
        }
    }
}
