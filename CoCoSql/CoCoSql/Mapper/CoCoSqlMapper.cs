using System;
using System.Collections.Generic;
using System.Text;

namespace CoCoSql.Mapper
{
    internal class CoCoSqlMapper
    {
        /// <summary>
        /// 简单类型映射
        /// </summary>
        /// <typeparam name="TIn">传入类型</typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <returns></returns>
        internal static TResult SimpleMap<TIn, TResult>(TIn obj) where TResult : new()
        {
            Type resultType = typeof(TResult);
            if (resultType == typeof(string))
            {
                return obj.ToString();
            }
            else if (resultType == typeof(int))
            {
                return Convert.ToInt32(obj);
            }

            return default;
        }
    }
}
