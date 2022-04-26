﻿using FluentValidation;
using RouteManager.Domain.Entities.Identity;

namespace RouteManager.Domain.Validations.Identity
{
    public class RoleValidation : AbstractValidator<Role>
    {
        public RoleValidation()
        {
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
