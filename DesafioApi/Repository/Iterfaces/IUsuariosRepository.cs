using DesafioApi.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Repository.Iterfaces
{
    public interface IUsuariosRepository
    {
        ICollection<Usuario> GetUsuarios();
        void AdicionaUsuario(Usuario usuario);
        void DeletaUsuario(int idUsuario);
        bool Salvar();
        Usuario GetUsuario(int idUsuario);
        string ToHash(string texto);
        bool EmailJaExiste(string email);
    }
}
