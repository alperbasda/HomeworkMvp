using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QuizMaker.Core.AbstractEntity;

namespace QuizMaker.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T Get(Expression<Func<T, bool>> filter,int skip);
        int GetTableCount(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
    }
}