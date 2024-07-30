import { Component } from '@angular/core';
import { Router, RouterLink, RouterModule, RouterOutlet } from '@angular/router';
import { Encuesta1Component } from './components/encuesta1/encuesta1.component';
import { HomeComponent } from "./components/home/home.component";
import { LoginComponent } from "./components/login/login.component";
import { FormsModule } from '@angular/forms';
import { SignInComponent } from './components/sign-in/sign-in.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
 Encuesta1Component,
 HomeComponent,
 LoginComponent,
SignInComponent,
 RouterOutlet,

],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'c19-38-FrontEnd';
}
