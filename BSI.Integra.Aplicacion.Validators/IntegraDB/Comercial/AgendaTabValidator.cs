using BSI.Integra.Aplicacion.DTOs.IntegraDB.Comercial;
using FluentValidation;

namespace BSI.Integra.Aplicacion.Validators.IntegraDB.Comercial
{
    public class AgendaTabValidator : AbstractValidator<AgendaTabDTO>
    {
        public AgendaTabValidator(bool isUpdate)
        {
            if (isUpdate == true)
            {
                RuleFor(x => x.Id)
                    .NotNull().WithMessage("El Id es obligatorio.");
                RuleFor(x => x.Id)
                    .Must(id => id == null || id > 0).When(x => x.Id.HasValue).WithMessage("El Id no es valido.");
            }
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El campo {PropertyName} es obligatorio")
                .MinimumLength(2).WithMessage("El campo {PropertyName} no puede tener menos de 2 caracteres")
                .MaximumLength(100).WithMessage("El campo {PropertyName} no puede tener más de 100 caracteres");

            RuleFor(x => x.CodigoAreaTrabajo)
                .NotEmpty().WithMessage("El campo {PropertyName} es obligatorio");

            RuleFor(x => x.CodigoAreaTrabajo)
                .Must(value => value == "VE" || value == "OP" || value == "PLA")
                .When(x => !string.IsNullOrEmpty(x.CodigoAreaTrabajo))
                .WithMessage("El campo {PropertyName} debe ser 'VE', 'OP' o 'PLA'.");
        }
    }
}
