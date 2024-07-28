using c19_38_BackEnd.Dtos;
using FluentValidation;

namespace c19_38_BackEnd.Validaciones
{
    public class EjercicioDtoValidator : AbstractValidator<EjercicioDto>
    {
        public EjercicioDtoValidator()
        {
            RuleFor(x => x.IdEjercicio)
                .GreaterThan(0).WithMessage("IdEjercicio debe ser mayor a 0.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción no puede estar vacía.")
                .MaximumLength(1000).WithMessage("La descripción no puede exceder los 1000 caracteres.");

            RuleFor(x => x.MusculoPrincipal)
                .NotEmpty().WithMessage("El músculo principal no puede estar vacío.")
                .MaximumLength(200).WithMessage("El músculo principal no puede exceder los 200 caracteres.");

            RuleFor(x => x.MusculoSecundario)
                .NotEmpty().WithMessage("El músculo secundario no puede estar vacío.")
                .MaximumLength(200).WithMessage("El músculo secundario no puede exceder los 200 caracteres.");

            RuleFor(x => x.MediaUrl)
                .NotEmpty().WithMessage("La URL no puede estar vacia")
                .MaximumLength(2048).WithMessage("La URL del medio no puede exceder los 2048 caracteres.");
        }
    }
}
