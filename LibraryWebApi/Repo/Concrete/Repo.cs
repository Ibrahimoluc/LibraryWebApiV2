using System.Drawing;
using System.Linq.Expressions;
using LibraryWebApi.Models;
using LibraryWebApi.Repo.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Repo.Concrete
{
    public class Repo<T> : IRepo<T> where T : class
    {

        private readonly Context context;

        public Repo(Context context)
        {
            this.context = context;
        }

        //orjinal yeri
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public void Add(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> func) 
        {
            return context.Set<T>().SingleOrDefault(func);
        }

        public void Update(T obj)
        {
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(T obj)
        {
            context.Set<T>().Remove(obj);
            context.SaveChanges();
        }
    }
}
