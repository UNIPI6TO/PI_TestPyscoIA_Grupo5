import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ITipoTest } from '../../../Interfaces/Configuraciones/itipo-test';
import { TipoTestService } from '../../../Service/Configuraciones/tipo-test';
import Swal from 'sweetalert2';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-tipo-test',
  imports: [DatePipe, CommonModule, RouterLink, RouterModule],
  templateUrl: './tipo-test.html',
  styleUrls: ['./tipo-test.css']
})
export class TipoTestComponent implements OnInit {

  evaluaciones: ITipoTest[] = [];
  constructor(private tipoTestService: TipoTestService) { }
  ngOnInit(): void {
    this.cargarEvaluaciones();
  }

  cargarEvaluaciones() {
    this.tipoTestService.getTipoTest().subscribe({
      next: (data) => {
        this.evaluaciones = data;
      },
      error: (error) => {
        Swal.fire('Error', error.message, 'error');
      }
    });
  }

  

  eliminarTipoTest(evaluacion: ITipoTest) {
    Swal.fire({
      title: '¿Estás seguro?',
      text: `¿Quieres eliminar el tipo de evaluación "${evaluacion.nombre}"?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.tipoTestService.eliminarTipoTest(evaluacion.id!).subscribe({
          next: () => {
            Swal.fire('Eliminado', 'El tipo de evaluación ha sido eliminado.', 'success');
            this.cargarEvaluaciones();
          },
          error: (error) => {
            Swal.fire('Error', error.message, 'error');
          }
        });
      }
    });
  }
}