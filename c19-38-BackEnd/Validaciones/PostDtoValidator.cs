using c19_38_BackEnd.Dtos;
using FluentValidation;

namespace c19_38_BackEnd.Validaciones
{
    public class PostDtoValidator : AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {
            RuleFor(x => x.IdPost)
                .GreaterThanOrEqualTo(0).WithMessage("IdPost debe ser mayor a 0.");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título no puede estar vacío.")
                .MaximumLength(200).WithMessage("El título no puede exceder los 200 caracteres.");

            RuleFor(x => x.Cuerpo)
                .NotEmpty().WithMessage("El cuerpo no puede estar vacío.")
                .MaximumLength(5000).WithMessage("El cuerpo no puede exceder los 5000 caracteres.");

            RuleFor(x => x.FechaPublicacion)
                .NotEmpty().WithMessage("La fecha de publicación no puede estar vacía.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de publicación no puede ser una fecha futura.");

            RuleFor(x => x.MediaUrl)
                .MaximumLength(2048).WithMessage("La URL del medio no puede exceder los 2048 caracteres.");

            RuleFor(x => x.IdAutorUsuario)
                .GreaterThanOrEqualTo(0).WithMessage("IdAutorUsuario debe ser mayor a 0.");
        }

    }
}
