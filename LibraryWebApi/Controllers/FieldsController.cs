using LibraryWebApi.DTOs;
using LibraryWebApi.Models;
using LibraryWebApi.Repo.Abstract;
using LibraryWebApi.Repo.Concrete;
using LibraryWebApi.RoleServices;
using LibraryWebApi.Service.Abstract;
using LibraryWebApi.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;


namespace LibraryWebApi.Controllers
{
    //Kitap Alanlari icin; fizik, edebiyat, kimya, felsefe gibi
    [ApiController]
    [Route("[controller]")]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldService fieldService;

        public FieldsController(IFieldService fieldService) 
        {
            this.fieldService = fieldService;
        }

         
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Butun katmanlarda async ve await belirtilcek mi, Evet
            var list = await fieldService.GetAllFields();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(fieldService.GetFieldById(id));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public IActionResult Add(FieldCreateDto request)
        {
            var field = new Field();
            field.Name = request.Name;
            Console.WriteLine("field.id:" + field.Id); //0 atanıyor otamatik olarak, o da EF nin işine geliyor.

            fieldService.AddField(field);
            return Created();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Field field)
        {
            if(id != field.Id)
            {
                return BadRequest();
            }

            try
            {
                fieldService.UpdateField(id, field);
                return NoContent();
            }

            //beklenen hata, yani sonradan benim kontrol ettigim exceptionların turunu ApplicationException olarak dusunebilirsin
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            //beklenmeyen genel hata da kendisi 500 atar zateen
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var field = fieldService.GetFieldById(id);
            if(field == null)
            {
                return NotFound("Aradiginiz id ile user bulunamadi");
            }

            fieldService.DeleteField(field);
            return NoContent();
        }
    }
}
