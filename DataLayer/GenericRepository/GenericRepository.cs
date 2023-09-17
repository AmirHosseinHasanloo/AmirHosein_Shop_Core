using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private EshopContext _context;
        DbSet<TEntity> _dbSet;

        public GenericRepository(EshopContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (var include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            var GetEntity = GetById(id);
            Delete(GetEntity);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }



    }
}
