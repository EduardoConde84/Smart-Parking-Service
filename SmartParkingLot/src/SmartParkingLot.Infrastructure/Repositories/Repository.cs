using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SmartParkingLot.Infrastructure.Context;
using SmartParkingLot.Infrastructure.Context.Entities;
using SmartParkingLot.Infrastructure.Interfaces;

namespace SmartParkingLot.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AplicationDbContext _context;
        private readonly DbSet<T> _entities;

        protected Repository(AplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _entities.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() =>
            await _entities.AsNoTracking().ToListAsync();
        public virtual async Task<T?> GetByIdAsync(int? id) =>
            await _entities.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        public virtual async Task<bool> InsertAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _entities.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}