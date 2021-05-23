using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TaskSchedulerModel.Models;
using TaskSchedulerRespository.Respositorys;
using Z.EntityFramework.Plus;

namespace TaskSchedulerRespositoryTest
{
    [TestClass]
    public class TaskRespositoryTest
    {
        [TestMethod]
        public void TestFind()
        {

        }
        [TestMethod]
        public void TestInsert()
        {
            TaskRespository respository = new TaskRespository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());
            var taskInfo = new TaskInfo() { Name = "test", ExecFile = "", WriteTime = DateTime.Now, UpdateTime = DateTime.Now };
            Assert.AreEqual(respository.Insert(taskInfo), 1);
        }

        [TestMethod]
        public void TestDelete()
        {
            TaskRespository respository = new TaskRespository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());

            //var linq = respository.DbContext.TaskInfos.Where(n => n.Id == 0);
            //var test = linq as EntityQueryable<TaskInfo>;
            //test.Update(n=> new TaskInfo { Name="dss"});
            //var type = linq.GetType();

            var taskInfo = new TaskInfo() { Name = "test", ExecFile = "", WriteTime = DateTime.Now, UpdateTime = DateTime.Now };
            Assert.AreEqual(respository.Insert(taskInfo), 1);
            respository.Delete(taskInfo);

            taskInfo = new TaskInfo() { Name = "test", ExecFile = "", WriteTime = DateTime.Now, UpdateTime = DateTime.Now };
            Assert.AreEqual(respository.Insert(taskInfo), 1);
            taskInfo = new TaskInfo() { Name = "test", ExecFile = "", WriteTime = DateTime.Now, UpdateTime = DateTime.Now };
            Assert.AreEqual(respository.Insert(taskInfo), 1);
            taskInfo = new TaskInfo() { Name = "test", ExecFile = "", WriteTime = DateTime.Now, UpdateTime = DateTime.Now };
            Assert.AreEqual(respository.Insert(taskInfo), 1);
          
            Assert.AreEqual(respository.Delete(n => n.Id < taskInfo.Id && n.Id >= taskInfo.Id - 2), 2);
        }
    }
}
