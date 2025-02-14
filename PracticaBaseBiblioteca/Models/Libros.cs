using System.ComponentModel.DataAnnotations
namespace PracticaBaseBiblioteca.Models
{
    public class Libros
    {
        [Key]
        public int IdLibro { get; set; }

        public string Título { get; set; }

        public int AñoPublicacion { get; set; }

        public int IdAutor {  get; set; }

        public int IdCategoria { get; set; }

        public string Resumen {  get; set; }

    }
}
