import { Component, OnInit } from '@angular/core';
import { IEvaluadores } from '../../../../Interfaces/Configuraciones/ievaluadores';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ICiudad } from '../../../../Interfaces/iciudad';
import { CiudadService } from '../../../../Service/ciudad';
import Swal from 'sweetalert2';
import { EvaluadoresService } from '../../../../Service/Configuraciones/evaluadores';

@Component({
  selector: 'app-nuevo-evaluador',
  imports: [
    CommonModule, 
    FormsModule,
    RouterLink
  ],
  templateUrl: './nuevo-evaluador.html',
  styleUrls: ['./nuevo-evaluador.css']
})
export class NuevoEvaluadorComponent implements OnInit {
  ciudadesDisponibles: ICiudad[] = [];
  evaluador: IEvaluadores = {
    id: 0,
    creado: new Date(),
    nombre: '',
    email: '',
    cargo: '',
    idCiudad: 0,
    eliminado: false,
    cedula: '',
    telefono: '',
    especialidad: ''
  };
  constructor(
    private ciudadService: CiudadService,
    private evaluadorService: EvaluadoresService
  ) { }
  ngOnInit() {
  // Mostrar el spinner de carga con SweetAlert2
  // Asegúrate de tener instalado sweetalert2: npm install sweetalert2
  Swal.fire({
    title: 'Cargando...',
    allowOutsideClick: false,
    didOpen: () => {
      Swal.showLoading();
    }
    });

    this.ciudadService.getCiudades().subscribe({
      next: (data: ICiudad[]) => {
        this.ciudadesDisponibles = data;
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'No se pudieron cargar las ciudades.'
        });
      },
      complete: () => {
        // Ocultar el spinner de carga
        Swal.close();
      }
    });
  }

  guardarEvaluador() {
    // Mostrar el spinner de carga con SweetAlert2

    this.evaluadorService.guardarEvaluador(this.evaluador).subscribe({
      next: (data) => {
        Swal.fire({
          icon: 'success',
          title: 'Éxito',
          text: 'Evaluador guardado correctamente.'
        });
        // Volver a la lista de evaluadores
        this.evaluador = {
          id: 0,
          creado: new Date(),
          nombre: '',
          email: '',
          cargo: '',
          idCiudad: 0,
          eliminado: false,
          cedula: '',
          telefono: '',
          especialidad: ''
        };
        // Navegar a la lista de evaluadores
        window.location.href = '/configuraciones/evaluadores';
        
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'No se pudo guardar el evaluador.'
        });
      },

    });
  }
}
