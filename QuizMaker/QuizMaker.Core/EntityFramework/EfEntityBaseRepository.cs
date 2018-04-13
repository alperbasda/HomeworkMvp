using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using QuizMaker.Core.AbstractEntity;
using QuizMaker.Core.DataAccess;

namespace QuizMaker.Core.EntityFramework
{
    public class EfEntityBaseRepository<T,Context>:IEntityRepository<T> where T : class, IEntity, new()
    where Context : DbContext,new()
    {
        private Context _context;

        public EfEntityBaseRepository()
        {
       
                if(_context == null) _context = Activator.CreateInstance<Context>();
                   
        }
        public List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? _context.Set<T>().ToList() : _context.Set<T>().Where(filter).ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefault(filter);
        }

        public T Add(T entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(T entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();
            return true;
        }
    }
}