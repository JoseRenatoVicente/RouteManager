using GeradorRotas.Domain.Entities.Base;

namespace GeradorRotas.Domain.Entities
{
    public class Cidade : EntityBase
    {
        public string Nome { get; set; }
        public string Estado { get; set; }
    }
}
