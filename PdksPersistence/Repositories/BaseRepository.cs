using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using PdksPersistence.DbContexts;
using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    public class BaseRepository<T> : IDisposable, IRepository<T> where T : EntityBase
    {

        private bool disposed = false;

        private readonly MainContext _context;
        private readonly DbSet<T> _table;

        public BaseRepository()
        {
            _context = new MainContext();
            _table = _context.Set<T>();
        }
        public int Add(T obj)
        {
            _table.Add(obj);
            return SaveChanges();
        }

        public int Delete(T obj)
        {
            _context.Entry<T>(obj).State = EntityState.Deleted;
            return SaveChanges();
        }

        public DbSet<T> GetAll()
        {
            return _table;
        }

        public T GetOne(int id)
        {
            return _table.Find(id);
        }

        public int Update(T obj)
        {
            _table.Update(obj);
            return _context.SaveChanges();
        }

        private int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Генерируется, когда возникла ошибка, связанная с параллелизмом.
                // Пока что просто сгенерировать исключение повторно,
                throw;
            }
            catch (RetryLimitExceededException)
            {
                // Генерируется, когда достигнуто максимальное количество попыток.
                // Дополнительные детали можно найти во внутреннем исключении (исключениях) .
                // Пока что просто сгенерировать исключение повторно.
                throw;
            }
            catch (DbUpdateException)
            {
                // Генерируется, когда обновление базы данных потерпело неудачу.
                // Дополнительные детали и затронутые объекты можно
                // найти во внутреннем исключении (исключениях).
                // Пока что просто сгенерировать исключение повторно,
                throw;
            }
            catch (Exception)
            {
                // Возникло какое-то другое исключение, которое должно быть обработано,
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
