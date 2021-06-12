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
    public class TaskRepositoryTest
    {
        [TestMethod]
        public void TestFind()
        {
            TaskRepository respository = new TaskRepository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());

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

            int total = 0;
            list = respository.Find(1, 3, n => n.Id > 0, out total, n => n.Id, false); 
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 3);
            Assert.IsTrue(list[0].Id > list[1].Id);
            Assert.IsTrue(total > 0);
        }

        [TestMethod]
        public void TestInsert()
        {
            TaskRepository respository = new TaskRepository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());
            var taskInfo = new TaskInfo() { Name = "test", ExecFile = ""};
            Assert.AreEqual(respository.Insert(taskInfo), 1);
        }

        [TestMethod]
        public void TestDelete()
        {
            TaskRepository respository = new TaskRepository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());

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
            TaskRepository respository = new TaskRepository(new TaskSchedulerRespository.DbContexts.TaskSchedulerDbContext());

            var taskInfo = new TaskInfo() { Name = "test", ExecFile = "" };
            Assert.AreEqual(respository.Insert(taskInfo), 1);

            var ret = respository.Update(n => n.Id == taskInfo.Id, n => new TaskInfo { UpdateTime = DateTime.Now });
            Assert.AreEqual(ret, 1);
        }
    }
}
