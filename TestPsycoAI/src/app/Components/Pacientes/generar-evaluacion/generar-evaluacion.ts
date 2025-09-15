import { Component, OnInit } from '@angular/core';
import { IPaciente } from '../../../Interfaces/ipaciente';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PacienteService } from '../../../Service/paciente';
import { Title } from '@angular/platform-browser';
import { IConfigEvaluaciones } from '../../../Interfaces/Configuraciones/iconfig-evaluaciones';
import { ConfigEvaluacionesService } from '../../../Service/Configuraciones/config-evaluaciones';
import { IEvaluacion } from '../../../Interfaces/Evaluaciones/ievaluacion';
import { EvaluacionesService } from '../../../Service/Test/evaluaciones';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-generar-evaluacion',
  imports: [
    CommonModule,
    FormsModule,
    RouterLink
  ],
  templateUrl: './generar-evaluacion.html',
  styleUrls: ['./generar-evaluacion.css']
})
export class GenerarEvaluacionComponent implements  OnInit {

  idEvaluador: number = 5; // Aquí debes asignar el ID del evaluador actual
  pacienteId: number = 0;
  cargando: boolean = false;
  evaluacionSeleccionada: number | null = null;
  // tiposTest: ITipoTest[] = []; // Suponiendo que tienes una interfaz ITipoTest
  // tipoTestSeleccionado: number | null = null;
  paciente: IPaciente = {
    id: 0,
    creado: new Date(),
    cedula: ''  ,
    nombre: '',
    email: '',
    fechaNacimiento: new Date(),
    direccion: '',
    idCiudad: 0
  };

  evaluacionesConfig: IConfigEvaluaciones[] = [];

  evaluacionesGeneradas: IEvaluacion[] = [];
  constructor(
    private pacienteService: PacienteService,
    private evaluacionService: EvaluacionesService,
    private configEvaluacionService: ConfigEvaluacionesService,
    private titleService: Title,
    private parametros: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // Mostrar spinner de carga con SweetAlert2
    // @ts-ignore
    Swal.fire({
      title: 'Cargando...',
      text: 'Por favor espere',
      allowOutsideClick: false,
      didOpen: () => {
      // @ts-ignore
      Swal.showLoading();
      }
    });

    this.cargarUnPaciente();
    this.titleService.setTitle('Generar Evaluación - PsycoAI');
    this.cargarEvaluacionesGeneradas();
    this.configEvaluacionService.obtenerEvaluaciones().subscribe({
      next: (evaluaciones) => {
      this.evaluacionesConfig = evaluaciones;
      // @ts-ignore
      Swal.close();
      },
      error: (err) => {
      // @ts-ignore
      Swal.fire('Error', 'No se pueden cargar las evaluaciones', 'error');
      console.error('Error al cargar las evaluaciones:', err);
      }
    });
    
  }

  generarEvaluacion()
  {

    this.cargando = true;
    if(this.evaluacionSeleccionada && this.pacienteId > 0)
    {

      //Validar que no exista ya una evaluacion de este tipo para el paciente 
      if(this.validarExistente())
      {
        // Mostrar spinner de carga con SweetAlert2
        // @ts-ignore
        Swal.fire({
        title: 'Generando evaluación...',
        text: 'Por favor espere',
        allowOutsideClick: false,
        didOpen: () => {
          // @ts-ignore
          Swal.showLoading();
        }
        });

        //Generar la evaluacion
        this.evaluacionService.generarEvaluacion(
          Number(this.pacienteId),
          Number(this.evaluacionSeleccionada),
          Number(this.idEvaluador)
        ).subscribe({
          next: (data) => {
          // @ts-ignore
          this.cargarEvaluacionesGeneradas();
          Swal.close();
          Swal.fire('Éxito', 'Evaluación generada correctamente', 'success');
          },
          error: (err) => {
          // @ts-ignore
          Swal.close();
          console.error('Error al generar evaluación:', err.message);
          const mensaje = err?.message || 'Error al generar la evaluación';
          Swal.fire('Error', mensaje, 'error');
          console.error('Error al generar la evaluación:', err);
          }
        });
         
      
      
      }
    } 
    this.cargando = false;
  }


  eliminarEvaluacion(evaluacion: IEvaluacion) {
    Swal.fire({
      title: '¿Estás seguro?',  
      text: 'Esta acción no se puede deshacer.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.evaluacionService.eliminarEvaluacion(evaluacion.id!).subscribe({
          next: () => {
            this.evaluacionesGeneradas = this.evaluacionesGeneradas.filter(e => e.id !== evaluacion.id);
            Swal.fire('Eliminado', 'La evaluación ha sido eliminada.', 'success');
          },
          error: (err) => {
            Swal.fire('Error', 'No se pudo eliminar la evaluación.', 'error');
            console.error('Error al eliminar la evaluación:', err);
          }
        });
      }
    });
  }
  validarExistente(): boolean {
      //Validar que no exista ya una evaluacion de este tipo para el paciente 
      const evaluacionExistente = this.evaluacionesGeneradas.find(
        e => Number(e.idConfiguracionTest) === Number(this.evaluacionSeleccionada) && 
        Number(e.idPaciente) === Number(this.pacienteId) && 
        e.completado === false
      );
      if(evaluacionExistente)
      {
        Swal.fire('Error', 'Ya existe una evaluación de este tipo para el paciente que no ha sido completada. Elimine la anterior y vuelva a ejecutar', 'error');
        this.cargando = false;
        return false;
      }
      return true;
  }
  cargarEvaluaciones() {
    this.configEvaluacionService.obtenerEvaluaciones().subscribe({
      next: (evaluaciones) => {
        this.evaluacionesConfig = evaluaciones;
        
      },
      error: (err) => {
        console.error('Error al cargar las evaluaciones:', err);
      }
    });
  }
  cargarEvaluacionesGeneradas() {
    if (this.pacienteId > 0) {
      this.evaluacionService.obtenerEvaluacionesPorPaciente(this.pacienteId).subscribe({
        next: (evaluaciones) => {
          this.evaluacionesGeneradas = evaluaciones;
          console.log('Evaluaciones generadas cargadas:', this.evaluacionesGeneradas);
        },
        error: (err) => {
          Swal.fire('Error', 'No se pueden cargar las evaluaciones generadas', 'error');
          console.error('Error al cargar las evaluaciones generadas:', err);
        }
      });
    }
  }
  cargarUnPaciente() {
    this.pacienteId = Number(this.parametros.snapshot.paramMap.get('idpaciente'));
    if (isNaN(this.pacienteId) || this.pacienteId <= 0) {
      console.error('ID de paciente no válido:', this.pacienteId);
      return;
    }
    this.pacienteService.obtenerUnPaciente(this.pacienteId).subscribe({
      next: (paciente) => {
        this.paciente = paciente;
        console.log('Paciente cargado:', this.paciente);
      },
      error: (err) => {
        console.error('Error al cargar el paciente:', err);
      }
    });
  }
}
