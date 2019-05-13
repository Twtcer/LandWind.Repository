using LandWind.Repository.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandWind.Repository.Core
{
    public class DbContext 
    {
        public SqlSugarClient sqlSugarClient;
        //public SimpleClient<StudentModel> StudentDb { get { return new SimpleClient<StudentModel>(sqlSugarClient); } } 

        public DbContext()
        {
            sqlSugarClient = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Server=LandWind;Database=WindCMSDB;User ID=WindCMSDBUSer;Password=cms123CMS!@#;",
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,

            });
            sqlSugarClient.Aop.OnLogExecuting = (sql, pars) => {
                Console.WriteLine($"{sql}\r\n{sqlSugarClient.Utilities.SerializeObject(pars.ToDictionary(a => a.ParameterName, a => a.Value))}");
                Console.WriteLine();
            };
        }

        public SimpleClient<T> GetEntityDB<T>() where T : class, new()
        {
            return new SimpleClient<T>(sqlSugarClient);
        }
    }
}
