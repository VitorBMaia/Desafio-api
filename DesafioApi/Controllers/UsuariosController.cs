using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DesafioApi.Data.Entity;
using DesafioApi.Model.Usuario;
using DesafioApi.Repository.Iterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioApi.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    [Authorize()]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _repository;
        public UsuariosController(IUsuariosRepository repostory)
        {
            _repository = repostory;
        }
        [HttpGet]
        public ActionResult GetUsuarios()
        {
            var usuarios = _repository.GetUsuarios();
            var usuariosDto = new List<UsuarioDto>();
            foreach (Usuario u in usuarios)
            {
                usuariosDto.Add(Mapper.Map<UsuarioDto>(u));
            }
            return Ok(usuariosDto);
        }

        [HttpDelete("{UsuarioId}")]
        public ActionResult DeleteUsuarios([FromRoute] int usuarioId)
        {
            var usuario = _repository.GetUsuario(usuarioId);

            if (usuario == null)
            {
                var erro = new
                {
                    Mensagem = "O usuário não foi encontrardo",
                    StatusCode = 404,
                };
                return NotFound(erro);
            }

            _repository.DeletaUsuario(usuarioId);

            if (!_repository.Salvar())
            {
                var erro = new
                {
                    Mensagem = "Ocorreu um  erro ao salvar alteracoes no banco",
                    StatusCode = 500
                };
                return StatusCode(500, erro);
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult PostUsuarios([FromBody] UsuarioParaAdicionarDto usuario)
        {
            if(usuario == null)
            {
                var erro = new
                {
                    Mensagem = "É necessario informar um usuario",
                    StatusCode = 400
                };
                return BadRequest(erro);
            }

            if (!ModelState.IsValid)
            {
                var erro = new
                {
                    Mensagem = ModelState.Values,
                    StatusCode = 400
                };
                return BadRequest(erro);
            }

            if (_repository.EmailJaExiste(usuario.Email))
            {
                var erro = new
                {
                    Mensagem = "O email informado já está cadastrado",
                    StatusCode = 400
                };
                return BadRequest(erro);
            }
            usuario.Senha = _repository.ToHash(usuario.Senha);
            var usuarioEntity = Mapper.Map<Usuario>(usuario);
            _repository.AdicionaUsuario(usuarioEntity);

            if (!_repository.Salvar())
            {
                var erro = new
                {
                    Mensagem = "Houve um erro ao tentar salvar mudancas no banco",
                    StatusCode = 500
                };
                return StatusCode(500, erro);
            }
            var usuarioCriado = Mapper.Map<UsuarioDto>(usuarioEntity);
            if (Request != null)
            {
                var uri = ((Request.IsHttps) ? "https://" : "http://")
                + Request.Host.ToUriComponent()
                + Request.Path.ToUriComponent() + "/"
                + usuarioCriado.UsuarioId;

                return Created(uri, usuarioCriado);
            }

            return Created("ambiente de teste", usuarioCriado);
        }


    }
}