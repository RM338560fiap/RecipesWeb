using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Receitas
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        //[Range(1, Int32.MaxValue,ErrorMessage = "Valor invalido")]
        public int? CategoriaId { get; set; }
        public Categorias Categoria { get; set; }

    }
}
