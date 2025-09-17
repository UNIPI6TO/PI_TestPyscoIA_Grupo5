import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Route } from '@angular/router';
import Swal from 'sweetalert2';
import { EvaluacionesService } from '../../../Service/Test/evaluaciones';
import { IEvaluacion } from '../../../Interfaces/Evaluaciones/ievaluacion';
import { PacienteService } from '../../../Service/paciente';
import { IPaciente } from '../../../Interfaces/ipaciente';
import { IEvaluadores } from '../../../Interfaces/Configuraciones/ievaluadores';
import { EvaluadoresService } from '../../../Service/Configuraciones/evaluadores';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PdfService } from '../../../Service/Test/pdf';


@Component({
  selector: 'app-deatlle-evaluacion',
  imports: [
    CommonModule,
    FormsModule

  ],
  templateUrl: './deatlle-evaluacion.html',
  styleUrls: ['./deatlle-evaluacion.css']
})
export class DeatlleEvaluacionComponent implements OnInit {

  constructor(
    private titleService: Title,
    private parametros: ActivatedRoute,
    private evaluacionesService: EvaluacionesService,
    private pacientesService: PacienteService,
    private evaluadoresService: EvaluadoresService,
    private pdfService: PdfService
  ) {  }

  idPaciente : number = 0;  
  idEvaluacion : number = 0;
  evaluacionDetalle: IEvaluacion=null !;
  evaluador: IEvaluadores = null!;
  paciente: IPaciente = null!;
  ngOnInit(): void {
    this.obtenerParametros();
    this.cargarEvaluacion(this.idEvaluacion, this.idPaciente);
  }

  cargarEvaluacion(idEvaluacion: number, idPaciente: number) {
    this.evaluacionesService.cargarEvaluacionId(idEvaluacion).subscribe({
      next: (evaluacion) => {
        if (evaluacion.idPaciente == Number(idPaciente)) {
            this.evaluacionDetalle = evaluacion;
            this.titleService.setTitle(`Detalle ${this.evaluacionDetalle.configuracionTest?.tipoTest?.nombre} - PsycoAI`);
            console.log('Evaluación cargada:', this.evaluacionDetalle);
            this.cargarPaciente(this.evaluacionDetalle.idPaciente);
            this.cargarEvaluador(this.evaluacionDetalle.idEvaluador);
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Esta evaluación no pertenece al paciente seleccionado.',
          });
            this.parametros.paramMap.subscribe(() => {
              window.history.back();
            });
            return;
        }

      },
      error: (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'No se pudo cargar la evaluación. Por favor, inténtelo de nuevo más tarde.',
        });
        console.error('Error al cargar la evaluación:', error);
      }
    });
  }

  cargarEvaluador(idEvaluador: number) {
    this.evaluadoresService.obtenerUnEvaluador(idEvaluador).subscribe({
      next: (evaluador) => {
        console.log('Evaluador cargado:', evaluador);
        this.evaluador = evaluador;
      }
    });
  }

  async exportarAPDF() {
    const pdfBlob = await this.pdfService.generarPdf(this.evaluacionDetalle, this.paciente, this.evaluador);
    const fechaObj = this.fechaZonahoraria(new Date());
    const fecha = `${fechaObj.getFullYear()}${(fechaObj.getMonth()+1).toString().padStart(2,'0')}${fechaObj.getDate().toString().padStart(2,'0')}${fechaObj.getHours().toString().padStart(2,'0')}${fechaObj.getMinutes().toString().padStart(2,'0')}${fechaObj.getSeconds().toString().padStart(2,'0')}`;
    this.pdfService.descargarPdf(pdfBlob, `detalle_evaluacion_${this.idEvaluacion}_${fecha}.pdf`);
  }

  cargarPaciente(idPaciente: number) {
    this.pacientesService.obtenerUnPaciente(idPaciente).subscribe({
      next: (paciente) => {
        console.log('Paciente cargado:', paciente);
        this.paciente = paciente;
      },
      error: (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'No se pudo cargar el paciente. Por favor, inténtelo de nuevo más tarde.',
        });
        console.error('Error al cargar el paciente:', error);
      } 
    });
  }
  calcularEdad(fechaNacimiento: string): number {
    const nacimiento = new Date(fechaNacimiento);
    const hoy = new Date();
    let edad = hoy.getFullYear() - nacimiento.getFullYear();
    const m = hoy.getMonth() - nacimiento.getMonth();
    if (m < 0 || (m === 0 && hoy.getDate() < nacimiento.getDate())) {
      edad--;
    }
    return edad;
  }

  obtenerParametros() {
    this.parametros.params.subscribe({
      next: (data) => {
        this.idPaciente = data["idPaciente"];
        this.idEvaluacion = data["idEvaluacion"];

      }
    });
  }
  goBack(): void {
    window.history.back();
  }
  fechaZonahoraria(fecha: Date): Date {
    const fechaLocal = new Date(fecha);
    // GMT-5 is UTC-5, so offset is -5 hours in milliseconds
    const gmtMinus5Offset = -5 * 60 * 60 * 1000;
    return new Date(fechaLocal.getTime() + gmtMinus5Offset);
  }
}
