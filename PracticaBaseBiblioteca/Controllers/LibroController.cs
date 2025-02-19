using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticaBaseBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace PracticaBaseBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly BibliotecaContext _BibliotecaContexto;

        public LibroController(BibliotecaContext BibliotecaContexto)
        {
            _BibliotecaContexto = BibliotecaContexto;
        }

        ///<sumary>
        ///Endpoint que retorna el listado de todos los libros existentes
        ///</sumary>
        ///<returns></returns>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {

            List<Libro> listadoLibros = (from e in _BibliotecaContexto.Libro
                                          select e).ToList();

            if (listadoLibros.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }



    }
}
