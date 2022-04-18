using GeradorRotas.Domain.Entities;
using GeradorRotas.Infrastructure.Repository.Base;
using GeradorRotas.Infrastructure.Repository.Interfaces;
using GeradorRotasMVC.Data;

namespace GeradorRotas.Infrastructure.Repository
{
    public class CidadeRepository : BaseRepository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(GeradorRotasContext db) : base(db)
        {
        }
    }
}
