import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { EvaluacionesService } from '../../../Service/Test/evaluaciones';
import { IEvaluacion } from '../../../Interfaces/Evaluaciones/ievaluacion';
import { IPreguntas } from '../../../Interfaces/Evaluaciones/ipreguntas';
import { IOpciones } from '../../../Interfaces/Evaluaciones/iopciones';
import { PreguntasService } from '../../../Service/Test/preguntas';

@Component({
  selector: 'app-iniciar-evaluacion',
  imports: [
    CommonModule,
    FormsModule,
    RouterLink
  ],
  templateUrl: './iniciar-evaluacion.html',
  styleUrls: ['./iniciar-evaluacion.css']
  
})
export class IniciarEvaluacionComponent implements OnInit {
  NumPregunta: number = -1;
  preguntaActual: IPreguntas = {
    id: 0,
    creado: new Date(),
    orden: 0,
    opciones: [],
    eliminado: false,
    idConfiguracionPreguntas: 0,
    idSecciones: 0,
    pregunta: ''
  };
  EvaluacionId: number = 0;
  
  constructor(
    private parametros: ActivatedRoute,
    private evaluacionesService: EvaluacionesService,
    private preguntasService: PreguntasService,
    private router: Router
  ) {  }
  obtenerParametros() {
      const evaluacionIdParam = this.parametros.snapshot.paramMap.get('id');
    const numPreguntaParam = this.parametros.snapshot.paramMap.get('numPregunta');
    const evaluacionId: number = evaluacionIdParam ? Number(evaluacionIdParam) : 0;
    const numPregunta: number = numPreguntaParam ? Number(numPreguntaParam) : -1;
    if (numPregunta) {
      this.NumPregunta = numPregunta;
    }
    if (evaluacionId) {
      this.EvaluacionId = evaluacionId;
    }
  }
  siguiente: boolean = false;
  evaluacion: IEvaluacion | null = null;
  
