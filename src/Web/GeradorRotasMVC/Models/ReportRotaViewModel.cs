using System.Collections.Generic;
using System.Text;

namespace GeradorRotasMVC.Models
{
    public class ReportRotaViewModel
    {
        public string NameOS { get; set; }
        public string NameCidade { get; set; }
        public string NameBase { get; set; }
        public string NameServico { get; set; }
        public string NameRua { get; set; }
        public string NameNumero { get; set; }
        public string NameComplemento { get; set; }
        public string NameBairro { get; set; }
        public string NameCEP { get; set; }

        public List<List<string>> Table { get; set; }
        public StringBuilder TableHTML { get; set; } = new StringBuilder();
        public IEnumerable<string> Columns { get; set; }

        public IEnumerable<string> ReportColumns { get; set; }
    }
}
