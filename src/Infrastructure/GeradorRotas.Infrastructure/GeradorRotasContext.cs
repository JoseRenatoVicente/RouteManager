using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeradorRotasMVC.Data
{
    public class GeradorRotasContext : IdentityDbContext
    {
        public GeradorRotasContext(DbContextOptions<GeradorRotasContext> options)
            : base(options)
        {

        }
        public DbSet<GeradorRotas.Domain.Entities.Cidade> Cidade { get; set; }

        public DbSet<GeradorRotas.Domain.Entities.Pessoa> Pessoa { get; set; }

        public DbSet<GeradorRotas.Domain.Entities.Equipe> Equipe { get; set; }

        public DbSet<GeradorRotas.Domain.Entities.Rota> Rota { get; set; }
    }
}
