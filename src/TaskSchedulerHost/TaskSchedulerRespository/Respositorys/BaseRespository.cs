using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using Z.EntityFramework.Extensions.EFCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;

namespace TaskSchedulerRespository.Respositorys
{
    public abstract class BaseRespository<T, TEntity> : IRespository<TEntity> where T : DbContext where TEntity : class
    {
        private T _db;
        public BaseRespository(T db)
        {
            this._db = db;
        }

        public virtual int Delete(TEntity data) 
        {
            _db.Set<TEntity>().Remove(data);
            return _db.SaveChanges();
        }

        public virtual int Delete(Expression<Func<TEntity, bool>> whereCase)
        {
            IEnumerable<TEntity> entitys =  _db.Set<TEntity>().Where(whereCase);
            _db.Set<TEntity>().RemoveRange(entitys);
            return _db.SaveChanges();
        }

        public virtual List<TEntity> Find(Expression<Func<TEntity, bool>> whereCase)
        {
            return _db.Set<TEntity>().Where(whereCase).ToList();
        }

        public virtual List<TEntity> Find(int page, int pagesize, Expression<Func<TEntity, bool>> whereCase)
        {
            return _db.Set<TEntity>().Where(whereCase).Skip(page * pagesize).Take(pagesize).ToList();
        }

        public virtual int Insert(List<TEntity> data)
        { 
            _db.Set<TEntity>().AddRange(data);
            return _db.SaveChanges();
        }

        public virtual int Insert(TEntity data)
        {
            _db.Set<TEntity>().Add(data);
            return _db.SaveChanges();
        }

        public virtual int Update(TEntity data)
        {
            _db.Set<TEntity>().Update(data);
            return _db.SaveChanges();
        }

        public virtual int Update(Expression<Func<TEntity, bool>> whereCase, TEntity updateColumn)
        {
           
            return ((EntityQueryable<TEntity>)_db.Set<TEntity>().Where(whereCase)).Update(n => updateColumn);
        }

        public T DbContext => _db;
    }
}
