using SqlSugar;
using System;

namespace LandWind.Repository.Model
{
    [SugarTable("Student")]
    public class StudentModel
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
