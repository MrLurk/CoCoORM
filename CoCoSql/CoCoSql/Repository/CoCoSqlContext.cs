using CoCoSql.Attributer;
using CoCoSql.ExpressionExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoCoSql.Repository
{
    public class CoCoSqlContext
    {
        public static IList<T> Where<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var visit = new MyExpressionVisitor();
            visit.Visit(expression);
            var sqlWhere = visit.WhereMarkUp<T>();
            var tableAttr = GetTableAttribute<T>();
            var sql = $"Select * from {tableAttr.TableName}{ sqlWhere} ;";

            IList<T> result = GetFillDataSet<T>(sql);
            return result;
        }

        public static T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var visit = new MyExpressionVisitor();
            visit.Visit(expression);
            var sqlWhere = visit.WhereMarkUp<T>();
            var tableAttr = GetTableAttribute<T>();
            var sql = $"Select Top 1 * from {tableAttr.TableName}{ sqlWhere} ;";

            IList<T> result = GetFillDataSet<T>(sql);
            return result.FirstOrDefault();
        }

        public static int Count<T>(Expression<Func<T, bool>> expression)
        {
            var visit = new MyExpressionVisitor();
            visit.Visit(expression);
            var sqlWhere = visit.WhereMarkUp<T>();
            var tableAttr = GetTableAttribute<T>();
            var sql = $"Select Count(*) from {tableAttr.TableName}{ sqlWhere} ;";

            var result = DBHelper.ExecuteScalar(sql);
            var count = Convert.ToInt32(result);
            return count;
        }

        public static int Count<T>()
        {
            var sqlWhere = " Where 1 = 1";
            var tableAttr = GetTableAttribute<T>();
            var sql = $"Select Count(*) from {tableAttr.TableName}{ sqlWhere} ;";

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

        private static IList<T> GetFillDataSet<T>(string sql) where T : class
        {
            var dataSet = DBHelper.FillDataSet(sql);
            IList<T> result = new List<T>();
            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                var type = typeof(T);
                T instance = type.Assembly.CreateInstance(type.FullName) as T;
                foreach (var propertry in type.GetProperties())
                {
                    var propertryValue = datarow[propertry.Name];
                    propertry.SetValue(instance, propertryValue);
                }
                result.Add(instance);
            }

            return result;
        }


        #endregion
    }
}
