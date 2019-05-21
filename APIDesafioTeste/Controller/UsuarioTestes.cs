using DesafioApi.Controllers;
using DesafioApi.Data.Entity;
using DesafioApi.Model.Usuario;
using DesafioApiTeste.Fixture;
using DesafioApiTeste.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DesafioApiTeste.Controller
{
    [Collection("Mapper collection")]
    public class UsuarioTestes : IClassFixture<MapperFixture>
    {
        private readonly MapperFixture _mapperFixture;
        private readonly UsuariosController _controller;

        public UsuarioTestes(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
            _controller = new UsuariosController(new UsuariosTestRepository());
        }

        [Fact(DisplayName = "Get Usuarios deve retornar o status Ok")]
        public void GetUsuariosDeveRetornarOStatusOk()
        {
            var atual = _controller.GetUsuarios();

            Assert.IsType<OkObjectResult>(atual);
        }

        [Fact(DisplayName = "Get Usuarios deve retornar uma lista de UsuariosDto")]
        public void GetUsuariosDeveRetorarUmaListaDeUsuariosDto()
        {
            var ok = (OkObjectResult)_controller.GetUsuarios();
            var atual = ok.Value;

            Assert.IsType<List<UsuarioDto>>(atual);
        }

        [Fact(DisplayName = "Delete Usuarios deve retornar o status Not Found")]
        public void DeleteUsuariosDeveRetornarOStautsNotFound()
        {
            var atual = _controller.DeleteUsuarios(100);

            Assert.IsType<NotFoundObjectResult>(atual);

        }

        [Fact(DisplayName = "Delete Usuarios deve retornar o status No Content")]
        public void DeleteUsuariosDeveRetornarOStatusNoContent()
        {
            var atual = _controller.DeleteUsuarios(1);

            Assert.IsType<NoContentResult>(atual);
        }

        [Fact(DisplayName = "Delete Usuarios deve deletetar o usuario de id = 2")]
        public void DeleteUsuariosDeveDeletarOUsuariosDeId2()
        {
            var usuarios = ((OkObjectResult)_controller.GetUsuarios()).Value;
            var esperado = ((List<UsuarioDto>)usuarios).Count - 1;

            _controller.DeleteUsuarios(2);

            var usuariosAtual = ((OkObjectResult)_controller.GetUsuarios()).Value;
            var atual = ((List<UsuarioDto>)usuariosAtual).Count;

            Assert.Equal(esperado, atual);
        }

        [Fact(DisplayName = "Post Usuarios deve checar se o usuario é nulo e retornar o status Bad Request")]
        public void PostUsuariosDeveChecarSeOUsuarioENuloERetornarOStatusBadRequest()
        {
            var atual = _controller.PostUsuarios(null);

            Assert.IsType<BadRequestObjectResult>(atual);
        }

        [Fact(DisplayName = "Post Usuarios deve checar se o usuario é valido e retornar o status Bad Reqeust")]
        public void PostUsuariosDeveChecarSeOUsuarioEValidoERetornarOStatusBadRequest()
        {
            var usuarioInvalido = new UsuarioParaAdicionarDto()
            {
                Email = null,
                Nome = null,
                Senha = null
            };

            var atual = _controller.PostUsuarios(usuarioInvalido);

            Assert.IsType<BadRequestObjectResult>(atual);
        }

        [Fact(DisplayName = "Post Usuarios deve checar se o email já existe e retornar o status Bad Request")]
        public void PostUsuariosDeveChecarSeOEmailJaExisteERetornarOStatusBadRequest()
        {
            var usuarioInvalido = new UsuarioParaAdicionarDto()
            {
                Senha = "teste1",
                Nome = "teste1",
                Email = "teste1@teste.com"

            };

            var atual = _controller.PostUsuarios(usuarioInvalido);

            Assert.IsType<BadRequestObjectResult>(atual);
        }
    }

}
