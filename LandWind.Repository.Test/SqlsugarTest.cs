
using LandWind.Repository.Core;
using LandWind.Repository.Model;
using SqlSugar;
using System;
using System.Linq;
using Xunit;

namespace LandWind.Repository.Test
{
    public class SqlsugarTest
    {
        [Fact]
        public void SqlsugarBaseTest()
        {
            using (var demo = new SugarDemoManager())
            {
                try
                {
                    //var methods = typeof(SugarDemoManager).GetMethods(); 
                    demo.SearchDemo();
                    demo.InsertDemo();
                    demo.UpdateDemo();
                    demo.DeleteDemo();
                    demo.TranDemo();
                }
                catch (Exception ex)
                { 

                } 
            } 
        }
    }
}
