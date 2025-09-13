import { Component, OnInit } from '@angular/core';
import { IConfigEvaluacionesResumen } from '../../../../Interfaces/Configuraciones/iconfig-evaluaciones-resumen';
import { ConfigEvaluacionesService } from '../../../../Service/Configuraciones/config-evaluaciones';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IConfigSecciones } from '../../../../Interfaces/Configuraciones/iconfig-secciones';
import Swal from 'sweetalert2';
import { ConfigSeccionesService } from '../../../../Service/Configuraciones/config-secciones';

@Component({
  selector: 'app-config-evaluaciones-editar',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './config-evaluaciones-editar.html',
  styleUrls: ['./config-evaluaciones-editar.css']
})
export class ConfigEvaluacionesEditarComponent implements OnInit {
  evaluacion: IConfigEvaluacionesResumen;
  nuevaSeccion: IConfigSecciones = {
    id: 0,
    seccion: '',
    numeroPreguntas: 0,
    formulaAgregado: '',
    creado: new Date(),
    eliminado: false,
    idConfiguracionesTest: 0
  };

  constructor(private configuracionesService: ConfigEvaluacionesService,private configuracionesSeccionesService: ConfigSeccionesService, private route: ActivatedRoute) {
    this.evaluacion = {
      id: 0,
      nombre: '',
      duracion: 0,
      creado: new Date(),
      actualizado: new Date(),
      eliminado: false, 
      tipoTest: { id: 0, nombre: '', descripcion: '', instrucciones: '', creado: new Date(), eliminado: false },
      numeroSecciones: 0,
      numeroPreguntas: 0
    };
  }

  ngOnInit(): void {
    // Mostrar spinner al iniciar la carga
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      Swal.showLoading();
      }
    });

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      const id = Number(idParam);
      this.configuracionesService.getUnaConfigEvaluacion(id).subscribe({
      next: (data: IConfigEvaluacionesResumen) => {
        this.evaluacion = data;
        Swal.close(); // Cerrar spinner al terminar de cargar
      },
      error: () => {
        Swal.close();
        Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Ocurrió un error al cargar la evaluación.'
        });
      }
      });
    } else {
      Swal.close();
    }
    if (idParam) {
      const id = Number(idParam);
      this.configuracionesService.getUnaConfigEvaluacion(id).subscribe((data: IConfigEvaluacionesResumen) => {
        this.evaluacion = data;
      });
    }
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
  agregarSeccion(): void {
    if (this.evaluacion.id && this.nuevaSeccion.seccion) {
      this.nuevaSeccion.idConfiguracionesTest = this.evaluacion.id;
      this.configuracionesSeccionesService.agregarSeccion(this.nuevaSeccion).subscribe({
        next: () => {
            this.configuracionesService.getUnaConfigEvaluacion(this.evaluacion.id).subscribe((data: IConfigEvaluacionesResumen) => {
            this.evaluacion = data;
            this.cerrarModal('agregarSeccionModal');
            this.nuevaSeccion = {
              id: 0,
              seccion: '',
              numeroPreguntas: 0,
              formulaAgregado: '',
              creado: new Date(),
              eliminado: false,
              idConfiguracionesTest: 0
            };
            Swal.fire({
              icon: 'success',
              title: 'Sección Agregada con Éxito',
            });
            });
          },
          error: (err) => {
            console.error('Error al agregar la sección:', err);
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: 'Ocurrió un error al agregar la sección. Intente nuevamente.',
            });
          }
        });
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Datos Incompletos',
        text: 'Por favor, complete todos los campos requeridos antes de agregar la sección.',
      });  
    }
  }

  eliminarSeccion(id: number): void {
    Swal.fire({
      title: '¿Está seguro?',
      text: 'Esta acción no se puede deshacer.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.configuracionesSeccionesService.eliminarSeccion(id).subscribe({
          next: () => { 
            if (this.evaluacion.id) {
              this.configuracionesService.getUnaConfigEvaluacion(this.evaluacion.id).subscribe((data: IConfigEvaluacionesResumen) => {
                this.evaluacion = data;
                Swal.fire('Eliminado', 'La sección ha sido eliminada.', 'success');
              });
            }
          },
          error: (err) => {
            console.error('Error al eliminar la sección:', err);
            Swal.fire('Error', 'Ocurrió un error al eliminar la sección. Intente nuevamente.', 'error');
          }
        });
      } else {
        Swal.fire('Cancelado', 'La sección no ha sido eliminada.', 'info');
      } 
    });
  }
}
