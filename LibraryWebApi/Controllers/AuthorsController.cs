using LibraryWebApi.DTOs;
using LibraryWebApi.Models;
using LibraryWebApi.RoleServices;
using LibraryWebApi.Service.Abstract;
using LibraryWebApi.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;
        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await authorService.GetAllAuthors();
            //if(list == null)
            //{
            //    Console.WriteLine("Author yok");
            //    return NotFound();
            //}

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = authorService.GetAuthordById(id);

            return Ok(author);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public IActionResult Add(AuthorCreateDto request)
        {
            var author = new Author();
            author.Name = request.Name;
            author.SurName = request.SurName;
            author.Nation = request.Nation;

            authorService.AddAuthor(author);
            return Created();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Author author)
        {
            if(id != author.Id)
            {
                return BadRequest();
            }
            try
            {
                authorService.UpdateAuthor(id, author);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var field = authorService.GetAuthordById(id);
            if (field == null)
            {
                return NotFound("Aradiginiz id ile author bulunamadi");
            }

            authorService.DeleteAuthor(field);
            return NoContent();
        }
    }
}
