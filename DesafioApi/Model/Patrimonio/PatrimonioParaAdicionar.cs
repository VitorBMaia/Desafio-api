using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Model.Patrimonio
{
    public class PatrimonioParaAdicionarDto
    {
        public PatrimonioParaAdicionarDto(string nome, int marcaId, string descricao)
        {

            Nome = nome;
            MarcaId = marcaId;
            Descricao = descricao;
        }
        [Required(ErrorMessage = "Voce deveria informar um nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Voce deveria informar o id da marca")]
        public int MarcaId { get; set; }

        public string Descricao { get; set; }
    }
}
