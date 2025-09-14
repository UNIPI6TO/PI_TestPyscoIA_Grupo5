import { Component, OnInit } from '@angular/core';
import { IEvaluadores } from '../../../Interfaces/Configuraciones/ievaluadores';
import { EvaluadoresService } from '../../../Service/Configuraciones/evaluadores';
import Swal from 'sweetalert2';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-config-evaluadores',
  imports: [RouterLink],
  templateUrl: './config-evaluadores.html',
  styleUrls: ['./config-evaluadores.css']
})
export class ConfigEvaluadoresComponent implements OnInit {
  evaluadores: IEvaluadores[] = [];
  
  constructor(
    private evaluadoresService: EvaluadoresService
  ) { }

  ngOnInit(): void {
    // Mostrar spinner de carga con SweetAlert2
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      Swal.showLoading();
      }
    });

    this.evaluadoresService.getEvaluadores().subscribe({
      next: (data) => {
      this.evaluadores = data as IEvaluadores[];
      Swal.close();
      },
      error: (err) => {
      Swal.close();
      Swal.fire('Error', 'Error al cargar evaluadores', 'error');
      console.error('Error al cargar evaluadores:', err);
      }
    });
  }


  cargarEvaluadores() {
    this.evaluadoresService.getEvaluadores().subscribe({
      next: (data) => {
        this.evaluadores = data as IEvaluadores[];
        console.log('Evaluadores cargados:', this.evaluadores);
      },
      error: (err) => {
        console.error('Error al cargar evaluadores:', err);
      }
    });
  }

  eliminarEvaluador(evaluador: IEvaluadores) {
    Swal.fire({
      title: '¿Estás seguro?',
      text: `Esta acción eliminará al evaluador ${evaluador.nombre}.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.evaluadoresService.eliminarEvaluador(evaluador.id!).subscribe({
          next: () => {
            this.evaluadores = this.evaluadores.filter(e => e.id !== evaluador.id);
          },
          error: (err) => {
            Swal.fire('Error', 'Error al eliminar el evaluador', 'error');
            console.error('Error al eliminar el evaluador:', err);
          }
        });
      }
    });
  }

}
