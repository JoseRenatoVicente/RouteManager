using GeradorRotas.Domain.Entities;
using GeradorRotas.Infrastructure.Repository.Base;
using GeradorRotas.Infrastructure.Repository.Interfaces;
using GeradorRotasMVC.Data;

namespace GeradorRotas.Infrastructure.Repository
{
    public class RotaRepository : BaseRepository<Rota>, IRotaRepository
    {
        public RotaRepository(GeradorRotasContext db) : base(db)
        {
        }
    }
}
