using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioApi.Data;
using DesafioApi.Entity;
using DesafioApi.Iterfaces;
using AutoMapper;
using DesafioApi.Model;
using DesafioApi.Model.Patrimonio;
using Microsoft.AspNetCore.Authorization;

namespace DesafioApi.Controllers
{
    [Route("api/patrimonios")]
    [ApiController]
    [Authorize()]
    public class PatrimoniosController : ControllerBase
    {
        private readonly IPatrimoniosRepository _repository;

        public PatrimoniosController(IPatrimoniosRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Patrimonios
        [HttpGet(Name = "GetPatrimonios")]
        public ActionResult GetPatrimonios()
        {
            var patrimonios = _repository.GetPatrimonios();
            var patrimoniosDto = new List<PatrimonioDto>();
            foreach (Patrimonio p in patrimonios)
            {
                patrimoniosDto.Add(Mapper.Map<PatrimonioDto>(p));
            }
            return Ok(patrimoniosDto);
        }

        // GET: api/Patrimonios/5
        [HttpGet("{tomboId}")]
        public ActionResult GetPatrimonio([FromRoute]int tomboId)
        {
            var patrimonio = _repository.GetPatrimonio(tomboId);

            if (patrimonio == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PatrimonioDto>(patrimonio));
        }

        //PUT: api/Patrimonios/5
        [HttpPut("{tomboId}")]
        public IActionResult PutPatrimonio([FromRoute]int tomboId, [FromBody]PatrimonioParaAtualizarDto patrimonioAtualizado)
        {
            if (patrimonioAtualizado == null)
            {
                var erro = new
                {
                    Mensagem = "E necessario informar um patrimonio",
                    StatusCode = 400
                };

                return BadRequest(erro);
            }
            if (!ModelState.IsValid)
            {
                var erro = new
                {
                    Mesagem = ModelState.Values,
                    StatusCode = 400
                };

                return BadRequest(erro);
            }

            if (!_repository.MarcaExiste(patrimonioAtualizado.MarcaId))
            {
                var erro = new
                {
                    Mensagem = "Marca informada nao encontrada.O Patrimonio deve conter uma marca existente.",
                    StatusCode = 400
                };
                return BadRequest(erro);
            }
            var patrimonio = _repository.GetPatrimonio(tomboId);
            if (patrimonio == null)
            {
                return NotFound();
            }
            Mapper.Map(patrimonioAtualizado, patrimonio);

            if (!_repository.Salvar())
            {
                var erro = new
                {
                    Mesagem = "Ocorreu um erro ao salvar as alteracoes no banco",
                    StatusCode = 500
                };

                return StatusCode(500, erro);
            }

            return NoContent();

        }

        //POST: api/Patrimonios
       [HttpPost]
        public ActionResult PostPatrimonio([FromBody]PatrimonioParaAdicionarDto patrimonioNovo)
        {
            if (patrimonioNovo == null)
            {
                var erro = new
                {
                    Mesagem = "E necessario informar um patrimonio" ,
                    StatusCode = 400
                };

                return BadRequest(erro);
            }
            if (!ModelState.IsValid)
            {
                var erro = new
                {
                    Mesagem = ModelState.Values,
                    StatusCode = 400
                };

                return BadRequest(erro);
            }

            if (!_repository.MarcaExiste(patrimonioNovo.MarcaId))
            {
                var erro = new
                {
                    Mesagem = "Marca informada nao encontrada. O Patrimonio deve conter uma marca existente.",
                    StatusCode = 400
                };
                return BadRequest(erro);
            }
            var patrimonio = Mapper.Map<Patrimonio>(patrimonioNovo);
            _repository.AdicionaPatrimonio(patrimonio);

            if (!_repository.Salvar())
            {
                var erro = new
                {
                    Mesagem = "ocorreu um erro ao salvar as alteracoes no banco",
                    StatusCode = 500
                };

                return StatusCode(500, erro);

            }

            var patrimonioCriado = Mapper.Map<PatrimonioDto>(patrimonio);

            if (Request != null)
            {
                var uri = ((Request.IsHttps) ? "https://" : "http://")
                + Request.Host.ToUriComponent()
                + Request.Path.ToUriComponent()
                + patrimonioCriado.TomboId;

            return Created(uri, patrimonioCriado);
            }

            return Created("ambiente de teste", patrimonioCriado);

        }

        // DELETE: api/Patrimonios/5
        [HttpDelete("{tomboId}")]
        public ActionResult DeletePatrimonio([FromRoute]int tomboId)
        {
            var patrimonio = _repository.GetPatrimonio(tomboId);
            if (patrimonio == null)
            {
                var erro = new
                {
                    Mensagem = "Patrimonio nao encontrado",
                    StatusCode = 404
                };
                return NotFound(erro);
            }

            _repository.DeletaPatrimonio(tomboId);

            if (!_repository.Salvar())
            {
                var erro = new
                {
                    Mesagem = "ocorreu um erro ao salvar as alteracoes no banco",
                    StatusCode = 500
                };

                return StatusCode(500, erro);
            }

            return NoContent();
        }

    }
}
