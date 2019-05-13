using LandWind.Repository.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandWind.Repository.Core
{
    public   class SugarDemoManager: DbContext,System.IDisposable
    {
        private SimpleClient<StudentModel> StudentDb;
        public SugarDemoManager()
        {
            StudentDb = GetEntityDB<StudentModel>();
        }
        //SimpleClient实现查询例子
        public void SearchDemo()
        { 
            var data1 = StudentDb.GetById(1);//根据ID查询
            var data2 = StudentDb.GetList();//查询所有
            var data3 = StudentDb.GetList(it => it.Id == 1);  //根据条件查询  
            var data4 = StudentDb.GetSingle(it => it.Id == 1);//根据条件查询一条

            var p = new PageModel() { PageIndex = 1, PageSize = 2 };// 分页查询
            var data5 = StudentDb.GetPageList(it => it.Name == "xx", p);
            Console.Write(p.PageCount);//返回总数


            // 分页查询加排序
            var data6 = StudentDb.GetPageList(it => it.Name == "xx", p, it => it.Name, OrderByType.Asc);
            Console.Write(p.PageCount);//返回总数


            //组装条件查询作为条件实现 分页查询加排序
            List<IConditionalModel> conModels = new List<IConditionalModel>
            {
                new ConditionalModel() { FieldName = "id", ConditionalType = ConditionalType.Equal, FieldValue = "1" }//id=1
            };
            var data7 = StudentDb.GetPageList(conModels, p, it => it.Name, OrderByType.Asc);

            //4.9.7.5支持了转换成queryable,我们可以用queryable实现复杂功能
            StudentDb.AsQueryable().Where(x => x.Id == 1).ToList();
        }


        //插入例子
        public void InsertDemo()
        {

            var student = new StudentModel() { Name = "jack",CreatedTime=DateTime.Now };
            var studentArray = new StudentModel[] { student };

            StudentDb.Insert(student);//插入

            StudentDb.InsertRange(studentArray);//批量插入

            var id = StudentDb.InsertReturnIdentity(student);//插入返回自增列

            //4.9.7.5我们可以转成 Insertable实现复杂插入
            //StudentDb.AsInsertable(insertObj).ExecuteCommand();
        }


        //更新例子
        public void UpdateDemo()
        {
            var student = new StudentModel() { Id = 1, Name = "jack",CreatedTime=DateTime.Now };
            var studentArray = new StudentModel[] { student };

            StudentDb.Update(student);//根据实体更新

            StudentDb.UpdateRange(studentArray);//批量更新

            StudentDb.Update(it => new StudentModel() { Name = "a", CreatedTime = DateTime.Now }, it => it.Id == 1);// 只更新Name列和CreateTime列，其它列不更新，条件id=1

            //支持StudentDb.AsUpdateable(student)
        }

        //删除例子
        public void DeleteDemo()
        {
            var student = new StudentModel() { Id = 1, Name = "jack" };

            StudentDb.Delete(student);//根据实体删除
            StudentDb.DeleteById(1);//根据主键删除
            StudentDb.DeleteById(new int[] { 1, 2 });//根据主键数组删除
            StudentDb.Delete(it => it.Id == 1);//根据条件删除

            //支持StudentDb.AsDeleteable()
        }

        //使用事务的例子
        public void TranDemo()
        { 
            var result = sqlSugarClient.Ado.UseTran(() =>
            {
                //这里写你的逻辑
            });
            if (result.IsSuccess)
            {
                //成功
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

        public void Dispose()
        {
             
        }
    }
}
