using GeradorRotas.Domain.Entities;
using GeradorRotas.Infrastructure.Repository.Base;
using GeradorRotas.Infrastructure.Repository.Interfaces;
using GeradorRotasMVC.Data;

namespace GeradorRotas.Infrastructure.Repository
{
    public class EquipeRepository : BaseRepository<Equipe>, IEquipeRepository
    {
        public EquipeRepository(GeradorRotasContext db) : base(db)
        {
        }
    }
}
