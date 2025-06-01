using System.Linq.Expressions;
using LibraryWebApi.Models;
using LibraryWebApi.Repo.Abstract;
using LibraryWebApi.Service.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Service.Concrete
{

    public class FieldService : IFieldService
    {
        //public IFieldRepo repo;
        private readonly IRepo<Field> repo;

        public FieldService(IRepo<Field> repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<Field>> GetAllFields()
        {
            return await repo.GetAll();
        }

       public Field GetFieldById(int id)
        {
            return repo.GetById(id);
        }

        public void AddField(Field field)
        {
            repo.Add(field);
        }

        public void UpdateField(int id, Field field)
        {
            try
            {
                Field foundedField = repo.GetById(id);
                if (foundedField == null) throw new ApplicationException("Aranan id ile field yok");

                foundedField.Name = field.Name;
                repo.Update(foundedField);
            }
            catch(DbUpdateConcurrencyException e){
                Console.WriteLine(e.Message);
                throw new ApplicationException("Aranan id ile field yok");
            }
        }

        public void DeleteField(Field field)
        {
            repo.Delete(field);
        }
    }
}
