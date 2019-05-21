using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DesafioApi.Model.Usuario;
using DesafioApi.Repository.Iterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DesafioApi.Controllers
{ 
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _repository;
        public LoginController(IConfiguration configuartion, ILoginRepository repository)
        {
            _repository = repository;
            _configuration = configuartion;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Logar([FromBody] UsuarioParaLoginDto usuario)
        {
            if (!_repository.Autentica(usuario))
            {
                var erro = new
                {
                    Mensagem = "Usuário ou senha incorretos",
                    Status = "400"
                };

                return BadRequest(erro);
            }

            var claims = new[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email)
                };

            var chave = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(_configuration["ChaveDeSeguranca"]));

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "DesafioApi",
                audience: "Consumidor",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credenciais
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}