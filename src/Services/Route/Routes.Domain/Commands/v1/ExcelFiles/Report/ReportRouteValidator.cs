using FluentValidation;

namespace Routes.Domain.Commands.v1.ExcelFiles.Report;

public sealed class ReportRouteValidator : AbstractValidator<ReportRouteCommand>
{
    public ReportRouteValidator()
    {
        RuleFor(command => command.NameOS)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameBase)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameService)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameStreet)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameNumber)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameComplement)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameDistrict)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameCEP)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameCity)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.TypeService)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.ExcelFileId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(command => command.NameTeams)
            .NotEmpty().WithMessage("Selecione o nome de alguma equipe");
    }
}