  ngOnInit(): void {
    // Mostrar spinner de carga con SweetAlert2
    // @ts-ignore
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      Swal.showLoading();
      }
    });

    this.parametros.paramMap.subscribe(() => {
      this.obtenerParametros();
      if (this.evaluacion === null) {
        this.cargarEvaluacionConPregunta();
      } else {
        this.cargarPregunta();
        Swal.close();
      }
    });
  

  }

  cargarEvaluacionConPregunta() {
    if (this.EvaluacionId) {
      this.evaluacionesService.cargarEvaluacionId(this.EvaluacionId).subscribe({
        next: (evaluacion) => {
          this.evaluacion = evaluacion;

          this.cargarPregunta();
          Swal.close();
        },
        error: (error) => {
          Swal.fire('Error', 'Error al cargar la evaluación', 'error');
          console.error('Error al cargar la evaluación:', error);
          Swal.close();
        }
      });
    }
  }

  cargarEvaluacion() {
    if (this.EvaluacionId) {
      this.evaluacionesService.cargarEvaluacionId(this.EvaluacionId).subscribe({
        next: (evaluacion) => {
          this.evaluacion = evaluacion;

        },
        error: (error) => {
          Swal.fire('Error', 'Error al cargar la evaluación', 'error');
          console.error('Error al cargar la evaluación:', error);
        }
      });
    }

  }

  
  reanudarEvaluacion(preguntasContestadas: number) {
    if (this.evaluacion) {
      this.evaluacion.iniciado = true;
      this.NumPregunta = preguntasContestadas+1;
      this.cargarPregunta();
      if (this.evaluacion.fechaInicioTest==null || this.evaluacion.fechaInicioTest===undefined) {
        this.evaluacion.fechaInicioTest = this.fechaZonahoraria(new Date());
        this.evaluacion.iniciado = true;
        
        this.evaluacionesService.actualizarEvaluacion(this.evaluacion).subscribe({
          next: (evaluacion) => {
            console.log("Evaluacion Reanudada");
          },
          error: (error) => {
            console.error("Error al actualizar la evaluación:", error);
          }
        });
      }
    }
  }

  iniciarEvaluacion() {
    if (this.evaluacion) {
      this.evaluacion.iniciado = true;
      this.NumPregunta = 1;
      this.cargarPregunta();
    }
  }
  

  siguientePregunta() {
    if (this.evaluacion && this.NumPregunta < this.evaluacion.cantidadPreguntas!) {
      const opcionSeleccionadaId:number = this.buscarOpcionSelecionadaHtml();
      this.actualizarPregunta(this.NumPregunta, opcionSeleccionadaId);
      this.NumPregunta++;
      
      this.router.navigate([`/test/iniciar-evaluacion/${this.EvaluacionId}/pregunta/${this.NumPregunta}`]);
      

      this.cargarPregunta();
    }
    this.siguiente=false;
  }


  cargarPregunta() {
    this.obtenerParametros()
    if (this.NumPregunta > 0) {
      if (this.evaluacion) {
        this.evaluacion.secciones.forEach(secciones => {
          secciones.preguntas.find(pregunta => { 
            if (pregunta.orden === this.NumPregunta) {
              this.preguntaActual = pregunta;
            }
            
          });
        });
        this.siguiente = false;
      }
    }
  }

  

  buscarOpcionSelecionadaHtml(): number
  {
    const div = document.getElementById("opciones");
    const radios = div?.querySelectorAll<HTMLInputElement>('input[type="radio"]');
    if (!radios) {
      return -1;
    }
    for (const radio of radios) {
      if (radio.checked) {
      // El id tiene formato "op-123", quitamos "op-" y devolvemos el número
      const idStr = radio.id.replace('op-', '');
      const id = Number(idStr);
      return isNaN(id) ? -1 : id;
      }
    }
    return -1;
  }
  actualizarPregunta(Orden: number, OpcionId: number) {
    if (this.preguntaActual) {
      this.evaluacion?.secciones.forEach(seccion => {
        seccion.preguntas.forEach(pregunta => {
          if (pregunta.orden === Orden) {
            if (pregunta.opciones) {
              pregunta.opciones.forEach(opcion => {
                if (opcion.id === OpcionId) {
                  seccion.actualizado = this.fechaZonahoraria(new Date());
                  pregunta.actualizado = this.fechaZonahoraria(new Date());
                  opcion.seleccionado = true;
                  pregunta.valor= opcion.peso;
                  pregunta.respuesta= opcion.opcion;
                  if (this.evaluacion!.fechaInicioTest==null || this.evaluacion!.fechaInicioTest===undefined) {
                    this.evaluacion!.fechaInicioTest = this.fechaZonahoraria(new Date());
                    this.evaluacion!.iniciado = true;
                  }
                  this.evaluacion!.contestadas=this.obtenerPreguntasContestadas(this.evaluacion!);
                  this.evaluacion!.noContestadas= this.evaluacion!.cantidadPreguntas - this.evaluacion!.contestadas;
                  pregunta.actualizado = this.fechaZonahoraria(new Date());
                  opcion.actualizado = this.fechaZonahoraria(new Date());
                  this.evaluacionesService.actualizarEvaluacion(this.evaluacion!).subscribe({
                    next: (evaluacion) => {
                      console.log("Evaluacion actualizada");
                    },
                    error: (error) => {
                      console.error("Error al actualizar la evaluación:", error);
                    }
                  });
                } else {
                  opcion.seleccionado = false;
                }
              });
            }
          }
        });
      });
    }
  }
  obtenerPreguntasContestadas(evaluacion: IEvaluacion): number {
    let contador = 0;
    evaluacion.secciones.forEach(seccion => {
      seccion.preguntas.forEach(pregunta => {
        if (pregunta.valor !== null && pregunta.valor !== undefined && pregunta.valor !== 0) {
          contador++;
        }
      });
    });
    return contador;
  }
  fechaZonahoraria(fecha: Date): Date {
    const fechaLocal = new Date(fecha);
    // GMT-5 is UTC-5, so offset is -5 hours in milliseconds
    const gmtMinus5Offset = -5 * 60 * 60 * 1000;
    return new Date(fechaLocal.getTime() + gmtMinus5Offset);
  }
}
