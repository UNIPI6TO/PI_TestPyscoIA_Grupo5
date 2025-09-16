import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IEvaluacion } from '../../Interfaces/Evaluaciones/ievaluacion';
import { PacienteService } from '../../Service/paciente';
import { EvaluacionesService } from '../../Service/Test/evaluaciones';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IPaciente } from '../../Interfaces/ipaciente';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-evaluacion',
  imports: [
    CommonModule,
    FormsModule,
    RouterLink
],
  templateUrl: './evaluacion.html',
  styleUrls: ['./evaluacion.css']
})
export class EvaluacionComponent implements OnInit {
  constructor(
    private titleService: Title, 
    private pacientesService: PacienteService,
    private evaluacionesService: EvaluacionesService
  ) {  }

  pacienteSelecionado: number = 0;
  evaluaciones: IEvaluacion[] = [];
  pacientes: IPaciente[] = [];
  admin: boolean = true;

  ngOnInit(): void {
    this.titleService.setTitle('Evaluación de [Aqui el Test] - PsycoAI');
    this.cargarPacientes();
  }
  cargarPacientes() { 
    this.pacientesService.getPacientes().subscribe({
      next: (data) => {
        this.pacientes = data as IPaciente[];
        
      },
      error: (err) => {
        console.error('Error al cargar pacientes:', err);
      }
    });
  }
  cargarEvaluacionesDelPaciente() {
    if (this.pacienteSelecionado > 0) {
      this.evaluacionesService.obtenerEvaluacionesPorPaciente(this.pacienteSelecionado).subscribe({
        next: (data) => {
          this.evaluaciones = data as IEvaluacion[];
          console.log('Evaluaciones cargadas:', this.evaluaciones);
        },
        error: (err) => {
          console.error('Error al cargar evaluaciones:', err);
        }
      });
    }
  }
  onPacienteChange() {
    this.cargarEvaluacionesDelPaciente();
  }
}
