﻿using FluentValidation;
using RouteManager.Domain.Entities;

namespace RouteManager.Domain.Validations
{
    public class CityValidation : AbstractValidator<City>
    {
        public CityValidation()
        {

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(command => command.State)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}