using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskSchedulerHost.Db.Models;

namespace TaskSchedulerHost.Db
{
    public class TaskSchedulerDbContext : DbContext
    {
        public TaskSchedulerDbContext(DbContextOptions options) :base(options)
        {
            //数据库迁移命令：Add-Migration InitialCreate；Update-Database
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseIdentityColumns(1,1);
        }

        public virtual DbSet<Task> Tasks { get; set; }
    }
}
