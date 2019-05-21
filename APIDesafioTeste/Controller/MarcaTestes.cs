using AutoMapper;
using DesafioApi.Controllers;
using DesafioApi.Entity;
using DesafioApi.Model;
using DesafioApiTeste.Fixture;
using DesafioApiTeste.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace APIDesafioTeste
{

    [Collection("Mapper collection")]
    public class MarcaTestes : IClassFixture<MapperFixture>
    {
        MarcasController _controller;
        MapperFixture _mapperFixture;
        public MarcaTestes(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
            _controller = new MarcasController(new MarcasTestRepository());
        }

        [Fact(DisplayName = "Get Marca deve retornar o status OK")]
        public void GetMarcaDeveRetornarOStatusOk()
        {
            var atual = _controller.GetMarca(1);

            Assert.IsType<OkObjectResult>(atual);

        }

        [Fact(DisplayName = "Get Marca deve retornar o status Not Found")]
        public void GetMarcaDeveRetornarOStatusNotFound()
        {
            var atual = _controller.GetMarca(100);

            Assert.IsType<NotFoundResult>(atual);
        }

        [Fact(DisplayName = "Get Marca deve retornar a marca AVELL")]
        public void GetMarcaDeveRetornarAMarcaAvell()
        {
            var esperado = "AVELL";

            var atual = (OkObjectResult)_controller.GetMarca(0);

            Assert.Equal(esperado, ((MarcaDto)atual.Value).Nome);

        }


        [Fact(DisplayName = "Get Marcas deve retornar o status Ok")]
        void GetMarcasDeveRetornarOStatusOk()
        {
            var atual = _controller.GetMarcas();

            Assert.IsType<OkObjectResult>(atual);
        }

        [Fact(DisplayName = "Put Marca deve retornar o status No Content")]
        void PutMarcaDeveRetornarOStatusNoContent()
        {

            var marca = new MarcaParaAtualizarDto("Samsung");

            var atual = _controller.PutMarca(1, marca);

            Assert.IsType<NoContentResult>(atual);
        }

        [Fact(DisplayName = "Put Marca deve testar se nome ja existe e retornar o status Bad Request ")]
        void PutMarcaDeveTestarSeNomeJaExisteERetornarOStatusBadRequest()
        {
            var marca = new MarcaParaAtualizarDto("DELL");

            var atual = _controller.PutMarca(0, marca);

            Assert.IsType<BadRequestObjectResult>(atual);
        }

        [Fact(DisplayName = "Put Marca deve atualizar a marca AVELL para Samsung")]
        void PutMarcaDeveAtualizarAMarcaAVELLparaSamsung()
        {
            var marca = new MarcaParaAtualizarDto("Samsung");

            _controller.PutMarca(0, marca);

            var okObjectResult = (OkObjectResult)_controller.GetMarca(0);
            var marcaAtualizada = (MarcaDto)okObjectResult.Value;
            var marcaMapeada = Mapper.Map<Marca>(marcaAtualizada);
            var esperado = new Marca { MarcaId = 0, Nome = marca.Nome };

            
            Assert.Equal(marcaMapeada, esperado);
        }

        [Fact(DisplayName = "Post Marca deve retornar o status Created At")]
        void PostMarcaDeveRetornarOStatusCreatedAt()
        {
            var marca = new MarcaParaAdicionarDto("Microsoft");

            var atual = _controller.PostMarca(marca);

            Assert.IsType<CreatedResult>(atual);
        }

        [Fact(DisplayName = "Post Marca deve testar se o nome ja existe e retornar o stauts Bad Request")]
        void PostMarcaDeveTestarSeONomeJaExisteERetornarOStautsBadRequest()
        {
            var marca = new MarcaParaAdicionarDto("AVELL");

            var atual = _controller.PostMarca(marca);

            Assert.IsType<BadRequestObjectResult>(atual);
        }

        [Fact(DisplayName = "Post Marca deve adcionar a marca Voss")]
        void PostMarcaDeveAdicionarAMarcaVoss()
        {
            var marca = new MarcaParaAdicionarDto("Voss");

            var createdAtObjectResult = _controller.PostMarca(marca);
            var okObjectResult = (OkObjectResult)_controller.GetMarcas();
            var marcas = (IList<MarcaDto>)okObjectResult.Value;

            var atual = marcas.Any(m => m.Nome == marca.Nome);

            Assert.True(atual);
        }

        [Fact(DisplayName = "Delete Marca deve retornar o staus Not Found")]
        void DeleteMarcaDeveRetornarOStatusNotFound()
        {
            var atual = _controller.DeleteMarca(100);

            Assert.IsType<NotFoundResult>(atual);
        }

        [Fact(DisplayName = "Delete Marca deve retornar o status No Content")]
        void DeleteMarcaDeveRetornarOStatusNoContent()
        {
            var atual = _controller.DeleteMarca(2);

            Assert.IsType<NoContentResult>(atual);
        }

        [Fact(DisplayName = "Get Patrimonios deve retornar o status Ok")]
        void GetPatrimoniosDeveRetornarOStatusOk()
        {
            var atual = _controller.GetPatrimonios(0);

            Assert.IsType<OkObjectResult>(atual);
        }

        [Fact(DisplayName = "Get Patrimonios deve retornar o satus Not Found")]
        void GetPatrimoniosDeveRetornarOStatusNotFound()
        {
            var atual = _controller.GetPatrimonios(100);

            Assert.IsType<NotFoundResult>(atual);
        }

        [Fact(DisplayName = "Get Patrimonios deve retornar uma colecao de patrimoios")]
        void GetPatrimoniosDEveRetornarUmaColecaoDePatrimonios()
        {
            var okObjectResult = (OkObjectResult)_controller.GetPatrimonios(1);
            var atual = okObjectResult.Value;

            Assert.IsType<List<PatrimonioDto>>(atual);
        }



    }
}
