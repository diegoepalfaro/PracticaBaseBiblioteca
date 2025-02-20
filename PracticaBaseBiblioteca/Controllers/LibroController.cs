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
        ///Endpoint que retorna los registros de libros por si id e incluyendo el nombre de su autor
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

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarLibro([FromBody] Libro libro)
        {
            try
            {
                _BibliotecaContexto.Libro.Add(libro);
                _BibliotecaContexto.SaveChanges();
                return Ok(libro);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarLibro(int id, [FromBody] Libro libroModificar)
        {
            //Para actualizar un registro primero se accede a el desde la base
            Libro? libroActual = (from e in _BibliotecaContexto.Libro
                                     where e.IdLibro == id
                                     select e).FirstOrDefault();
            //Se verifica que exista segun su ID
            if (libroActual == null)
            { return NotFound(); }

            //Si se ecuentra se altera
            libroActual.Título = libroModificar.Título;
            libroActual.AñoPublicacion = libroModificar.AñoPublicacion;
            libroActual.IdAutor = libroModificar.IdAutor;
            libroActual.IdCategoria = libroModificar.IdCategoria;
            libroActual.Resumen = libroModificar.Resumen;
            

            //Se marca como modificado y se envía
            _BibliotecaContexto.Entry(libroActual).State = EntityState.Modified;
            _BibliotecaContexto.SaveChanges();
            return Ok(libroModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarLibro(int id)
        {
            //Se obtiene el original de la base
            Libro? libro = (from e in _BibliotecaContexto.Libro
                               where e.IdLibro == id
                               select e).FirstOrDefault();
            //Verificar si existe
            if (libro == null)
                return NotFound();

            //Se elimina el registro
            _BibliotecaContexto.Libro.Attach(libro);
            _BibliotecaContexto.Libro.Remove(libro);
            _BibliotecaContexto.SaveChanges();
            return Ok(libro);
        }

    }
}
