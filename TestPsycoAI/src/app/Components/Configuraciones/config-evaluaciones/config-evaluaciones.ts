import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { ConfigEvaluacionesService } from '../../../Service/Configuraciones/config-evaluaciones';
import { IConfigEvaluaciones } from '../../../Interfaces/Configuraciones/iconfig-evaluaciones';
import { Form, FormsModule } from '@angular/forms';
import { IConfigEvaluacionesResumen } from '../../../Interfaces/Configuraciones/iconfig-evaluaciones-resumen';
import Swal from 'sweetalert2';

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
    this.isLoadingDetalle = true;
    // Mostrar spinner con SweetAlert2
    // @ts-ignore
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      // @ts-ignore
      Swal.showLoading();
      }
    });

    this.configEvaluacionesService.getConfigEvaluaciones().subscribe({
      next: (configEvaluacionForm) => {
      this.evaluaciones = configEvaluacionForm;
      this.isLoadingDetalle = false;
      // @ts-ignore
      Swal.close();
      },
      error: (error) => {
      this.isLoadingDetalle = false;
      // @ts-ignore
      Swal.close();
      console.error('Error al cargar las evaluaciones:', error);
      // @ts-ignore
      Swal.fire('Error', 'Error al cargar las evaluaciones', 'error');
      }
    });

  }

  eliminarEvaluacion(id: number) {
    Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción no se puede deshacer.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.configEvaluacionesService.eliminarEvaluacion(id).subscribe({
          next: () => {
            Swal.fire('Eliminado', 'La evaluación ha sido eliminada', 'success');
            this.cargarEvaluaciones();
          },
          error: (error) => {
            console.error('Error al eliminar la evaluación:', error);
            Swal.fire('Error', 'Error al eliminar la evaluación', 'error');
          }
        });
      }
    });
  }

}
 