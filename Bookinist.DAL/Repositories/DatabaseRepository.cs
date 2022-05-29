using Bookinist.DAL.Context;
using Bookinist.DAL.Entities.Base;
using Bookinist.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bookinist.DAL.Repositories
{
    internal class DatabaseRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly BookinistDb _database;

        private readonly DbSet<T> _set;

        public bool IsAutoSavingEnabled { get; set; } = true;

        public DatabaseRepository(BookinistDb database)
        {
            _database = database;
            _set = _database.Set<T>();
        }

        public virtual IQueryable<T> Items => _set;

        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

        public async Task<T> GetAsync(int id, CancellationToken cancellationToken = default) =>
            await Items.SingleOrDefaultAsync(item => item.Id == id, cancellationToken)
                .ConfigureAwait(false);

        public T Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _database.Entry(item).State = EntityState.Added;

            if (IsAutoSavingEnabled)
            {
                _database.SaveChanges();
            }

            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _database.Entry(item).State = EntityState.Added;

            if (IsAutoSavingEnabled)
            {
                await _database.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            return item;
        }

        public void Update(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _database.Entry(item).State = EntityState.Modified;

            if (IsAutoSavingEnabled)
            {
                _database.SaveChanges();
            }
        }

        public async Task UpdateAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _database.Entry(item).State = EntityState.Modified;

            if (IsAutoSavingEnabled)
            {
                await _database.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public void Remove(int id)
        {
            //T item = Get(id);

            //if (item is null)
            //{
            //    return;
            //}

            //_database.Entry(item).State = EntityState.Deleted;

            T item = _set.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };

            _database.Remove(item);

            if (IsAutoSavingEnabled)
            {
                _database.SaveChanges();
            }
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            _database.Remove(new T
            {
                Id = id
            });

            if (IsAutoSavingEnabled)
            {
                await _database.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
