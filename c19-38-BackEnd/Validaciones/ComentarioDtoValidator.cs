using c19_38_BackEnd.Dtos;
using FluentValidation;

namespace c19_38_BackEnd.Validaciones
{
    public class ComentarioDtoValidator : AbstractValidator<ComentarioDto>
    {
        public ComentarioDtoValidator()
        {
            RuleFor(x => x.IdComentario)
                .NotEmpty().WithMessage("El IdComentario no puede ser vacío o nulo");

            RuleFor(x => x.Cuerpo)
                .NotEmpty().WithMessage("El cuerpo del comentario no puede ser vacío")
                .MaximumLength(500).WithMessage("El cuerpo del comentario no puede exceder los 500 caracteres");

            RuleFor(x => x.IdPost)
                .NotEmpty().WithMessage("El IdPost no puede ser vacío o nulo");

            RuleFor(x => x.IdAutor)
                .NotEmpty().WithMessage("El IdAutor no puede ser vacío o nulo");

            RuleFor(x => x.FechaPublicacion)
                .NotEmpty().WithMessage("La FechaPublicacion no puede ser vacía o nula")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La FechaPublicacion no puede ser en el futuro");
        }
    }
}