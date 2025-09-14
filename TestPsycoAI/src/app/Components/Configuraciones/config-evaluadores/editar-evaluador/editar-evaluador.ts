import { Component, OnInit } from '@angular/core';
import { IEvaluadores } from '../../../../Interfaces/Configuraciones/ievaluadores';
import { ICiudad } from '../../../../Interfaces/iciudad';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { CiudadService } from '../../../../Service/ciudad';
import { EvaluadoresService } from '../../../../Service/Configuraciones/evaluadores';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-editar-evaluador',
  imports: [
    CommonModule,
    FormsModule,
    RouterLink
  ],
  templateUrl: './editar-evaluador.html',
  styleUrls: ['./editar-evaluador.css']
})
export class EditarEvaluadorComponent implements OnInit {
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
  ciudadesDisponibles: ICiudad[] = [];

  constructor(
    private ciudadService: CiudadService,
    private evaluadorService: EvaluadoresService,
    private parametros: ActivatedRoute,
    private titleService: Title
  ) { }

  ngOnInit() {
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      Swal.showLoading();
      }
    });

    this.cargarCiudades();
    this.cargarUnEvaluador();
    this.titleService.setTitle('Editar Evaluador - PsycoAI');
    setTimeout(() => {
      Swal.close();
    }, 1000);
  }
  cargarCiudades() {
    this.ciudadService.getCiudades().subscribe({
      next: (data: ICiudad[]) => {
        this.ciudadesDisponibles = data;
      },
      error: (err) => {
        console.error('Error al cargar ciudades:', err);
      }
    });
  }
  cargarUnEvaluador() {
    this.parametros.params.subscribe(params => {
      const id = params['id'];
      this.evaluadorService.obtenerUnEvaluador(id).subscribe({
          next: (data) => {
            this.evaluador = data as IEvaluadores;
            console.log('Evaluador cargado:', this.evaluador);
          },
        error: (err) => {
          console.error('Error al cargar evaluador:', err);
        }
      });
  });
  }

  editarEvaluador(){
    this.evaluadorService.editarEvaluador(this.evaluador).subscribe({
      next: (data) => {
        Swal.fire({
          icon: 'success',
          title: 'Éxito',
          text: 'Evaluador editado correctamente.'
        });
        window.location.href = '/configuraciones/evaluadores';
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'No se pudo editar el evaluador.'
        });
      }
    });
  }
}
