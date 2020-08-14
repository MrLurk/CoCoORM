using System;
using System.Collections.Generic;
using System.Linq;
using CoCoSql;
using CoCoSql.Entrance;
using CoCoSql.Repository;
using CoCoSqlTest.Models;
using Newtonsoft.Json;

namespace CoCoSqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CoCoSqlEntrance sql = new CoCoSqlEntrance();
            sql.Init("server=.;uid=sa;pwd=sasa;database=CoCoORMTest");

            //var count = CoCoSqlContext.Count<Student>(x => x.Id == 1);
            //var count2 = CoCoSqlContext.Count<Student>();

            //Console.WriteLine($"count : { count }!");
            //Console.WriteLine($"count2 : { count2 }!");

            //var data = CoCoSqlContext.Where<Student>(x => x.Id > 0);
            //var data2 = CoCoSqlContext.Where<Student>();
            //Console.WriteLine(JsonConvert.SerializeObject(data));
            //Console.WriteLine(JsonConvert.SerializeObject(data2));

            //var data3 = CoCoSqlContext.FirstOrDefault<Student>(x => x.Id > 0);
            //var data4 = CoCoSqlContext.FirstOrDefault<Student>();
            //Console.WriteLine(JsonConvert.SerializeObject(data3));
            //Console.WriteLine(JsonConvert.SerializeObject(data4));

            //CoCoSqlContext.Update<Student>(x => x.Id > 1, new { Age = 22 });
            //CoCoSqlContext.Update<Student>(new { Age = 20 });



        }
    }
}
