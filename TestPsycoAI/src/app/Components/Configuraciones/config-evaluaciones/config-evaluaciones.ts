import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { ConfigEvaluacionesService } from '../../../Service/Configuraciones/config-evaluaciones';
import { FormsModule } from '@angular/forms';
import { IConfigEvaluacionesResumen } from '../../../Interfaces/Configuraciones/iconfig-evaluaciones-resumen';
import Swal from 'sweetalert2';
import { IConfigEvaluaciones } from '../../../Interfaces/Configuraciones/iconfig-evaluaciones';
import { TipoTestService } from '../../../Service/Configuraciones/tipo-test';
import { ITipoTest } from '../../../Interfaces/Configuraciones/itipo-test';
import { IUsuario } from '../../../Interfaces/Login/iusuario';
import { AccesoDenegadoComponent } from '../../../layout/acceso-denegado/acceso-denegado';

@Component({
  selector: 'app-config-evaluaciones',
  imports: [ CommonModule,FormsModule, RouterLink, RouterModule, AccesoDenegadoComponent],
  templateUrl:  './config-evaluaciones.html',
  styleUrls: ['./config-evaluaciones.css']
})
export class ConfigEvaluacionesComponent implements OnInit {

  evaluaciones: IConfigEvaluacionesResumen[] = [];
  isLoadingDetalle: boolean = false;
  constructor(
    private configEvaluacionesService: ConfigEvaluacionesService,
    private configTiposTestService: TipoTestService,
    private router: Router
  ) { }

  ngOnInit(): void {
    // Mostrar spinner con SweetAlert2 al iniciar
    // @ts-ignore
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      // @ts-ignore
      Swal.showLoading();
      }
    });

    this.verificarSesion();
    this.cargarEvaluaciones();

    
    setTimeout(() => { Swal.close(); }, 1000);
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

  
  nuevaEvaluacion: IConfigEvaluaciones = {
    id: 0,
    nombre: '',
    duracion: 0,
    idTipoTest: 0,
    creado: new Date(),
    eliminado: false
  };


  tiposTest: ITipoTest[] = [];
  cargarNuevaEvaluacion(){
    this.nuevaEvaluacion = {
      id: 0,
      nombre: '',
      duracion: 0,
      idTipoTest: 0,
      creado: new Date(),
      eliminado: false
    };
    this.configTiposTestService.getTipoTest().subscribe({
      next: (tiposTest) => {
        this.tiposTest = tiposTest;
      },
      error: (error) => {
        console.error('Error al cargar los tipos de test:', error);
      }
    });
  }
  agregarEvaluacion() {
    if (this.nuevaEvaluacion.nombre && this.nuevaEvaluacion.duracion > 0 && this.nuevaEvaluacion.idTipoTest) {
      this.configEvaluacionesService.agregarEvaluacion(this.nuevaEvaluacion).subscribe({
        next: () => {
          Swal.fire('Éxito', 'La evaluación ha sido agregada', 'success');
          this.cargarEvaluaciones();
          this.cargarNuevaEvaluacion();
          this.cerrarModal('agregarEvaluacionModal');
        },
        error: (error) => {
          console.error('Error al agregar la evaluación:', error);
          Swal.fire('Error', 'Error al agregar la evaluación', 'error');
        }
      });
    }
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
 
  cerrarModal(modalName: string): void {
    const modal = document.getElementById(modalName);
    if (modal) {
      // If using native <dialog>
      if (typeof (modal as any).close === 'function') {
        (modal as any).close();
        document.body.style.overflow = 'auto';
        // Remove Bootstrap modal classes and backdrop if present
        document.body.classList.remove('modal-open');
        const backdrops = document.querySelectorAll('.modal-backdrop');
        backdrops.forEach((backdrop) => backdrop.parentNode?.removeChild(backdrop));
      } else {
        // For other modal implementations (e.g., Bootstrap, custom)
        modal.style.display = 'none';
        document.body.classList.remove('modal-open');
        const backdrops = document.querySelectorAll('.modal-backdrop');
        backdrops.forEach((backdrop) => backdrop.parentNode?.removeChild(backdrop));
      }
    }
  }
   

}
 