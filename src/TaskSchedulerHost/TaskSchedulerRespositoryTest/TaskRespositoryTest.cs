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
            TaskRespository respository = new TaskRespository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());

            var taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            respository.Insert(taskInfo);

            var list = respository.Find(n => n.Id == taskInfo.Id);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 1);

            taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            respository.Insert(taskInfo);
            taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            respository.Insert(taskInfo);
            taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            respository.Insert(taskInfo);

            list = respository.Find(1, 3, n => n.Id > 0, n => n.Id, false); 
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 3);
            Assert.IsTrue(list[0].Id > list[1].Id);
        }

        [TestMethod]
        public void TestInsert()
        {
            TaskRespository respository = new TaskRespository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());
            var taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            Assert.AreEqual(respository.Insert(taskInfo), 1);
        }

        [TestMethod]
        public void TestDelete()
        {
            TaskRespository respository = new TaskRespository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());

            var taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            Assert.AreEqual(respository.Insert(taskInfo), 1);
            respository.Delete(taskInfo);

            taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            Assert.AreEqual(respository.Insert(taskInfo), 1);
            taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            Assert.AreEqual(respository.Insert(taskInfo), 1);
            taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            Assert.AreEqual(respository.Insert(taskInfo), 1);
          
            Assert.AreEqual(respository.Delete(n => n.Id < taskInfo.Id && n.Id >= taskInfo.Id - 2), 2);
        }


        [TestMethod]
        public void TestUpdate()
        {
            TaskRespository respository = new TaskRespository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());

            var taskInfo = new TaskInfo() { Name = "test", ExecFile = "" };
            Assert.AreEqual(respository.Insert(taskInfo), 1);

            var ret = respository.Update(n => n.Id == taskInfo.Id, n => new TaskInfo { UpdateTime = DateTime.Now });
            Assert.AreEqual(ret, 1);
        }
    }
}
