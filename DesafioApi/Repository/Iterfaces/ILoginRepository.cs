using DesafioApi.Model.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Repository.Iterfaces
{
    public interface ILoginRepository
    {
        bool Autentica(UsuarioParaLoginDto usuario);
    }
}
