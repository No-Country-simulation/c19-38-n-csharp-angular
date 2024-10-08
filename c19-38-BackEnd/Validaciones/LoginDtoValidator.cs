﻿using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Modelos;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace c19_38_BackEnd.Validaciones
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        private readonly UserManager<Usuario> _userManager;
        public LoginDtoValidator(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El campo Email no puede estar vacio")
                .EmailAddress().WithMessage("Formato de correo invalido");

            RuleFor(x => x.Contraseña)
                .NotEmpty().WithMessage("El campo Contraseña no puede estar vacio")
                .Length(6, 12).WithMessage("El campo Contraseña debe estar en un rango de 6 a 12 caracteres");

            RuleFor(x => x)
                .MustAsync(UsuarioExistenteConEmail).WithMessage("No existe un usuario con el email proporcionado")
                .When(x => !string.IsNullOrEmpty(x.Email) && !string.IsNullOrEmpty(x.Contraseña))
                .MustAsync(ContraseñaCorrespondeUsuario).WithMessage("La contraseña es invalida")
                .When(x => !string.IsNullOrEmpty(x.Email) && !string.IsNullOrEmpty(x.Contraseña));
        }


        private async Task<bool> UsuarioExistenteConEmail(LoginDto loginDto,CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            return user is null ? false : true;
        }

        private async Task<bool> ContraseñaCorrespondeUsuario(LoginDto loginDto, CancellationToken cancellationToken = default)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);
            if(usuario is not null)
            {
                var corresponde = await _userManager.CheckPasswordAsync(usuario, loginDto.Contraseña);
                return corresponde;
            }
            return false;
        }
    }
}
