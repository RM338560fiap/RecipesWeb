using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class Categorias
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string Descricao { get; set; }
        public virtual IEnumerable<Receitas> Receitas { get; set; }
    }
}
