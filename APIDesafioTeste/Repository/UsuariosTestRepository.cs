using DesafioApi.Data.Entity;
using DesafioApi.Model.Usuario;
using DesafioApi.Repository.Iterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesafioApiTeste.Repository
{
    class UsuariosTestRepository : IUsuariosRepository
    {
        private IList<Usuario> _usuarios;
        public UsuariosTestRepository()
        {
            var json = File.ReadAllText(@"Mock\UsuariosMock.json");
            _usuarios = JsonConvert.DeserializeObject<IList<Usuario>>(json);
            Salvar();
        }

        public void AdicionaUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
            Salvar();
        }

        public void DeletaUsuario(int idUsuario)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.UsuarioId == idUsuario);
            _usuarios.Remove(usuario);
            Salvar();
        }

        public bool EmailJaExiste(string email)
        {
            return _usuarios.Any(u => u.Email == email);
        }

        public Usuario GetUsuario(int idUsuario)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.UsuarioId == idUsuario);
            return usuario;
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _usuarios;
        }

        public bool Salvar()
        {
            try
            {
                var usuariosAtualizados = JsonConvert.SerializeObject(_usuarios);
                System.IO.File.WriteAllText(@"Mock\UsuariosMock.json", usuariosAtualizados);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ToHash(string texto)
        {
            throw new NotImplementedException();
        }
    }
}
