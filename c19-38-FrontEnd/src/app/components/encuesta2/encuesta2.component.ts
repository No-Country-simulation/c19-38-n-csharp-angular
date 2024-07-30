import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-encuesta2',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink ],
  templateUrl: './encuesta2.component.html',
  styleUrl: './encuesta2.component.css'
})
export class Encuesta2Component {
  constructor(private router: Router) {}

  navigateTo(route: string): void {
    this.router.navigate([`/${route}`]);
  }
}