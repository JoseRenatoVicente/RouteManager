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
                Description = "Admin"
            };

            Role roleUser = new Role
            {
                Description = "User"
            };

            if (await _roleRepository.FindAsync(c => c.Description == roleUser.Description) == null)
                await _roleRepository.AddAsync(roleUser);


            if (await _roleRepository.FindAsync(c => c.Description == roleAdmin.Description) == null) 
                await _roleRepository.AddAsync(roleAdmin);


        }
    }
}
