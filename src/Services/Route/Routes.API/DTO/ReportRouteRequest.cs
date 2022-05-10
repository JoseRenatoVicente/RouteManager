using FluentValidation;
using Routes.Domain.Entities.v1;
using System.Collections.Generic;

namespace Routes.API.DTO;

public class ReportRouteRequest
{
    public string NameOS { get; init; }
    public string NameBase { get; init; }
    public string NameService { get; init; }
    public string NameStreet { get; init; }
    public string NameNumber { get; init; }
    public string NameComplement { get; init; }
    public string NameDistrict { get; init; }
    public string NameCEP { get; init; }
    public string NameCity { get; init; }

    public string TypeService { get; init; }
    public City City { get; init; }

    public string ExcelFileId { get; init; }

    public IEnumerable<string> ReportColumns { get; init; }

    public IEnumerable<string> NameTeams { get; init; }
}
public sealed class ReportRouteRequestValidation : AbstractValidator<ReportRouteRequest>
{
    public ReportRouteRequestValidation()
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