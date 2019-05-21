using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Model
{
    public class MarcaDto
    {
        public MarcaDto(int marcaId, string nome)
        {
            this.Nome = nome;
            this.MarcaId = marcaId;
        }

        public string Nome { get; set; }
        public int MarcaId { get; set; }

    }
}
