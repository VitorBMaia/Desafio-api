using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Entity
{
    public class Patrimonio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TomboId { get; set; }
        [Required]
        public string Nome { get; set; }

        
        [ForeignKey("MarcaId")]
        public int MarcaId { get; set; }

        public Marca Marca { get; set; }
        public string Descricao { get; set; }


        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Patrimonio))
            {
                var patrimonio = (Patrimonio)obj;
                return
                    this.Descricao.Equals(patrimonio.Descricao) &&
                    this.MarcaId.Equals(patrimonio.MarcaId) &&
                    this.Nome.Equals(patrimonio.Nome) &&
                    this.TomboId.Equals(patrimonio.TomboId);
            }

            return false;
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int hash = 1;

            hash = hash * prime + ((this.Descricao == null) ? 0 : this.Descricao.GetHashCode());
            hash = hash * prime + ((this.MarcaId == 0) ? 0 : this.MarcaId.GetHashCode());
            hash = hash * prime + ((this.Nome == null) ? 0 : this.Nome.GetHashCode());
            hash = hash * prime + ((this.TomboId == 0) ? 0 : this.TomboId.GetHashCode());

            return hash;
        }
    }
}
