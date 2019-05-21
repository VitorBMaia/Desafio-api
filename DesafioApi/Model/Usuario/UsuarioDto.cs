using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Model.Usuario
{
    public class UsuarioDto
    {
        public UsuarioDto()
        {

        }

        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
    }
}
