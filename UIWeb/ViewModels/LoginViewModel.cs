using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dominio.Models;

namespace UIWeb.ViewModels
{
    public class LoginViewModel 
    {
        [Display(Name = "Nome")]
        public string vwNome { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Usuario")]
        public string vwUsuario { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Senha")]
        public string vwSenha { get; set; }

        public string vwUrlRetorno { get; set; }

        public string Hashpwd(string input)
        {
            string sSalt = "666999@rEcIpEsnEt";
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input + sSalt);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}
