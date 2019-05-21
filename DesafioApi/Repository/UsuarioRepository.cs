using DesafioApi.Data;
using DesafioApi.Data.Entity;
using DesafioApi.Repository.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesafioApi.Repository
{
    public class UsuarioRepository : IUsuariosRepository
    {
        private readonly DesafioContext _context;
        public UsuarioRepository(DesafioContext contexto)
        {
            _context = contexto;
        }

        public void AdicionaUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            Salvar();
        }

        public void DeletaUsuario(int idUsuario)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == idUsuario);
            _context.Usuarios.Remove(usuario);
            Salvar();
        }

        public bool EmailJaExiste(string email)
        {
            return _context.Usuarios.Any(u => u.Email == email);
        }

        public Usuario GetUsuario(int idUsuario)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == idUsuario);
            return usuario;
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public bool Salvar()
        {
            return _context.SaveChanges() >= 0;
        }

        public string ToHash(string texto)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(texto);

                byte[] hash = mySHA256.ComputeHash(bytes);

                string hashString = "";

                foreach (byte b in hash)
                {
                    hashString += b.ToString("x2");
                }
                return hashString;
            }
        }


    }
}


