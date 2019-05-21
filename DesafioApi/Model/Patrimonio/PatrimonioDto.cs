using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Model
{
    public class PatrimonioDto
    {
        public PatrimonioDto(int tomboId, string nome, int marcaId, string descricao)
        {
            TomboId = tomboId;
            Nome = nome;
            MarcaId = marcaId;
            Descricao = descricao;
        }
       
        public int TomboId { get; set; }
        public string Nome { get; set; }
        public int MarcaId { get; set; }
        public string Descricao { get; set; }
    }
}
