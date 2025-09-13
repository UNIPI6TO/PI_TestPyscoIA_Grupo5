import { Component, OnInit } from '@angular/core';
import { IConfigEvaluacionesResumen } from '../../../../Interfaces/Configuraciones/iconfig-evaluaciones-resumen';
import { ConfigEvaluacionesService } from '../../../../Service/Configuraciones/config-evaluaciones';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IConfigSecciones } from '../../../../Interfaces/Configuraciones/iconfig-secciones';
import Swal from 'sweetalert2';
import { ConfigSeccionesService } from '../../../../Service/Configuraciones/config-secciones';
import { IConfigEvaluaciones } from '../../../../Interfaces/Configuraciones/iconfig-evaluaciones';
import { ITipoTest } from '../../../../Interfaces/Configuraciones/itipo-test';
import { TipoTestService } from '../../../../Service/Configuraciones/tipo-test';
import { IConfigPreguntas } from '../../../../Interfaces/Configuraciones/iconfig-preguntas';
import { IConfigOpciones } from '../../../../Interfaces/Configuraciones/iconfig-opciones';
import { ConfigOpcionesService } from '../../../../Service/Configuraciones/config-opciones';
import { ConfigPreguntasService } from '../../../../Service/Configuraciones/config-preguntas';

@Component({
  selector: 'app-config-evaluaciones-editar',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './config-evaluaciones-editar.html',
  styleUrls: ['./config-evaluaciones-editar.css']
})
export class ConfigEvaluacionesEditarComponent implements OnInit {
  evaluacion: IConfigEvaluacionesResumen;
  evaluacionEditable: IConfigEvaluaciones = {
    id: 0,
    nombre: '',
    duracion: 0,
    idTipoTest: 0,
    creado: new Date(),
    eliminado: false
  };
  tiposTest: ITipoTest[] = [];

