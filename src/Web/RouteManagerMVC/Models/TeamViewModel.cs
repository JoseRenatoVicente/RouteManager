using RouteManager.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RouteManagerMVC.Models
{
    public class TeamViewModel
    {
        public TeamRequest Team { get; set; }
        public virtual IEnumerable<string> PeopleIds { get; set; }
        public virtual IEnumerable<CityViewModel> Cities { get; set; }
        public virtual IEnumerable<PersonViewModel> People { get; set; }
    }

    public class TeamRequest
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome da Equipe precisa deve ser preenchido")]
        [MinLength(2, ErrorMessage = "O nome da Equipe precia ter no mínimo 2 caracteres")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Pessoas")]
        public virtual IEnumerable<PersonViewModel> People { get; set; }
        [DisplayName("Cidade")]
        public virtual CityViewModel City { get; set; }

        public string PeopleString()
        {
            return string.Concat(People);
        }

    }
}
