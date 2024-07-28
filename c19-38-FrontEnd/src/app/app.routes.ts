import { Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
        
    { path: '', component: HomeComponent},
    { path: '**', redirectTo: '' },

    // Otras rutas aquí
    { path: '', redirectTo: '/home', pathMatch: 'full' } // Ruta predeterminada
    
];
