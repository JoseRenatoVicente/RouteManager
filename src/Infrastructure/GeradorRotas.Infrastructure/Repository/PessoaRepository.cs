using GeradorRotas.Domain.Entities;
using GeradorRotas.Infrastructure.Repository.Base;
using GeradorRotas.Infrastructure.Repository.Interfaces;
using GeradorRotasMVC.Data;

namespace GeradorRotas.Infrastructure.Repository
{
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(GeradorRotasContext db) : base(db)
        {
        }
    }
}
