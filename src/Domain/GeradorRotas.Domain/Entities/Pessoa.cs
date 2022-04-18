using GeradorRotas.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeradorRotas.Domain.Entities
{
    public class Pessoa : EntityBase
    {
        public string Nome { get; set; }

        public Equipe Equipe { get; set; }

        [NotMapped]
        public virtual IEnumerable<Equipe> Equipes { get; set; }
    }
}
