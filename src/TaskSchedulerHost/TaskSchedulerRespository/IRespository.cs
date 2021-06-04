using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;

namespace TaskSchedulerRespository
{
    public interface IRespository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="whereCase">查询条件</param>
        /// <returns>查询数据</returns>
        public List<TEntity> Find(Expression<Func<TEntity, bool>> whereCase);

        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns>查询数据</returns>
        public List<TEntity> FindAll();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="whereCase">查询条件</param>
        /// <returns>查询数据</returns>
        public List<TEntity> Find<TSelector>(int page, int pagesize, Expression<Func<TEntity, bool>> whereCase, out int count, Expression<Func<TEntity, TSelector>> orderby = null, bool isASC = true);

        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="whereCase">查询条件</param>
        /// <returns>查询数据</returns>
        public TEntity FindFirst(Expression<Func<TEntity, bool>> whereCase);

        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="data">插入数据信息列表</param>
        /// <returns>受影响数据量</returns>
        public int Insert(List<TEntity> data);

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <param name="data">插入数据信息</param>
        /// <returns>受影响数据量</returns>
        public int Insert(TEntity data);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <returns>受影响数据量</returns>
        public int Delete(TEntity data);

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="whereCase">删除条件</param>
        /// <returns>受影响数据量</returns>
        public int Delete(Expression<Func<TEntity, bool>> whereCase);

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <returns>受影响数据量</returns>
        public int Update(TEntity data);

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="whereCase">更新条件</param>
        /// <param name="column">更新列数据</param>
        /// <returns>受影响数据量</returns>
        public int Update(Expression<Func<TEntity, bool>> whereCase, Expression<Func<TEntity, TEntity>> updateColumn);
    }
}