  nuevaSeccion: IConfigSecciones = {
    id: 0,
    seccion: '',
    numeroPreguntas: 0,
    formulaAgregado: '',
    creado: new Date(),
    eliminado: false,
    idConfiguracionesTest: 0
  };
  seccionSelecionada: IConfigSecciones = {
    id: 0,
    seccion: '',
    numeroPreguntas: 0,
    formulaAgregado: '',
    creado: new Date(),
    eliminado: false,
    idConfiguracionesTest: 0
  };
  nuevaPregunta: IConfigPreguntas= {
      id: 0,
      creado: new Date(),
      actualizado: new Date(),
      eliminado: false,
      pregunta: '',
      idConfiguracionSecciones: 0,
      inversa: false,
      opciones: [],
  };
  constructor(
    private configuracionesService: ConfigEvaluacionesService,
    private configuracionesSeccionesService: ConfigSeccionesService,
    private tiposTestService: TipoTestService,
    private configuracionesPreguntasService: ConfigPreguntasService,
    private configuracionesOpcionesService: ConfigOpcionesService,
    private route: ActivatedRoute
  ) {
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
  cargarEdicion(): void {
    this.tiposTestService.getTipoTest().subscribe({
      next: (data: ITipoTest[]) => {
        this.tiposTest = data;
      }
    });
    //cargar evaluacion editable
    this.evaluacionEditable = {
      id: this.evaluacion.id,
      nombre: this.evaluacion.nombre,
      duracion: this.evaluacion.duracion,
      idTipoTest: this.evaluacion.tipoTest.id,
      creado: this.evaluacion.creado,
      eliminado: this.evaluacion.eliminado

    };
  }

    editarEvaluacion(): void {
    if (this.evaluacionEditable.nombre && this.evaluacionEditable.duracion > 0 && this.evaluacionEditable.idTipoTest) {
      this.configuracionesService.editarEvaluacion(this.evaluacionEditable).subscribe({
        next: () => {
          Swal.fire({
            icon: 'success',
            title: 'Evaluación Actualizada con Éxito',
          }).then(() => {
            this.cerrarModal('editarEvaluacionModal');
            if (this.evaluacion.id) {
              this.configuracionesService.getUnaConfigEvaluacion(this.evaluacion.id).subscribe((data: IConfigEvaluacionesResumen) => {
                this.evaluacion = data;
              });
            }
          });
        },
        error: (err) => {
          console.error('Error al actualizar la evaluación:', err);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Ocurrió un error al actualizar la evaluación. Intente nuevamente.',
          });
        }
      });
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Datos Incompletos',
        text: 'Por favor, complete todos los campos requeridos antes de actualizar la evaluación.',
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

  cargarPreguntaNueva(seccion: IConfigSecciones): void {
    
    this.nuevaPregunta = {
      id: 0,
      creado: new Date(),
      actualizado: new Date(),
      eliminado: false,
      pregunta: '',
      idConfiguracionSecciones: seccion.id!,
      inversa: false,
      opciones: [],
    };
    this.seccionSelecionada = seccion;
  }
  agregarPregunta(): void {
    if (this.seccionSelecionada.id && this.nuevaPregunta.pregunta) {
      this.nuevaPregunta.idConfiguracionSecciones = this.seccionSelecionada.id;
      this.configuracionesPreguntasService.agregarPregunta(this.nuevaPregunta).subscribe({
        next: () => {
            // Agregar la nueva pregunta al inicio del array de preguntas de la sección seleccionada
            if (this.seccionSelecionada.bancoPreguntas) {
              this.seccionSelecionada.bancoPreguntas.unshift({ ...this.nuevaPregunta });
            } else {
              this.seccionSelecionada.bancoPreguntas = [{ ...this.nuevaPregunta }];
            }
            this.cerrarModal('agregarPreguntaModal');
            this.nuevaPregunta = {
              id: 0,
              creado: new Date(),
              actualizado: new Date(),
              eliminado: false,
              pregunta: '',
              idConfiguracionSecciones: 0,
              inversa: false,
              opciones: [],
            };
            Swal.fire({
            icon: 'success',
            title: 'Pregunta Agregada con Éxito',
            });

        },
        error: (err) => {
          console.error('Error al agregar la pregunta:', err);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Ocurrió un error al agregar la pregunta. Intente nuevamente.',
          });
        }
      });
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Datos Incompletos',
        text: 'Por favor, complete todos los campos requeridos antes de agregar la pregunta.',
      });
    }
  }
  agregarOpcion(): void {
    Swal.fire({
      icon: 'info',
      title: 'Funcionalidad en Desarrollo',
      text: 'La funcionalidad para agregar opciones está en desarrollo y estará disponible próximamente.',
    });
    this.nuevaPregunta.opciones = this.nuevaPregunta.opciones || [];
    this.nuevaPregunta.opciones.push({
      id: 0,
      creado: new Date(),
      eliminado: false,
      orden: 0,
      opcion: '',
      peso: 0,
      idConfigPreguntas: 0
    });
  }
  eliminarOpcion(pregunta: IConfigPreguntas, opcion: IConfigOpciones): void {
    Swal.fire({
      title: '¿Está seguro?',
      text: 'Esta acción no se puede deshacer.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        if (pregunta.opciones) {
          const index = pregunta.opciones.indexOf(opcion);
          
          if (index > -1) {
            this.configuracionesOpcionesService.eliminarOpcion(opcion.id!).subscribe({
              next: () => {
                console.log('Opción eliminada del servidor');
                pregunta.opciones!.splice(index, 1);
                Swal.fire('Eliminado', 'La opción ha sido eliminada.', 'success');
              },
              error: (err) => {
                console.error('Error al eliminar la opción:', err);
                Swal.fire('Error', 'Ocurrió un error al eliminar la opción. Intente nuevamente.', 'error');
              }
            }); 
          }
        }
      } else {
        Swal.fire('Cancelado', 'La opción no ha sido eliminada.', 'info');
      } 
    });
  }
  
  moverOpcionArriba(pregunta: IConfigPreguntas, opcion: IConfigOpciones): void {
    const index = pregunta.opciones?.indexOf(opcion);
    if (typeof index === 'number' && index > 0 && pregunta.opciones) {
      const opciones = pregunta.opciones;
      // Intercambiar las opciones en el array
      [opciones[index - 1], opciones[index]] = [opciones[index], opciones[index - 1]];
      // Intercambiar el valor de 'peso' entre las dos opciones
      const tempPeso = opciones[index - 1].peso;
      opciones[index - 1].peso = opciones[index].peso;
      opciones[index].peso = tempPeso;
      // Actualizar el campo 'orden' para reflejar el nuevo orden
      opciones.forEach((op, idx) => {
      op.orden = idx + 1;
      });
      this.configuracionesOpcionesService.modificarOrden(opciones).forEach(obs => {
        obs.subscribe({
          next: () => {
            console.log('Orden de opciones actualizado');
          },
          error: (err) => {
            Swal.fire({
              icon: 'error',
              title: 'Error', 
              text: 'Ocurrió un error al actualizar el orden de las opciones. Intente nuevamente.',
            });
            console.error('Error al actualizar el orden de las opciones:', err);
          }
        });
      });
    }
  }
  moverOpcionAbajo(pregunta: IConfigPreguntas, opcion: IConfigOpciones): void {
    const index = pregunta.opciones?.indexOf(opcion); 
    if (typeof index === 'number' && pregunta.opciones && index < pregunta.opciones.length - 1) {
      const opciones = pregunta.opciones;
      // Intercambiar las opciones en el array
      [opciones[index + 1], opciones[index]] = [opciones[index], opciones[index + 1]];
      // Intercambiar el valor de 'peso' entre las dos opciones
      const tempPeso = opciones[index + 1].peso;
      opciones[index + 1].peso = opciones[index].peso;
      opciones[index].peso = tempPeso;
      // Actualizar el campo 'orden' para reflejar el nuevo orden
      opciones.forEach((op, idx) => {
        op.orden = idx + 1;
      });

    
      this.configuracionesOpcionesService.modificarOrden(opciones).forEach(obs => {
        obs.subscribe({
          next: () => {
            console.log('Orden de opciones actualizado');
          },
          error: (err) => {
            Swal.fire({
              icon: 'error',
              title: 'Error', 
              text: 'Ocurrió un error al actualizar el orden de las opciones. Intente nuevamente.',
            });
            console.error('Error al actualizar el orden de las opciones:', err);
          }
        });
      });
    }
  }
}
