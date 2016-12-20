using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Domain.Entity;

namespace Domain.Interface
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        void Add(TEntity entity);
        TEntity Create();
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        void SaveChanges();
    }
}