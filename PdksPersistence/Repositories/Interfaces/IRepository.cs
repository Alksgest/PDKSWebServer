using Microsoft.EntityFrameworkCore;

using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    internal interface IRepository<T> where T : EntityBase
    {
        int Update(T obj);
        int Delete(T obj);
        int Add(T obj);
        T GetOne(int id);
        DbSet<T> GetAll();
    }
}
