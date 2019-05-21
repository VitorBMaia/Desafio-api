using AutoMapper;
using DesafioApi.Controllers;
using DesafioApi.Entity;
using DesafioApi.Model;
using DesafioApi.Model.Patrimonio;
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
    public class PatrimonioTestes : IClassFixture<MapperFixture>
    {
        private readonly PatrimoniosController _controller;
        private MapperFixture _mapperFixture;
        public PatrimonioTestes(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
            _controller = new PatrimoniosController(new PatrimonioTestRepository());
        }

        [Fact(DisplayName = "Get Patrimonios deve retornar o status Ok")]
        void GetPatrimonioosDeveRetornarOStatusOk()
        {
            var atual = _controller.GetPatrimonios();

            Assert.IsType<OkObjectResult>(atual);
        }

        [Fact(DisplayName = "Get Patrimonio deve retornar o status Ok")]
        void GetPatrimonioDeveRetornarOStautsOk()
        {
            var atual = _controller.GetPatrimonio(0);

            Assert.IsType<OkObjectResult>(atual);
        }

        [Fact(DisplayName = "Get Patrimonio deve retornar o status Not Found")]
        void GetPatrimonioDeveRetornarOStatusNotFound()
        {
            var atual = _controller.GetPatrimonio(100);

            Assert.IsType<NotFoundResult>(atual);
        }

        [Fact(DisplayName = "Get Patrimonio deve retornar o patrimonio Computador")]
        void GetPatrimonioDeveRetornarOPatrimonioComputador()
        {
            var patrimonio = new PatrimonioDto(0, "computador", 1, "notebook");

            var esperado = Mapper.Map<Patrimonio>(patrimonio);
            var okObjectResult = (OkObjectResult)_controller.GetPatrimonio(0);
            var atual = (PatrimonioDto)okObjectResult.Value;


            Assert.Equal(esperado, Mapper.Map<Patrimonio>(atual));

        }

        [Fact(DisplayName = "Put Patrimonio deve retornar o status No Content")]
        void PutPatrimonioDeveRetornarOStatusNoContent()
        {
            var patrimonio = new PatrimonioParaAtualizarDto("computador", 1, "notebook");

            var atual = _controller.PutPatrimonio(0, patrimonio);

            Assert.IsType<NoContentResult>(atual);
        }

        [Fact(DisplayName = "Put Patrimonio deve retornar o stauts Not Found")]
        void PutPatrimonioDeveRetornarOStatusNotFound()
        {
            var patrimonio = new PatrimonioParaAtualizarDto("computador", 1, "notebook");

            var atual = _controller.PutPatrimonio(100, patrimonio);

            Assert.IsType<NotFoundResult>(atual);
        }

        [Fact(DisplayName = "Put Patrimonio deve retornar o status Bad Request")]
        void PutPatrimonioDeveRetornarOStatusBadRequest()
        {
            var atual = _controller.PutPatrimonio(1, null);

            Assert.IsType<BadRequestObjectResult>(atual);
        }

        [Fact(DisplayName = "Put Patrimonio deve atualizar o patrimonio mouse")]
        void PutPatrimonioDeveAtualziarOPatrimonioMouse()
        {
            var esperado = new PatrimonioParaAtualizarDto("mouse", 1, "com fio");

            _controller.PutPatrimonio(2, esperado);
            var okObjectResult = (OkObjectResult)_controller.GetPatrimonio(2);
            var atual = (PatrimonioDto)okObjectResult.Value;

            Assert.Equal(esperado.Descricao, atual.Descricao);
        }

        [Fact(DisplayName = "Post Patrimonio deve retornar o status BadRequest")]
        void PostPatrimonioDeveRetornarOStatusBadRequest()
        {
            var atual = _controller.PostPatrimonio(null);

            Assert.IsType<BadRequestObjectResult>(atual);
        }

        [Fact(DisplayName = "Post Patrimonio deve retornar o status Created At")]
        void PostPatrimonioDeveRetornarOStatusCreatedAt()
        {
            var patrimonio = new PatrimonioParaAdicionarDto("Patrimonio Criado", 0, "teste");
            var atual = _controller.PostPatrimonio(patrimonio);

            Assert.IsType<CreatedResult>(atual);
        }

        [Fact(DisplayName = "Post Patrimonio deve criar o patrimonio Mesa")]
        void PostPatrimonioDeveCriarOPatrimonioMesa()
        {
            var patrimonio = new PatrimonioParaAdicionarDto("Mesa", 0, "longa");
            var esperado = Mapper.Map<Patrimonio>(patrimonio);

            var createdAtRoute = (CreatedResult)_controller.PostPatrimonio(patrimonio);
            var novoPatrimonio = (PatrimonioDto)createdAtRoute.Value;
            var atual = Mapper.Map<Patrimonio>(novoPatrimonio);

            Assert.Equal(esperado, atual);
        }

        [Fact(DisplayName = "Delete Patrimonio deve retornar o status Not Found")]
        void DeletePatrimonioDEveRetornarOStatusNotFound()
        {
            var atual = _controller.DeletePatrimonio(100);

            Assert.IsType<NotFoundObjectResult>(atual);
        }

        [Fact(DisplayName = "Delete Patrimonio deve deletar o patrimonio monitor")]
        void DeletePatrimonioDeveDeletarOPatrimonioMonitor()
        {
            _controller.DeletePatrimonio(1);

            var atual = _controller.GetPatrimonio(1);

            Assert.IsType<NotFoundResult>(atual);
        }
    }
}
