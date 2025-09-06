import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './layout/header/header';
import {  NuevoPacienteComponent } from './Components/Pacientes/nuevo-paciente/nuevo-paciente';
import {  PacientesComponent } from './Components/Pacientes/pacientes';




@Component({
  selector: 'app-root',
  imports: [ 
    RouterOutlet, 
    Header,
    PacientesComponent
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TestPsycoAI');
}
