using LibraryWebApi.Models;
using LibraryWebApi.Repo.Abstract;
using LibraryWebApi.Service.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Service.Concrete
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepo<Author> repo;

        public AuthorService(IRepo<Author> repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await repo.GetAll();
        }

        public void AddAuthor(Author author)
        {
            repo.Add(author);
        }

        public Author GetAuthordById(int id)
        {
            return repo.GetById(id);
        }

        public void UpdateAuthor(int id, Author author)
        {
            try
            {
                Author foundedAuthor = repo.GetById(id);
                if (foundedAuthor == null) throw new ApplicationException("Aranan id ile Author yok");

                foundedAuthor.Name = author.Name;
                foundedAuthor.SurName = author.SurName;
                foundedAuthor.Nation = author.Nation;

                repo.Update(foundedAuthor);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e.Message);
                throw new ApplicationException("Aranan id ile Author yok");
            }
        }

        public void DeleteAuthor(Author author)
        {
            repo.Delete(author);
        }
    }
}
