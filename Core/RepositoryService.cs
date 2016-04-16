using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RepositoryService<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private IDbContext Context;
        private bool IsNoTracking;

        /// <summary>
        /// 获取实体集合
        /// </summary>
        private IDbSet<TEntity> Entities
        {
            get
            {

                return this.Context.Set<TEntity>();
            }
        }

        private DbEntityEntry Entry(TEntity entity)
        {
            return this.Context.Entry<TEntity>(entity);
        }

        public RepositoryService(IDbContext context, bool isNoTracking)
        {

            this.Context = context;
            this.IsNoTracking = isNoTracking;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetList()
        {
            if (!IsNoTracking)
                return this.Entities.AsQueryable();
            else
                return this.Entities.AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// 通过id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            return Entities.Find(id);

        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        public int Insert(TEntity entity)
        {
            Entities.Add(entity);
            return this.Context.SaveChanges();
        }

        public int Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            foreach (var entity in entities)
            {
                Entities.Add(entity);
            }
            return this.Context.SaveChanges();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public int Delete(TEntity entity)
        {
            if (!IsNoTracking)
                this.Entities.Remove(entity);
            else
                this.Entities.Attach(entity);
            this.Entities.Remove(entity);
            return this.Context.SaveChanges();
        }

        public int DeleteByRequirement(Expression<Func<TEntity, bool>> func)
        {
            var list = GetList().Where(func).ToList();
            list.ForEach(e =>
            {
                if (!IsNoTracking)
                    this.Entities.Remove(e);
                else
                    this.Entities.Attach(e);
                this.Entities.Remove(e);
            });

            return this.Context.SaveChanges();
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public int Update(TEntity entity)
        {
            if (!IsNoTracking)
                return this.Context.SaveChanges();
            else
                this.Context.Entry(entity).State = EntityState.Modified;
            return this.Context.SaveChanges();
        }

        public int Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("enetities");
            if (!IsNoTracking)
                return this.Context.SaveChanges();
            else
                foreach (var t in entities)
                {
                    this.Context.Entry(t).State = EntityState.Modified;
                }

            return this.Context.SaveChanges();
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }

    }
}
