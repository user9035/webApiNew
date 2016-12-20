using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interface;

namespace DAL.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private FeedContext _feedContext;

        public void Add(TEntity entity)
        {
            this.EntitySet.Add(entity);
        }

        public TEntity Create()
        {
            var newEntity = this.EntitySet.Create();
            return newEntity;
        }

        public void Delete(TEntity entity)
        {
            if (this.EntitySet.Any(x => x.Id == entity.Id))
                this.EntitySet.Remove(entity);
        }

        public void Dispose()
        {
            if (this._feedContext != null)
            {
                this._feedContext.Dispose();
                this._feedContext = null;
            }
        }

        protected DbSet<TEntity> EntitySet { get; }

        public IEnumerable<TEntity> GetAll()
        {
            return this.EntitySet.ToList();
        }

        protected RepositoryBase(FeedContext feedContext)
        {
            this._feedContext = feedContext;
            this.EntitySet = feedContext.Set<TEntity>();
            var s= this._feedContext.Database.Connection.ConnectionString;
            var con = AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE").ToString();
        }

        public void SaveChanges()
        {
            this._feedContext.SaveChanges();
        }
    }
}
