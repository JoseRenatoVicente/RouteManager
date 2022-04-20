using RouteManager.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RouteManager.Domain.Entities
{
    public class Team : EntityBase
    {
        [Required(ErrorMessage = "O nome da Equipe precisa deve ser preenchido")]
        [MinLength(2, ErrorMessage = "O nome da Equipe precia ter no mínimo 2 caracteres")]
        [DisplayName("Nome")]
        public string Name { get; set; }
        public virtual IEnumerable<Person> People { get; set; }
        public virtual City City { get; set; }
    }
}