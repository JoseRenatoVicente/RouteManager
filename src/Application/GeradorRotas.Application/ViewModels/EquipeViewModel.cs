using GeradorRotas.Domain.Entities;
using System.Collections.Generic;

namespace GeradorRotas.Application.ViewModels
{
    public class EquipeViewModel 
    {
        public Equipe Equipe { get; set; }
        public virtual IEnumerable<Cidade> Cidades { get; set; }
        public virtual IEnumerable<Pessoa> Pessoas { get; set; }
    }
}
