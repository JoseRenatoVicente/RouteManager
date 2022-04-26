using RouteManager.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RouteManagerMVC.Models
{
    public class RouteRequestViewModel
    {

        public string Id { get; set; }

        [Required(ErrorMessage = "A Ordem de Serviço deve ser preenchido")]
        [DisplayName("Ordem de Serviço")]
        public string OS { get; set; }

        [Required(ErrorMessage = "A Base deve ser preenchido")]
        public string Base { get; set; }

        [Required(ErrorMessage = "O Tipo de Serviço deve ser preenchido")]
        [DisplayName("Tipo de Serviço")]
        public string Service { get; set; }

        [Required(ErrorMessage = "O Endereço deve ser preenchido")]
        [DisplayName("Endereço")]
        public AddressViewModel Address { get; set; }
    }
}
