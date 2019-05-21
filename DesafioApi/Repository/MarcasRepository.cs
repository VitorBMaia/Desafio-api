using DesafioApi.Data;
using DesafioApi.Entity;
using DesafioApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DesafioApi.Repository
{
    public class MarcasRepository : IMarcasRepository
    {
        private DesafioContext _context;
        public MarcasRepository(DesafioContext context)
        {
            _context = context;
        }
        public ICollection<Marca> GetMarcas()
        {
            return _context.Marcas.OrderBy(m => m.Nome).ToList();
        }
        public Marca GetMarca(int marcaId)
        {
            return _context.Marcas.FirstOrDefault(m => m.MarcaId == marcaId);
        }

        public void AdicionaMarca(Marca marca)
        {
            _context.Marcas.Add(marca);
        }

        public void DeletaMarca(int marcaId)
        {
            var marca = _context.Marcas.FirstOrDefault(m => m.MarcaId == marcaId);

            _context.Marcas.Remove(marca);
        }

        public ICollection<Patrimonio> GetPatrimonios(int marcaId)
        {
            var patrimonios = _context.Patrimonios.Where(p => p.MarcaId == marcaId).ToList();
            return patrimonios;
        }

        public bool NomeJaExiste(string nome)
        {
            return _context.Marcas.Any(m => m.Nome == nome);
        }

        public bool MarcaJaExiste(int marcaId)
        {
            return _context.Marcas.Any(m => m.MarcaId == marcaId);
        }

        public bool NomeJaExiste(string nome, int marcaId)
        {
            return _context.Marcas.Any(m => m.Nome.ToLower() == nome.ToLower() && m.MarcaId != marcaId);
        }

        public bool Salvar()
        {
             return (_context.SaveChanges() >= 0);
        }

        public bool TemPatrimonios(int marcaId)
        {
            return _context.Patrimonios.Any(p => p.MarcaId == marcaId);
        }
    }
}
