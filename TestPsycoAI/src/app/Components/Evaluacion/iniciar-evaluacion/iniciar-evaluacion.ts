import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { EvaluacionesService } from '../../../Service/Test/evaluaciones';
import { IEvaluacion } from '../../../Interfaces/Evaluaciones/ievaluacion';
import { IPreguntas } from '../../../Interfaces/Evaluaciones/ipreguntas';
import { Title } from '@angular/platform-browser';

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
    private titleService: Title,
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
          if(this.evaluacion?.completado){
            this.router.navigate(['/test/tomar-evaluacion']);
            Swal.fire('Evaluación Completada', 'La evaluación ya ha sido completada.', 'info');
            return;
          }
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
          this.titleService.setTitle(`${this.evaluacion?.configuracionTest?.tipoTest?.nombre} - PsycoAI`);
        },
        error: (error) => {
          Swal.fire('Error', 'Error al cargar la evaluación', 'error');
          console.error('Error al cargar la evaluación:', error);
        }
      });
    }

  }

  isOpcionSeleccionada(): boolean {
    return Array.isArray(this.preguntaActual?.opciones) && this.preguntaActual.opciones.some(o => o?.seleccionado);
  }
  finalizarEvaluacion() {
    Swal.fire({
      title: '¿Estás seguro de finalizar la evaluación?',
      text: "No podrás cambiar tus respuestas después de finalizar.", 
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, finalizar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.evaluacion) {
          this.evaluacion.completado = true;
          this.evaluacion.fechaFinTest = this.fechaZonahoraria(new Date());
          this.actualizarPregunta(this.NumPregunta, this.buscarOpcionSelecionadaHtml());
          this.totalizarSeccionesRespuestas(this.evaluacion);
          this.editarHTP(this.evaluacion);
          this.router.navigate(['/test/tomar-evaluacion']);
        }
      }
    });
  }
  totalizarSeccionesRespuestas(evaluacion : IEvaluacion) {
    evaluacion.secciones.forEach(seccion => {
      seccion.score = 0;
      seccion.preguntas.forEach(pregunta => {
        seccion.score! += pregunta.valor || 0;
      });
      if (seccion.formulaAgregado=='AVG') {
        seccion.score = seccion.score! / seccion.preguntas.length;
      }
    });
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
intervaloTiempo: any = null;

iniciarContadorTiempo() {
  const tiempoTrasncurrido = this.evaluacion?.tiempoTranscurrido || 0;
  console.log("Tiempo transcurrido al iniciar el contador:", tiempoTrasncurrido);
  let tiempoTranscurrido = tiempoTrasncurrido;
  
  if (this.preguntaActual) {
    if (this.intervaloTiempo) {
      clearInterval(this.intervaloTiempo);
    }
    // Solo inicializa tiempoTranscurrido si es la primera vez
    this.intervaloTiempo = setInterval(() => {
      tiempoTranscurrido++;
      if (this.evaluacion) {
        this.evaluacion.tiempoTranscurrido = tiempoTranscurrido;
      }
    }, 1000);
  }
}

  siguientePregunta() {
    if (this.evaluacion && this.NumPregunta < this.evaluacion.cantidadPreguntas!) {
      const opcionSeleccionadaId:number = this.buscarOpcionSelecionadaHtml();
      this.actualizarPregunta(this.NumPregunta, opcionSeleccionadaId);
      this.editarHTP(this.evaluacion);
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
              this.iniciarContadorTiempo();
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

  editarHTP(evaluacion: IEvaluacion) {
    this.evaluacionesService.actualizarEvaluacion(evaluacion).subscribe({
      next: (evaluacion) => {
        console.log("Evaluacion actualizada");
      },
      error: (error) => {
        console.error("Error al actualizar la evaluación:", error);
      }
    });
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
