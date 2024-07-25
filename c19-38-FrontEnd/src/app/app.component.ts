import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Encuesta1Component } from './components/encuesta1/encuesta1.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    Encuesta1Component,
    
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'c19-38-FrontEnd';
}
