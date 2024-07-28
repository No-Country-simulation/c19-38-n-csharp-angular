using c19_38_BackEnd.Dtos;
using FluentValidation;

namespace c19_38_BackEnd.Validaciones
{
    public class SerieDtoValidator : AbstractValidator<SerieDto>
    {
        public SerieDtoValidator()
        {
            RuleFor(x => x.IdSerie)
                .NotEmpty().WithMessage("El Id no puede ser vacio o nulo");

            RuleFor(x => x.IdPlan)
                .NotEmpty().WithMessage("El IdPlan no puede ser vacio o nulo");

            RuleFor(x => x.IdEjercicio)
                .NotEmpty().WithMessage("El IdEjercicio no puede ser vacio o nulo");

            RuleFor(x => x.CantidadSeries)
                .NotEmpty().WithMessage("La Cantidad de Series no puede ser vacio o nulo");

            RuleFor(x => x.CantidadRepeticiones)
                .NotEmpty().WithMessage("La Cantidad de Repeticiones no puede ser vacia o nula").When(x => x.Duracion is null);

            RuleFor(x => x.Duracion)
                .NotEmpty().WithMessage("La Duracion no puede ser vacia o nula").When(x => x.CantidadRepeticiones is null);

            RuleFor(x => x.TiempoDescanso)
                .NotEmpty().WithMessage("El Tiempo de Descanso no puede ser nulo");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripcion es obligatoria");
        }
    }
}
