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

        ///<sumary>
        ///Endpoint que retorna los registros de una tabla filtrada por su ID
        ///</sumary>
        ///<param name="id"></param>
        ///<returns></returns>

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            var listadoLibro = (from e in _BibliotecaContexto.Libro
                                      join a in _BibliotecaContexto.Autor
                                            on e.IdAutor equals a.IdAutor
                                      where e.IdLibro == id
                                      select new
                                      {
                                        e.IdLibro,
                                        e.Título,
                                        e.AñoPublicacion,
                                        e.IdAutor,
                                        Autor= a.Nombre,
                                        e.IdCategoria,
                                        e.Resumen

                                      }).ToList();

            if (listadoLibro == null)
            {
                return NotFound();
            }
            return Ok(listadoLibro);
        }


    }
}
