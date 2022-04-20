using RouteManager.Domain.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RouteManager.Domain.Entities
{
    public class Person : EntityBase
    {

        [Required(ErrorMessage = "O nome da Pessoa precisa deve ser preenchido")]
        [MinLength(2, ErrorMessage = "O nome da Pessoa precia ter no mínimo 2 caracteres")]
        [DisplayName("Nome")]
        public string Name { get; set; }
    }
}
