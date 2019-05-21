using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Model.Usuario
{
    public class UsuarioParaLoginDto
    {
        [Required(ErrorMessage = "Um email deve ser informado")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Uma senha deve ser iformada")]
        public string Senha { get; set; }
        
    }
}
