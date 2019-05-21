using DesafioApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Iterfaces
{
    public interface IPatrimoniosRepository
    {
        ICollection<Patrimonio> GetPatrimonios();
        Patrimonio GetPatrimonio(int tomboId);
        void AdicionaPatrimonio(Patrimonio patrimonio);
        void DeletaPatrimonio(int tomboId);
        bool Salvar();
        bool MarcaExiste(int marcaId);
    }
}
