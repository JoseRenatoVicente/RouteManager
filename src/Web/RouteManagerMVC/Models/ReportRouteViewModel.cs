using System.Collections.Generic;
using System.Text;

namespace RouteManagerMVC.Models
{
    public class ReportRouteViewModel
    {
        public RouteUploadRequest UploadRequest { get; set; } = new RouteUploadRequest();

        public StringBuilder TableHTML { get; set; } = new StringBuilder();

        public ExcelFileViewModel ExcelFile { get; set; }

        public List<string> Columns { get; set; }
        public IEnumerable<CityViewModel> Cities { get; set; }
        public IEnumerable<TeamRequest> Teams { get; set; }
        public IEnumerable<string> TypeSevices
        {
            get
            {
                return new string[] {
                    "INSTALAÇÃO",
                    "MUDANÇA DE ENDEREÇO",
                    "MUDANÇA DE LOCAL",
                    "MUDANCA DE PACOTE",
                    "REFAZER MANUTENÇÃO",
                    "REINSTALAÇÃO",
                    "RETORNO CREDENCIADA",
                    "SEM SINAL",
                    "TROCA DE TERMINAL",
                    "VISITA TECNICA" };
            }
        }
    }

    public class RouteUploadRequest
    {
        public string NameOS { get; set; } = "OS";
        public string NameBase { get; set; } = "BASE";
        public string NameService { get; set; } = "SERVIÇO";
        public string NameStreet { get; set; } = "ENDEREÇO";
        public string NameNumber { get; set; } = "NUMERO";
        public string NameComplement { get; set; } = "COMPLEMENTO";
        public string NameDistrict { get; set; } = "BAIRRO";
        public string NameCEP { get; set; } = "CEP";
        public string NameCity { get; set; } = "CIDADE";

        public string TypeService { get; set; }
        public CityViewModel City { get; set; }

        public string ExcelFileId { get; set; }

        public IEnumerable<string> ReportColumns { get; set; }

        public IEnumerable<string> NameTeams { get; set; }
    }
}

