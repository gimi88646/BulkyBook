using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
 
        void Add(T entity);
        
        void Remove(T entity);
        
        void Remove(IEnumerable<T> entities);
        
        // the expression is a function with only parameter T , and returns bool, we call it as filter
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
    } 
}
