import { Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';

import { NotFoundComponent } from './components/not-found/not-found.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { Encuesta1Component } from './components/encuesta1/encuesta1.component';
import { Encuesta2Component } from './components/encuesta2/encuesta2.component';
import { Encuesta3Component } from './components/encuesta3/encuesta3.component';
import { Encuesta4Component } from './components/encuesta4/encuesta4.component';
import { Encuesta5Component } from './components/encuesta5/encuesta5.component';
import { Encuesta6Component } from './components/encuesta6/encuesta6.component';
import { HomeDeportistaComponent } from './components/home-deportista/home-deportista.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'home', component: HomeComponent},
    {path: 'signin', component: SignInComponent},
    {path: 'encuesta1', component: Encuesta1Component},
    {path: 'encuesta2', component: Encuesta2Component},
    {path: 'encuesta3', component: Encuesta3Component},
    {path: 'encuesta4', component: Encuesta4Component},
    {path: 'encuesta5', component: Encuesta5Component},
    {path: 'encuesta6', component: Encuesta6Component},
    {path: 'homeDeportista', component: HomeDeportistaComponent},

    
    {path: 'not-found', component: NotFoundComponent},

    

    { path: '**', redirectTo: '/home' },

    // Otras rutas aquí
    { path: '', redirectTo: '/home', pathMatch: 'full' } // Ruta predeterminada
    
];
