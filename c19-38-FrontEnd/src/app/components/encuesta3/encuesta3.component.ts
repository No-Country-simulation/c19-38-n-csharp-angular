import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';


@Component({
  selector: 'app-encuesta3',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink ],
  templateUrl: './encuesta3.component.html',
  styleUrl: './encuesta3.component.css'
})
export class Encuesta3Component {  
  
constructor(private router: Router) {}

navigateTo(route: string): void {
  this.router.navigate([`/${route}`]);
}
}