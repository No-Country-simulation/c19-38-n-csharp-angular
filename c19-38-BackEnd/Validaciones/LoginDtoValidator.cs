using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Modelos;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace c19_38_BackEnd.Validaciones
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        //private readonly UserManager<Usuario> _userManager;
        //private readonly SignInManager<Usuario> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        public LoginDtoValidator(UserManager<Usuario> userManager)
        {
        //    _userManager = userManager;
        //    Console.WriteLine("Hola pepe");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El campo Email no puede estar vacio")
                .EmailAddress().WithMessage("El campo email tiene un formato invalido")
                .Must(email=>
                {
                    return email.GetType() == typeof(string);
                }).WithMessage("El campo es un string xd");
            RuleFor(x => x.Contraseña)
                .NotEmpty().WithMessage("El campo Contraseña no puede estar vacio")
                .Length(6, 12).WithMessage("El campo Contraseña debe estar en un rango de 6 a 12 caracteres");

        }
    }
}
