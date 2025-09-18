import { Component, OnInit } from '@angular/core';
import { IEvaluadores } from '../../../../Interfaces/Configuraciones/ievaluadores';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ICiudad } from '../../../../Interfaces/iciudad';
import { CiudadService } from '../../../../Service/ciudad';
import Swal from 'sweetalert2';
import { EvaluadoresService } from '../../../../Service/Configuraciones/evaluadores';
import { Title } from '@angular/platform-browser';
import { IUsuario } from '../../../../Interfaces/Login/iusuario';
import { AccesoDenegadoComponent } from '../../../../layout/acceso-denegado/acceso-denegado';

@Component({
  selector: 'app-nuevo-evaluador',
  imports: [
    CommonModule, 
    FormsModule,
    RouterLink,
    AccesoDenegadoComponent
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
    private evaluadorService: EvaluadoresService,
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
    this.titleService.setTitle('Crear Nuevo Evaluador - PsycoAI');
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
