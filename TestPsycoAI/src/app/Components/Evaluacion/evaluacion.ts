import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IEvaluacion } from '../../Interfaces/Evaluaciones/ievaluacion';
import { PacienteService } from '../../Service/paciente';
import { EvaluacionesService } from '../../Service/Test/evaluaciones';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IPaciente } from '../../Interfaces/ipaciente';
import {  Router, RouterLink } from '@angular/router';
import { IUsuario } from '../../Interfaces/Login/iusuario';
import Swal from 'sweetalert2';
import { AccesoDenegadoComponent } from '../../layout/acceso-denegado/acceso-denegado';



@Component({
  selector: 'app-evaluacion',
  imports: [
    CommonModule,
    FormsModule,
    RouterLink,
    AccesoDenegadoComponent
],
  templateUrl: './evaluacion.html',
  styleUrls: ['./evaluacion.css']
})
export class EvaluacionComponent implements OnInit {
  constructor(
    private titleService: Title, 
    private pacientesService: PacienteService,
    private evaluacionesService: EvaluacionesService,
    private router: Router
    ) {  }

  pacienteSelecionado: number = 0;
  evaluaciones: IEvaluacion[] = [];
  pacientes: IPaciente[] = [];
  

  ngOnInit(): void {
    Swal.fire({
      title: 'Cargando',
      text: 'Por favor espere...',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      }
    });

    this.titleService.setTitle('Evaluaciones - PsycoAI');
    this.verificarSesion();

    const finalizarCarga = () => Swal.close();

    if (this.sesion?.rol === 'PACIENTE') {
      this.pacienteSelecionado = this.sesion.idPaciente!;
      this.cargarEvaluacionesDelPaciente();
      finalizarCarga();
    } else if (this.sesion?.rol === 'ADMIN') {
      this.cargarPacientes();
      finalizarCarga();
    } else {
      finalizarCarga();
    }
    
  }
  
  rolesValidos: string[] = ['ADMIN', 'PACIENTE'];
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

  cargarPacientes() { 
    this.pacientesService.getPacientes().subscribe({
      next: (data) => {
        this.pacientes = data as IPaciente[];
        
      },
      error: (err) => {
        console.error('Error al cargar pacientes:', err);
      }
    });
  }
  cargarEvaluacionesDelPaciente() {
    if (this.pacienteSelecionado > 0) {
      this.evaluacionesService.obtenerEvaluacionesPorPaciente(this.pacienteSelecionado).subscribe({
        next: (data) => {
          this.evaluaciones = data as IEvaluacion[];
          console.log('Evaluaciones cargadas:', this.evaluaciones);
        },
        error: (err) => {
          console.error('Error al cargar evaluaciones:', err);
        }
      });
    }
  }
  onPacienteChange() {
    this.cargarEvaluacionesDelPaciente();
  }
}
