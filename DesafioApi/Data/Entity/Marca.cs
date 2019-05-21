using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Entity
{
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarcaId { get; set; }

        [Required]
        public string Nome { get; set; }
        public List<Patrimonio> Patrimonios { get; set; }
        public override bool Equals(Object obj)
        {
            if (obj.GetType() == typeof(Marca))
            {
                Marca marca = (Marca) obj;

                return
                    this.MarcaId.Equals(marca.MarcaId) &&
                    this.Nome.Equals(marca.Nome);
            }

            return false;
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int hash = 1;

            hash = prime * hash + ((this.Nome == null) ? 0 : this.Nome.GetHashCode());
            hash = prime * hash + ((this.MarcaId == 0) ? 0 : this.MarcaId.GetHashCode());

            return hash;

        }
    }
}
