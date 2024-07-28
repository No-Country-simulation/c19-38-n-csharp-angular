using c19_38_BackEnd.Dtos;
using FluentValidation;

namespace c19_38_BackEnd.Validaciones
{
    public class PlanDeEntrenamientoDtoValidator : AbstractValidator<PlanDeEntrenamientoDto>
    {
        public PlanDeEntrenamientoDtoValidator()
        {
            RuleFor(x => x.IdPlan)
                .GreaterThanOrEqualTo(0).WithMessage("IdPlan debe ser mayor a 0.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción no puede estar vacía.")
                .MaximumLength(1000).WithMessage("La descripción no puede exceder los 1000 caracteres.");

            RuleFor(x => x.TipoDisciplina)
                .IsInEnum().WithMessage("TipoDisciplina debe ser un valor válido del enum Disciplina.");

            RuleFor(x => x.Nivel)
                .IsInEnum().WithMessage("Nivel debe ser un valor válido del enum Nivel.");

            RuleFor(x => x.FechaPublicacion)
                .NotEmpty().WithMessage("La fecha de publicación no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de publicación no puede ser una fecha futura.");

            RuleFor(x => x.IdAutorUsuario)
                .GreaterThanOrEqualTo(0).WithMessage("IdAutorUsuario debe ser mayor a 0.");
        }
    }
}
