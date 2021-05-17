using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string Senha { get; set; }

    }
}
