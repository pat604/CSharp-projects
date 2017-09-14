using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using CurrentProject.Database;
using CurrentProjectRepository.GenericRepository.Interface;
using System.Linq.Expressions;

namespace CurrentProjectRepository.GenericRepository
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private readonly DatabaseEntities dbContext;

        public GenericRepository()
        {
            dbContext = new DatabaseEntities();
        }

        public void Create(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> Read()
        {
            return dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate);
        }

        public TEntity GetById(int id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(int id)
        {
            Delete(GetById(id));
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

    }
}
