using c19_38_BackEnd.Dtos;
using FluentValidation;

namespace c19_38_BackEnd.Validaciones
{
    public class UsuarioDtoValidator : AbstractValidator<UsuarioDto>
    {
        public UsuarioDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido no puede estar vacío.")
                .MaximumLength(100).WithMessage("El apellido no puede exceder los 100 caracteres.");

            RuleFor(x => x.Genero)
                .IsInEnum().WithMessage("Genero debe ser un valor válido del enum Genero.");

            RuleFor(x => x.FechaDeNac)
                .NotEmpty().WithMessage("La fecha de nacimiento no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("El usuario debe ser mayor de 18 años.");

            RuleFor(x => x.Peso)
                .GreaterThan(0).WithMessage("El peso debe ser mayor a 0.")
                .LessThanOrEqualTo(300).WithMessage("El peso debe ser menor o igual a 300 kg.");

            RuleFor(x => x.Altura)
                .GreaterThan(0).WithMessage("La altura debe ser mayor a 0.")
                .LessThanOrEqualTo(2.5f).WithMessage("La altura debe ser menor o igual a 2.5 metros.");

            RuleFor(x => x.ActividadFisica)
                .IsInEnum().WithMessage("NivelActividadFisica debe ser un valor válido del enum NivelActividadFisica.");

            RuleFor(x => x.MediaUrl)
                .MaximumLength(2048).WithMessage("La URL del medio no puede exceder los 2048 caracteres.");

            RuleFor(x => x.Disciplina)
                .IsInEnum().WithMessage("Disciplina debe ser un valor válido del enum Disciplina.");
        }

    }
}
