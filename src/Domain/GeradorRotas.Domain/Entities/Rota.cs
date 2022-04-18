using GeradorRotas.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRotas.Domain.Entities
{
    public class Rota : EntityBase
    {
        public string OS { get; set; }
        public Cidade Cidade { get; set; }
        public string Base { get; set; }
        public string Servico { get; set; }

        public Endereco Endereco { get; set; }
    }
}
