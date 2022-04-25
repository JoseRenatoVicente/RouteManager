using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RouteManagerMVC.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Nome de usuário")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [DisplayName("Ativo ?")]
        public bool Active { get; set; }

        [DisplayName("Função")]
        public RoleRequestViewModel Role { get; set; }

        public IEnumerable<RoleRequestViewModel> Roles { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [DisplayName("Senha Atual")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [DisplayName("Nova Senha")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Senha não confere")]
        [DisplayName("Confirme a Senha")]
        public string ConfirmPassword { get; set; }
    }


    public class UserRegister
    {
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Nome de usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Senha não confere")]
        [DisplayName("Confirme a Senha")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [DisplayName("Função")]
        public RoleRequestViewModel Role { get; set; }

        public IEnumerable<RoleRequestViewModel> Roles { get; set; }

    }

    public class UserUpdate
    {
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Nome de usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

    }


    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Password { get; set; }
    }
    public class UserResponseLogin
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}