using DesafioApi.Data;
using DesafioApi.Entity;
using DesafioApi.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Repository
{
    public class PatrimonioRepository : IPatrimoniosRepository
    {
        private DesafioContext _context;

        public PatrimonioRepository(DesafioContext context)
        {
            _context = context;
        }

        public void AdicionaPatrimonio(Patrimonio patrimonio)
        {
            _context.Add(patrimonio);
        }

        public void DeletaPatrimonio(int tomboId)
        {
            var patrimonio = _context.Patrimonios.FirstOrDefault(p => p.TomboId == tomboId);
            _context.Patrimonios.Remove(patrimonio);
        }

        public Patrimonio GetPatrimonio(int tomboId)
        {
            return _context.Patrimonios.FirstOrDefault(p => p.TomboId == tomboId);
        }

        public ICollection<Patrimonio> GetPatrimonios()
        {
            return _context.Patrimonios.ToList();
        }

        public bool MarcaExiste(int marcaId)
        {
            return _context.Marcas.Any(m => m.MarcaId == marcaId);
        }

        public bool Salvar()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
