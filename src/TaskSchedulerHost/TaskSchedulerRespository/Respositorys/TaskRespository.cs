using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRespository.DbContexts;
using Z.EntityFramework.Plus;

namespace TaskSchedulerRespository.Respositorys
{
    public class TaskRespository : BaseRespository<TaskSchedulerDbContext, TaskInfo>
    {
        public TaskRespository(TaskSchedulerDbContext db) : base(db)
        {
        }

        public override int Delete(TaskInfo data)
        {
            data.Status = -1;
            DbContext.TaskInfos.Update(data);
            return DbContext.SaveChanges();
        }

        public override int Delete(Expression<Func<TaskInfo, bool>> whereCase)
        {

            return DbContext.TaskInfos.Where(whereCase).Update(n => new TaskInfo { Status = -1 });
        }

        public override List<TaskInfo> Find(Expression<Func<TaskInfo, bool>> whereCase)
        {
            Expression<Func<TaskInfo, bool>> whereCase2 = n => n.Status == 0;
           
            var invokedExpression = Expression.Invoke(whereCase2, whereCase.Parameters.Cast<Expression>());
            whereCase = Expression.Lambda<Func<TaskInfo, bool>>(Expression.AndAlso(whereCase.Body,invokedExpression), whereCase.Parameters);

            return DbContext.TaskInfos.Where(whereCase).ToList();
        }

        public override List<TaskInfo> FindAll()
        {
            return DbContext.TaskInfos.Where(n => n.Status == 0).ToList();
        }

    }
}
