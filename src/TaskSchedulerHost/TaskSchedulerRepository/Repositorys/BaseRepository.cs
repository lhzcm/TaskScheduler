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

namespace TaskSchedulerRepository.Repositorys
{
    public abstract class BaseRepository<T, TEntity> : IRepository<TEntity> where T : DbContext where TEntity : class
    {
        private T _db;
        public BaseRepository(T db)
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
            return _db.Set<TEntity>().Where(whereCase).OrderBy(n=>n).ToList();
        }

        public virtual List<TEntity> FindAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public virtual List<TEntity> Find<TSelector>(int page, int pagesize, Expression<Func<TEntity, bool>> whereCase, out int count, Expression<Func<TEntity, TSelector>> orderby = null, bool isASC = true)
        {
            count = _db.Set<TEntity>().Where(whereCase).Count();

            IQueryable <TEntity> query = _db.Set<TEntity>().Where(whereCase);
            if (orderby != null)
            {
                if (isASC)
                {
                    query = query.OrderBy(orderby);
                }
                else
                {
                    query = query.OrderByDescending(orderby);
                }
            }
            
            return query.Skip((page - 1) * pagesize).Take(pagesize).ToList();
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

        public virtual int Update(Expression<Func<TEntity, bool>> whereCase, Expression<Func<TEntity, TEntity>> updateColumn)
        {
            return ((EntityQueryable<TEntity>)_db.Set<TEntity>().Where(whereCase)).Update(updateColumn);
        }

        public TEntity FindFirst(Expression<Func<TEntity, bool>> whereCase)
        {
            return _db.Set<TEntity>().Where(whereCase).FirstOrDefault();
        }

        public bool Exists(Expression<Func<TEntity, bool>> whereCase)
        {
            return _db.Set<TEntity>().Any(whereCase);
        }

        public T DbContext => _db;
    }
}
