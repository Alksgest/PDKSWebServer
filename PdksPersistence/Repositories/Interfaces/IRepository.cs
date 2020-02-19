using PdksPersistence.Models;
using System.Collections.Generic;

namespace PdksPersistence.Repositories
{
    internal interface IRepository<T> where T : EntityBase
    {
        int Update(T obj);
        int Delete(T obj);
        int Add(T obj);
        T GetOne(int id);
        IEnumerable<T> GetAll();
    }
}
