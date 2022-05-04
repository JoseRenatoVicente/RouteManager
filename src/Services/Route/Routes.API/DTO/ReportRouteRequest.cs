using FluentValidation;
using Routes.Domain.Entities.v1;
using System.Collections.Generic;

namespace Routes.API.DTO
{
    public class ReportRouteRequest
    {
        public string NameOS { get; set; }
        public string NameBase { get; set; }
        public string NameService { get; set; }
        public string NameStreet { get; set; }
        public string NameNumber { get; set; }
        public string NameComplement { get; set; }
        public string NameDistrict { get; set; }
        public string NameCEP { get; set; }
        public string NameCity { get; set; }

        public string TypeService { get; set; }
        public City City { get; set; }

        public string ExcelFileId { get; set; }

        public IEnumerable<string> ReportColumns { get; set; }

        public IEnumerable<string> NameTeams { get; set; }
    }
    public class ReportRouteRequestValidation : AbstractValidator<ReportRouteRequest>
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
}
