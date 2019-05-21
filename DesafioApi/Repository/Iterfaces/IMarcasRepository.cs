using DesafioApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Services
{
    public interface IMarcasRepository
    {
        ICollection<Marca> GetMarcas();
        Marca GetMarca(int marcaId);
        ICollection<Patrimonio> GetPatrimonios(int marcaId);
        void AdicionaMarca(Marca marca);
        void DeletaMarca(int marcaId);
        bool NomeJaExiste(string nome);
        bool NomeJaExiste(string nome, int marcaId);
        bool MarcaJaExiste(int marcaId);
        bool Salvar();
        bool TemPatrimonios(int marcaId);



    }
}
