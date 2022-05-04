using Identity.Domain.Contracts.v1;
using Identity.Domain.Entities.v1;
using System.Linq;

namespace Identity.API.Services
{
    public class SeederService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public SeederService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
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
                    new("Relátorio Rotas")
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

            if (!(await _userRepository.GetAllAsync()).Any())
                await _userRepository.AddAsync(userAdmin);

        }
    }
}
