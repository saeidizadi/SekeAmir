using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Configurations;
using Application.Contracts.Repository;

namespace Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SekeAminContext _context;

        public GenericRepository(SekeAminContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
          
                return null;
               
            }
           
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefault(
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (where != null)
                {
                    query = query.Where(where);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                 Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<T> LastOrDefault(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await orderBy(query).LastOrDefaultAsync(where);
        }
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            try
            {
                return await _context.Set<T>().AnyAsync(where);
            }
            catch(Exception e)
            {
                var a = e.InnerException;
                return false;
            }
        
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();

        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int skip = 0, int take = int.MaxValue)
        {
            var dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public IQueryable<T> GetAllQueryable(
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int skip = 0,
            int take = Int32.MaxValue)
        {
            var dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties
                             .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (where != null)
                query = query.Where(where);

            if (orderBy != null)
                query = orderBy(query);

            if (skip > 0)
                query = query.Skip(skip);

            if (take != int.MaxValue)
                query = query.Take(take);

            return query;
        }


        public async Task<int> GetCount(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
            {
                return await _context.Set<T>().CountAsync();
            }
            return await _context.Set<T>().CountAsync(where);
        }

        public async Task<bool> Exist(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
         
        }
    }
}