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
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacio");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El campo Apellido no puede estar vacio");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El campo Email no puede estar vacio")
                .EmailAddress().WithMessage("Formato de correo invalido")
                .MustAsync(NoExisteUsuarioConEmail).WithMessage("El correo electronico ya esta en uso")
                .When(x => !string.IsNullOrEmpty(x.Email) && !string.IsNullOrEmpty(x.Contraseña)); 

            RuleFor(x => x.Peso)
                .GreaterThan(0).WithMessage("El campo Peso no puede ser menor o igual a 0");

            RuleFor(x => x.Altura)
                .GreaterThan(0).WithMessage("El campo Altura no puede ser menor o igual a 0");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento no puede estar vacia")
                .Must(SerMayorDeEdad).WithMessage("Debe ser mayor de 18 años");

            RuleFor(x => x.Genero)
                .IsInEnum().WithMessage("Opcion incorrecta");

            RuleFor(x => x.Disciplina)
                .IsInEnum().WithMessage("Opcion incorrecta");

            RuleFor(x => x.Contraseña)
               .NotEmpty().WithMessage("El campo Contraseña no puede estar vacio");

            RuleFor(x => x.ConfirmarContraseña)
                .Equal(x=>x.Contraseña).WithMessage("Las contraseñas deben coincidir");


        }

        private async Task<bool> NoExisteUsuarioConEmail(string email, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.FindByEmailAsync(email);

            return result is null ? true : false;
        }

        private bool SerMayorDeEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Today;
            int edad = fechaActual.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > fechaActual.AddYears(-edad))
            {
                edad--;
            }
            return edad >= 18;
        }

    }
}
