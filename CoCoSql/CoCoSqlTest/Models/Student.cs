﻿using CoCoSql.Attributer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoCoSqlTest.Models
{
    [Table("T_Student")]
    public class Student
    {
        /// <summary>
        /// 编号
        /// </summary>
        [InsertExclusion]
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
