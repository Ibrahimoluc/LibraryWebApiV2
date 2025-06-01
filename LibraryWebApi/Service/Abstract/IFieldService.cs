using LibraryWebApi.Models;

namespace LibraryWebApi.Service.Abstract
{
    public interface IFieldService
    {
        public Field GetFieldById(int id);
        public Task<IEnumerable<Field>> GetAllFields();

        public void AddField(Field field);

        //return updatedField
        public void UpdateField(int id, Field field);

        public void DeleteField(Field field);
    }
}
