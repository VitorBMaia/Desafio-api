using DesafioApi.Entity;
using DesafioApi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesafioApiTeste.Repository
{
    public class MarcasTestRepository : IMarcasRepository
    {
        private IList<Marca> _marcas;
        private IList<Patrimonio> _patrimonios;

        public MarcasTestRepository()
        {
            var jsonMarcas = File.ReadAllText(@"Mock\MarcasMock.json");
            var jsonPatrimonios = File.ReadAllText(@"Mock\PatrimoniosMock.json");
            _marcas =  JsonConvert.DeserializeObject<IList<Marca>>(jsonMarcas);
            _patrimonios = JsonConvert.DeserializeObject<IList<Patrimonio>>(jsonPatrimonios);

        }
        public void AdicionaMarca(Marca marca)
        {
            _marcas.Add(marca);
            var marcasAtualizadas = JsonConvert.SerializeObject(_marcas);
            System.IO.File.WriteAllText(@"Mock\MarcasMock.json", marcasAtualizadas);
        }

        public void DeletaMarca(int marcaId)
        {
            var marca = GetMarca(marcaId);
            _marcas.Remove(marca);
            var marcasAtualizadas = JsonConvert.SerializeObject(_marcas);
            System.IO.File.WriteAllText(@"Mock\MarcasMock.json", marcasAtualizadas);

        }

        public Marca GetMarca(int marcaId)
        {
            
            return _marcas.FirstOrDefault(m => m.MarcaId == marcaId);
            
        }

        public ICollection<Marca> GetMarcas()
        {
            return _marcas.ToList();
        }

        public ICollection<Patrimonio> GetPatrimonios(int marcaId)
        {
            var patrimonios = _patrimonios.Where(p => p.MarcaId == marcaId).ToList();
            return patrimonios;
        }

        public bool MarcaJaExiste(int marcaId)
        {
            
            return _marcas.Any(m => m.MarcaId == marcaId);
        }

        public bool NomeJaExiste(string nome)
        {
            return _marcas.Any(m => m.Nome == nome);
        }

        public bool NomeJaExiste(string nome, int marcaId)
        {
            return _marcas.Any(m => m.Nome == nome && m.MarcaId != marcaId);
        }

        public bool Salvar()
        {
            return true;

        }

        public bool TemPatrimonios(int marcaId)
        {
            return _patrimonios.Any(p => p.MarcaId == marcaId);
        }
    }
}
