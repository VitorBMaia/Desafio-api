using DesafioApi.Entity;
using DesafioApi.Model;
using DesafioApi.Model.Patrimonio;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioApiTeste.Fixture
{
   public class MapperFixture : IDisposable
    {
        public MapperFixture()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Marca, MarcaDto>();
                cfg.CreateMap<MarcaDto, Marca>();
                cfg.CreateMap<MarcaParaAtualizarDto, Marca>();
                cfg.CreateMap<MarcaParaAdicionarDto, Marca>();
                cfg.CreateMap<Patrimonio, PatrimonioDto>();
                cfg.CreateMap<PatrimonioDto, Patrimonio>();
                cfg.CreateMap<PatrimonioParaAtualizarDto, Patrimonio>();
                cfg.CreateMap<PatrimonioParaAdicionarDto, Patrimonio>();
                cfg.CreateMap<PatrimonioParaAdicionarDto, PatrimonioDto>();
            });
        }
        public void Dispose()
        {
            AutoMapper.Mapper.Reset();
        }
    }
}
