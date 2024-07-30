using c19_38_BackEnd.Dtos;
using FluentValidation;

namespace c19_38_BackEnd.Validaciones
{
    public class BibliotecaPlanUsuarioDtoValidator : AbstractValidator<BibliotecaPlanUsuarioDto>
    {
        public BibliotecaPlanUsuarioDtoValidator()
        {
            RuleFor(x => x.IdBiblioteca)
                .GreaterThan(0).WithMessage("IdBiblioteca debe ser mayor a 0.");

            RuleFor(x => x.IdPlan)
                .GreaterThan(0).WithMessage("IdPlan debe ser mayor a 0.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("IdUsuario debe ser mayor a 0.");

            RuleFor(x => x.FechaGuardado)
                .NotEmpty().WithMessage("Fecha de Guardado no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Fecha de Guardado no puede ser una fecha futura.");
        }
    }
}
