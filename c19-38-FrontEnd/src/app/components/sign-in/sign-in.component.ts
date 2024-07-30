import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet, ReactiveFormsModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent implements OnInit {
  signUpForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router) {
    this.signUpForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      weight: ['', Validators.required],
      height: ['', Validators.required],
      discipline: ['', Validators.required],
      gender: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      acceptTerms: [false, Validators.requiredTrue]
    }, { validators: this.passwordMatchValidator }); // Agregar la validación personalizada aquí
  }

  ngOnInit(): void {}

  // Validador personalizado para comprobar si las contraseñas coinciden
  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onRegister() {
    if (this.signUpForm.invalid) {
      let errorMessage = 'Debe completar todos los campos.';
      if (this.signUpForm.get('acceptTerms')?.invalid) {
        errorMessage = 'Debe aceptar los términos y condiciones';
      } else if (this.signUpForm.errors && this.signUpForm.errors['mismatch']) {
        errorMessage = 'Las contraseñas no coinciden.';
      }
      alert(errorMessage);
      return; // Detiene la ejecución si el formulario es inválido
    }
    
    console.log('Registro exitoso');
    this.router.navigate(['/encuesta1']);
  }
}