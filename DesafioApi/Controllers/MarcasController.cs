using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioApi.Data;
using DesafioApi.Entity;
using DesafioApi.Services;
using DesafioApi.Model;
using AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace DesafioApi.Controllers
{
    [Route("api/marcas")]
    [ApiController]
    [Authorize()]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcasRepository _repository;

        public MarcasController(IMarcasRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Marcas
        [HttpGet(Name = "GetMarcas")]
        public ActionResult GetMarcas()
        {
            var marcas = _repository.GetMarcas();
            List<MarcaDto> marcasDto = new List<MarcaDto>();
            foreach (Marca m in marcas)
            {

                marcasDto.Add(Mapper.Map<MarcaDto>(m));
            }
            
            return Ok(marcasDto);
        }

        // GET: api/Marcas/5
        [Route("api/marcas/{id}")]
        [HttpGet("{id}")]
        public ActionResult GetMarca([FromRoute]int id)
        {
            var marca = _repository.GetMarca(id);
            if (marca == null)
            {
                return NotFound();
            }
            


            return Ok(Mapper.Map<MarcaDto>(marca));

        }

        [HttpPut("{id}")]
        public ActionResult PutMarca([FromRoute]int id, [FromBody]MarcaParaAtualizarDto marcaUpdated)
        {
            if (marcaUpdated == null)
            {
                var erro = new
                {
                    Mensagem = "E necessario informar uma marca",
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
            if (!_repository.MarcaJaExiste(id))
            {
                return NotFound();
            }
            if (_repository.NomeJaExiste(marcaUpdated.Nome, id))
            {
                var erro = new
                {
                    Mensagem = "Já existe uma marca com esse nome",
                    StatusCode = 400
                };
                return BadRequest(erro);
            }

            var marca = _repository.GetMarca(id);
            Mapper.Map(marcaUpdated, marca);

            if (!_repository.Salvar())
            {
                var erro = new
                {
                    Mesagem = "Ocorreu um erro ao salvar alteracoes no banco",
                    StatusCode = 500
                };
                return StatusCode(500, erro);
            }


            return NoContent();
            
        }

        [HttpPost]
        public ActionResult PostMarca([FromBody]MarcaParaAdicionarDto marca)
        {
            if (marca == null)
            {
                var erro = new
                {
                    Mesagem = "A marca nao pode ser nula",
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

            if (_repository.NomeJaExiste(marca.Nome))
            {
                var erro = new
                {
                    Mesagem = "Este nome de marca ja existe",
                    StatusCode = 400
                };

                return BadRequest(erro);
            }

            var marcaEntity = Mapper.Map<Marca>(marca);
            _repository.AdicionaMarca(marcaEntity);
            if (!_repository.Salvar())
            {
                var erro = new
                {
                    Mesagem = "Ocorreu um problema ao tentar salvar as alteracoes no banco",
                    StatusCode = 500
                };

                return StatusCode(500, erro);
            }

            var marcaCriada = Mapper.Map<MarcaDto>(marcaEntity);


            if (Request != null)
            {
                var uri = ((Request.IsHttps) ? "https://" : "http://")
                + Request.Host.ToUriComponent()
                + Request.Path.ToUriComponent() + "/"
                + marcaCriada.MarcaId;

                return Created(uri, marcaCriada);
            }

            return Created("ambiente de teste", marcaCriada);

        }

        [HttpDelete("{id}")]
        public  ActionResult DeleteMarca([FromRoute]int id)
        {
            if (!_repository.MarcaJaExiste(id))
            {
                return NotFound();
            }
            if (_repository.TemPatrimonios(id))
            {
                var erro = new
                {
                    Mensagem = "Essa marca possui patrimonios atrelados a ela.",
                    StatusCode = 400
                };
                return BadRequest(erro);
            }

            _repository.DeletaMarca(id);
            if (!_repository.Salvar())
            {
                var erro = new
                {
                    Mesagem = "Ocorreu um problema ao tentar salvar as alteracoes no banco.",
                    StatusCode = 500
                };

                return StatusCode(500, erro);
            }
            return NoContent();
        }

        [HttpGet("{id}/patrimonios")]
        public ActionResult GetPatrimonios([FromRoute]int id)
        {
            var marca = _repository.GetMarca(id);
            if (marca == null)
            {
                return NotFound();
            }

            var patrimonios = _repository.GetPatrimonios(id);
            var patrimoniosDto = new List<PatrimonioDto>();
            foreach (Patrimonio p in patrimonios)
            {
                patrimoniosDto.Add(Mapper.Map<PatrimonioDto>(p));
            }

            return Ok(patrimoniosDto);
        }
    }
}
