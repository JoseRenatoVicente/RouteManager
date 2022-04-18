using GeradorRotas.Domain.Entities.Base;
using System.Collections.Generic;

namespace GeradorRotas.Domain.Entities
{
    public class Equipe : EntityBase
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;
        public virtual IEnumerable<Pessoa> Pessoas { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
