using System.Linq.Expressions;
using LibraryWebApi.Models;

namespace LibraryWebApi.Repo.Abstract
{
    //Daha sonradan Generic yapılabilir bu kısım
    public interface IRepo<T>
    {
        T GetById(int id);
        Task<IEnumerable<T>> GetAll();

        T Get(Expression<Func<T, bool>> func);
        void Add(T obj);

        void Update(T obj);

        void Delete(T obj);
    }
}
