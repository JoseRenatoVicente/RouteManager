using System.Collections.Generic;
using System.Text;

namespace RouteManagerMVC.Models;

public sealed record ReportRouteViewModel
{
    public RouteUploadRequest UploadRequest { get; set; } = new();

    public StringBuilder TableHTML { get; set; } = new();

    public ExcelFileViewModel ExcelFile { get; set; }

    public List<string> Columns { get; set; }
    public IEnumerable<CityViewModel> Cities { get; set; }
    public IEnumerable<TeamRequest> Teams { get; set; }
    public IEnumerable<string> TypeSevices
    {
        get
        {
            return new[] {
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

public sealed record RouteUploadRequest
{
    public string NameOS { get; init; } = "OS";
    public string NameBase { get; init; } = "BASE";
    public string NameService { get; init; } = "SERVIÇO";
    public string NameStreet { get; init; } = "ENDEREÇO";
    public string NameNumber { get; init; } = "NUMERO";
    public string NameComplement { get; init; } = "COMPLEMENTO";
    public string NameDistrict { get; init; } = "BAIRRO";
    public string NameCEP { get; init; } = "CEP";
    public string NameCity { get; init; } = "CIDADE";

    public string TypeService { get; init; }
    public CityViewModel City { get; init; }

    public string ExcelFileId { get; set; }

    public IEnumerable<string> ReportColumns { get; init; }

    public IEnumerable<string> NameTeams { get; init; }
}