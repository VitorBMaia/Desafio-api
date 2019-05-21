using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Model
{
    public class MarcaParaAtualizarDto
    {
        public MarcaParaAtualizarDto(string nome)
        {
            this.Nome = nome;
        }
        [Required(ErrorMessage = "Voce deveria informar um nome")]
        public string Nome { get; set; }


        public bool Valida()
        {
            return !(Nome == null || Nome == "");
        }
    }
}
