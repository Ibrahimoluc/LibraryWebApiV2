using LibraryWebApi.Models;

namespace LibraryWebApi.Service.Abstract
{
    public interface IAuthorService
    {
        public Author GetAuthordById(int id);
        public Task<IEnumerable<Author>> GetAllAuthors();

        public void AddAuthor(Author author);

        public void UpdateAuthor(int id, Author author);

        public void DeleteAuthor(Author author);
    }
}
