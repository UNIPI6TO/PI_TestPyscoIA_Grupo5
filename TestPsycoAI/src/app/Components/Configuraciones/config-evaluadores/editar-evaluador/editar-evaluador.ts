import { Component, OnInit } from '@angular/core';
import { IEvaluadores } from '../../../../Interfaces/Configuraciones/ievaluadores';
import { ICiudad } from '../../../../Interfaces/iciudad';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { CiudadService } from '../../../../Service/ciudad';
import { EvaluadoresService } from '../../../../Service/Configuraciones/evaluadores';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { IUsuario } from '../../../../Interfaces/Login/iusuario';
import { AccesoDenegadoComponent } from '../../../../layout/acceso-denegado/acceso-denegado';

@Component({
  selector: 'app-editar-evaluador',
  imports: [
    CommonModule,
    FormsModule,
    RouterLink,
    AccesoDenegadoComponent
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
    private titleService: Title,
    private router: Router
  ) { }

  ngOnInit() {
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      Swal.showLoading();
      }
    });
    this.verificarSesion();
    this.cargarCiudades();
    this.cargarUnEvaluador();
    this.titleService.setTitle('Editar Evaluador - PsycoAI');
    setTimeout(() => {
      Swal.close();
    }, 1000);
  }
  rolesValidos: string[] = ['ADMIN'];
  accesoDenegado: boolean = false;
  sesion: IUsuario | null = null;
  iniciadaSesion: boolean = false;
  verificarSesion(){
    const match = document.cookie.match(new RegExp('(^| )username=([^;]+)'));
    if (match) {
      const username = JSON.parse(decodeURIComponent(match[2]));
      this.sesion = username;
      this.iniciadaSesion = true;
      if (this.sesion && !this.rolesValidos.includes(this.sesion.rol)) {
        this.accesoDenegado = true;
      }
    } else {
      this.sesion = null;
      this.iniciadaSesion = false;
      this.router.navigate(['/iniciar-sesion']).then(() => {
        window.location.reload();
      });
    }
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
