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
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        internal static TResult SimpleMap<TIn, TResult>(TIn obj) where TResult : new()
        { 
            Type resultType = typeof(TResult);
            if (resultType == typeof(string))
            {

            }
            else if (resultType == typeof(int))
            {
                TResult t = Activator.CreateInstance<TResult>();
                var properties = resultType.GetProperties();
                Int32 cc = new Int32(); 
            }

            return default;
        }
    }
}
