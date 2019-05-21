using DesafioApi.Data;
using DesafioApi.Model.Usuario;
using DesafioApi.Repository.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesafioApi.Repository
{
    public class LoginRepository : ILoginRepository
    {
        DesafioContext _contexto;
        public LoginRepository(DesafioContext contexto)
        {
            _contexto = contexto;
        }

        public bool Autentica(UsuarioParaLoginDto usuario)
        {
            var usuarioEncontrado = _contexto.Usuarios.FirstOrDefault(u => u.Email == usuario.Email);

            if(usuarioEncontrado == null)
            {
                return false;
            }

            if (Hash(usuario.Senha) != usuarioEncontrado.Senha)
            {
                return false;
            }

            return true;
        }

        public string Hash(string texto)
        {
            
            SHA256 mySHA256 = SHA256.Create();
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
