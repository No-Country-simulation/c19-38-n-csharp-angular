import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-encuesta1',
  standalone: true,
  imports: [CommonModule,RouterOutlet,RouterLink],
  templateUrl: './encuesta1.component.html',
  styleUrl: './encuesta1.component.css'
})
export class Encuesta1Component {
  constructor(private router: Router) {}

  navigateTo(route: string): void {
    this.router.navigate([`/${route}`]);
  }
}