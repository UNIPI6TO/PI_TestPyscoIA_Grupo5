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

  evaluaciones: IConfigEvaluaciones[] = [];

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
      this.evaluaciones = evaluaciones;
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
  }
  cargarEvaluaciones() {
    this.configEvaluacionService.obtenerEvaluaciones().subscribe({
      next: (evaluaciones) => {
        this.evaluaciones = evaluaciones;
        
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
