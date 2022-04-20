using RouteManager.Domain.Entities;
using System.Collections.Generic;

namespace RouteManager.Application.ViewModels
{
    public class EquipeViewModel 
    {
        public Equipe Equipe { get; set; }
        public virtual IEnumerable<City> Cidades { get; set; }
        public virtual IEnumerable<Pessoa> Pessoas { get; set; }
    }
}
