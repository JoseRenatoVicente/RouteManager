using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RouteManagerMVC.Models
{
    public class PersonViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome da Pessoa precisa deve ser preenchido")]
        [MinLength(2, ErrorMessage = "O nome da Pessoa precia ter no mínimo 2 caracteres")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name + " ";
        }
    }
}
