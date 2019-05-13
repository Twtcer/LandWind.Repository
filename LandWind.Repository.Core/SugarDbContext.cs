using LandWind.Repository.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandWind.Repository.Core
{
    public class SugarDbContext<T> where T:class,new()
    {
        public SqlSugarClient SqlSugarClient; 
        public SimpleClient<T> CurrentSugarClient { get { return new SimpleClient<T>(SqlSugarClient); } }

        public SugarDbContext()
        {
            SqlSugarClient = new SqlSugarClient(new ConnectionConfig() {
                ConnectionString = "Server=LandWind;Database=WindCMSDB;User ID=WindCMSDBUSer;Password=cms123CMS!@#;",
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
                
            });
            SqlSugarClient.Aop.OnLogExecuting = (sql, pars) => {
                Console.WriteLine($"{sql}\r\n{SqlSugarClient.Utilities.SerializeObject(pars.ToDictionary(a=>a.ParameterName,a=>a.Value))}");
                Console.WriteLine();
            };
        }

        public virtual List<T> GetList()
        {
            return CurrentSugarClient.GetList();
        }

        public virtual bool Delete(dynamic id)
        {
            return CurrentSugarClient.Delete(id);
        }

        public virtual bool Update(T model)
        {
            return CurrentSugarClient.Update(model);
        }
         

    }
}
