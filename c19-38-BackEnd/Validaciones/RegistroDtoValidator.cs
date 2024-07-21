using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Modelos;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace c19_38_BackEnd.Validaciones
{
    public class RegistroDtoValidator : AbstractValidator<RegistroDto>
    {
        private readonly UserManager<Usuario> _userManager;
        public RegistroDtoValidator(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacio")
                .NotNull().WithMessage("El campo Nombre no puede estar vacio");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El campo Apellido no puede estar vacio")
                .NotNull().WithMessage("El campo Apellido no puede estar vacio");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Formato de correo invalido")
                .MustAsync(NoExisteUsuarioConEmail).WithMessage("El correo electronico ya esta en uso");

            RuleFor(x => x.Peso)
                .NotEmpty().WithMessage("El campo Peso no puede estar vacio")
                .NotNull().WithMessage("El campo Peso no puede estar vacio")
                .GreaterThan(0).WithMessage("El campo Peso no puede ser menor o igual a 0");

            RuleFor(x => x.Altura)
                .NotEmpty().WithMessage("El campo Altura no puede estar vacio")
                .NotNull().WithMessage("El campo Altura no puede estar vacio")
                .GreaterThan(0).WithMessage("El campo Altura no puede ser menor o igual a 0");

            RuleFor(x => x.FechaNacimiento)
                .Must(fecha =>
                {
                    DateTime fechaActual = DateTime.Today;
                    int edad = fechaActual.Year - fecha.Year;

                    // Verifica si la fecha de nacimiento ya ha ocurrido este año
                    if (fecha.Date > fechaActual.AddYears(-edad))
                    {
                        edad--;
                    }
                    return edad >= 18;
                }).WithMessage("Debe ser mayor de 18 años");

            RuleFor(x => x.Genero)
                .IsInEnum().WithMessage("Opcion incorrecta");

            RuleFor(x => x.Disciplina)
                .IsInEnum().WithMessage("Opcion incorrecta");

            RuleFor(x => x.Contraseña)
               .NotEmpty().WithMessage("El campo Contraseña no puede estar vacio")
               .NotNull().WithMessage("El campo Contraseña no puede estar vacio");

            RuleFor(x => x.ConfirmarContraseña)
                .Must((registro, confirmarContraseña) =>
                {
                    return confirmarContraseña.Equals(registro.Contraseña);
                }).WithMessage("Las contraseñas deben coincidir");


        }

        private async Task<bool> NoExisteUsuarioConEmail(string email, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.FindByEmailAsync(email);

            return result is null ? true : false;
        }

    }
}
