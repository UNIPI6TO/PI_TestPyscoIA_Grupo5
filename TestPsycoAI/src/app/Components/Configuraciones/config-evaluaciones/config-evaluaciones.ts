import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { ConfigEvaluacionesService } from '../../../Service/Configuraciones/config-evaluaciones';
import { IConfigEvaluaciones } from '../../../Interfaces/Configuraciones/iconfig-evaluaciones';
import { Form, FormsModule } from '@angular/forms';
import { IConfigEvaluacionesResumen } from '../../../Interfaces/Configuraciones/iconfig-evaluaciones-resumen';

@Component({
  selector: 'app-config-evaluaciones',
  imports: [ CommonModule,FormsModule, RouterLink, RouterModule],
  templateUrl:  './config-evaluaciones.html',
  styleUrls: ['./config-evaluaciones.css']
})
export class ConfigEvaluacionesComponent implements OnInit {

  evaluaciones: IConfigEvaluacionesResumen[] = [];
  isLoadingDetalle: boolean = false;
  constructor(private configEvaluacionesService: ConfigEvaluacionesService) { }

  ngOnInit(): void {
    this.cargarEvaluaciones();
  }
  cargarEvaluaciones() {
    this.configEvaluacionesService.getConfigEvaluaciones().subscribe({
      next: (configEvaluacionForm) => {
        this.evaluaciones = configEvaluacionForm;
      },
      error: (error) => {
        console.error('Error al cargar las evaluaciones:', error);
      }
    });

  }
}