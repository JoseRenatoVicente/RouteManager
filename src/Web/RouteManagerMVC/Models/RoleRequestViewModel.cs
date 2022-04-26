using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RouteManagerMVC.Models
{
    public class RoleViewModel
    {
        public RoleRequestViewModel RoleRequest { get; set; }
        public IEnumerable<ClaimViewModel> GetClaims { get; set; }
        public IEnumerable<string> NameClaims { get; set; }

    }

    public class RoleRequestViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "A Descrição deve ser preenchida")]
        [MinLength(2, ErrorMessage = "A Descrição precia ter no mínimo 2 caracteres")]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Acessos")]
        public IEnumerable<ClaimViewModel> Claims { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }

    public class ClaimViewModel
    {
        public ClaimViewModel(string description)
        {
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }
        public string Description { get; set; }
    }
}
