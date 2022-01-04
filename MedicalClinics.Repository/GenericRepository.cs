//using Microsoft.EntityFrameworkCore;
using MedicalClinics.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TripFinder.Repository
{
    public class GenericRepository<TContext, TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MedicalClinicsDbContext _DbContext;
        private readonly DbSet<TEntity> _DbSet;

        public GenericRepository(MedicalClinicsDbContext context)
        {
            _DbContext = context;
            _DbSet = _DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _DbSet;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _DbSet.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetById(long Id)
        {
            return await _DbSet.FindAsync(Id);
        }

        public async Task<TEntity> GetByEmail(string email)
        {
            return await _DbSet.FindAsync(email);
        }

        public async void AddAsync(TEntity entity)
        {
            await _DbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _DbSet.Attach(entity);
            _DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _DbSet.Remove(entity);
        }

        public void DeleteRange(IQueryable<TEntity> entity)
        {
            _DbSet.RemoveRange(entity);
        }
        
    }
}
