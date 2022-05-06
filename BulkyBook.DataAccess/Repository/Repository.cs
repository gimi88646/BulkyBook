using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal DbSet<T> dbSet;
        
        private readonly ApplicationDbContext db;
        
        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            // for instance include CoverType, Category .. case sensitivity : covertype != CoverType.
            IQueryable<T> query = dbSet;
            if (includeProperties != null) {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) { 
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            //alternate way
            // db.Products.Include(u => u.Category);
            // db.Products.Include(u => u.Category).Include(u=>u.CoverType);


            // because getFirstOrDefault is not queryable type.
            IQueryable<T> query = dbSet.Where(filter);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
