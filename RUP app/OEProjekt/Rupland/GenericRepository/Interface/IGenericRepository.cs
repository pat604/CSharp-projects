﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CurrentProjectRepository.GenericRepository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity :  class
    {

        void Create(TEntity entity);
        IEnumerable<TEntity> Read();
        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);

        void Save();

    }
}
