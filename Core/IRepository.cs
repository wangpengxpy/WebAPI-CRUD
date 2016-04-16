using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetList();

        /// <summary>
        /// 通过id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        int Insert(TEntity entity);


        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities"></param>
        int Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        int Delete(TEntity entity);


        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="entities"></param>
        int DeleteByRequirement(Expression<Func<TEntity, bool>> func);


        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        int Update(TEntity entity);



        /// <summary>
        /// 更新实体集合
        /// </summary>
        /// <param name="entities"></param>
        int Update(IEnumerable<TEntity> entities);

    }
}
