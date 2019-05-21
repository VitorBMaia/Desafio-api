using DesafioApi.Data;
using DesafioApi.Entity;
using DesafioApi.Iterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesafioApiTeste.Repository
{
    public class PatrimonioTestRepository : IPatrimoniosRepository
    {
        private IList<Patrimonio> _patrimonios;
        private IList<Marca> _marcas;
        public PatrimonioTestRepository()
        {
            var json = File.ReadAllText(@"Mock\PatrimoniosMock.json");
            _patrimonios = JsonConvert.DeserializeObject<IList<Patrimonio>>(json);
            var json2 = File.ReadAllText(@"Mock\MarcasMock.json");
            _marcas = JsonConvert.DeserializeObject<IList<Marca>>(json);
        }
        public void AdicionaPatrimonio(Patrimonio patrimonio)
        {
            _patrimonios.Add(patrimonio);
            Salvar();
        }

        public void AtualizaPatrimonio(int tomboId, Patrimonio patrimonio)
        {
            throw new NotImplementedException();
        }

        public void DeletaPatrimonio(int tomboId)
        {
            var patrimonio = _patrimonios.FirstOrDefault(p => p.TomboId == tomboId);
            _patrimonios.Remove(patrimonio);
            Salvar();
        }

        public Patrimonio GetPatrimonio(int tomboId)
        {
            return _patrimonios.FirstOrDefault(p => p.TomboId == tomboId);
        }

        public ICollection<Patrimonio> GetPatrimonios()
        {
            return _patrimonios;
        }

        public bool MarcaExiste(int marcaId)
        {
            return _marcas.Any(m => m.MarcaId == m.MarcaId);
        }

        public bool PatrimonioJaExiste(int tomboId)
        {
            return _patrimonios.Any(p => p.TomboId == tomboId);
        }

        public bool Salvar()
        {
            try
            {
                var patrimoniosAtualizados = JsonConvert.SerializeObject(_patrimonios);
                System.IO.File.WriteAllText(@"Mock\PatrimoniosMock.json", patrimoniosAtualizados);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
