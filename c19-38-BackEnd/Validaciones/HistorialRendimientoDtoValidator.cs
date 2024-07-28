using c19_38_BackEnd.Dtos;
using FluentValidation;

namespace c19_38_BackEnd.Validaciones
{
    public class HistorialRendimientoDtoValidator : AbstractValidator<HistorialRendimientoDto>
    {
        public HistorialRendimientoDtoValidator()
        {
            RuleFor(x => x.IdHistorial)
                .GreaterThanOrEqualTo(0).WithMessage("IdHistorial debe ser mayor a 0.");

            RuleFor(x => x.cantFlexionesMin)
                .GreaterThanOrEqualTo(0).WithMessage("cantFlexionesMin debe ser mayor o igual a 0.");

            RuleFor(x => x.cantSentadillasMin)
                .GreaterThanOrEqualTo(0).WithMessage("cantSentadillasMin debe ser mayor o igual a 0.");

            RuleFor(x => x.CienMtsTiempo)
                .NotEmpty().WithMessage("Los cien metros en tiempo no puede estar vacío.");

            RuleFor(x => x.FechaGuardado)
                .NotEmpty().WithMessage("Fecha de Guardado no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Fecha de Guardado no puede ser una fecha futura.");

            RuleFor(x => x.Nota)
                .MaximumLength(500).WithMessage("Nota no puede exceder los 500 caracteres.");

            RuleFor(x => x.IdUsuario)
                .GreaterThanOrEqualTo(0).WithMessage("IdUsuario debe ser mayor a 0.");
        }
    }
}
