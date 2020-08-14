using System;
using System.Collections.Generic;
using System.Linq;
using CoCoSql;
using CoCoSql.Entrance;
using CoCoSql.Repository;
using CoCoSqlTest.Models;

namespace CoCoSqlTest
{
    class Program
    {
        static void Main(string[] args)
        { 
            CoCoSqlEntrance sql = new CoCoSqlEntrance();
            sql.Init("server=.;uid=sa;pwd=sasa;database=CoCoORMTest");

           var count =  CoCoSqlContext.Count<Student>(x => x.Id == 1);

            Console.WriteLine($"count : { count }!");

        }
    }
}
