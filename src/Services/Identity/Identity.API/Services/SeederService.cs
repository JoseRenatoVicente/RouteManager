using Identity.API.Repository;
using RouteManager.Domain.Entities.Identity;

namespace Identity.API.Services
{
    public class SeederService
    {
        private readonly IRoleRepository _roleRepository;

        public SeederService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async void Seed()
        {

            Role roleAdmin = new Role
            {
                Description = "Admin",
                Claims = new Claim[]
                {
                        new("Funções"),
                        new("Usuários"),
                        new("Logs"),
                        new("Rotas"),
                        new("Relátorio Rotas"),
                        new("Equipes"),
                        new("Cidades"),
                        new("Pessoas")
                }
            };

            Role roleUser = new Role
            {
                Description = "User",
                Claims = new Claim[]
                {
                    new("Rotas")
                }
            };

            User userAdmin = new User
            {
                Name = "Usuário de teste",
                UserName = "testemvc",
                Password = "1F7FwZIJ9b172f2vFgKdOQuX7xnhsfyvbMbpdGKmpDg=",
                PasswordSalt = "+G7q9SozBRCc52jsuWdxnvgeUmZS6Vf8FWQx+Jd6Wf5b76SFgckFB3fPGExXfmcPv9MjS++fFIVJ7xLdCPssQQ==",
                Email = "teste@teste.com",
                Role = roleAdmin
            };

            if (await _roleRepository.FindAsync(c => c.Description == roleUser.Description) == null)
                await _roleRepository.AddAsync(roleUser);


            if (await _roleRepository.FindAsync(c => c.Description == roleAdmin.Description) == null)
                await _roleRepository.AddAsync(roleAdmin);


        }
    }
}
