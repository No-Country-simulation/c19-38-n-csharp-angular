import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms'; // Importa FormsModule
@Component({
  selector: 'app-encuesta6',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, FormsModule],
  templateUrl: './encuesta6.component.html',
  styleUrl: './encuesta6.component.css'
})
export class Encuesta6Component {
  selectedOption: string | null = null;
  equipment: { [key: string]: boolean } = {
    mancuernas: false,
    bandas: false,
    trx: false,
    cinta: false,
    bicicleta: false,
    barra: false,
    yogaMat: false,
    otro: false
  };
  otherEquipment: string = '';

  constructor(private router: Router) {}

  selectOption(option: string) {
    this.selectedOption = option;
  }

  toggleEquipment(equipment: string) {
    this.equipment[equipment] = !this.equipment[equipment];
  }

  navigateTo(route: string) {
    if (!this.selectedOption) {
      alert('Por favor, seleccione un lugar de entrenamiento.');
      return;
    }

    // Guardar en localStorage si es necesario
    localStorage.setItem('selectedOption', this.selectedOption);
    localStorage.setItem('equipment', JSON.stringify(this.equipment));
    localStorage.setItem('otherEquipment', this.otherEquipment);

    this.router.navigate([route]);
  }

  
}