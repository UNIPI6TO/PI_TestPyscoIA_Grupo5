import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ITipoTest } from '../../../Interfaces/Configuraciones/itipo-test';
import { TipoTestService } from '../../../Service/Configuraciones/tipo-test';
import Swal from 'sweetalert2';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { IUsuario } from '../../../Interfaces/Login/iusuario';
import { AccesoDenegadoComponent } from '../../../layout/acceso-denegado/acceso-denegado';
import { Title } from '@angular/platform-browser';


@Component({
  selector: 'app-tipo-test',
  imports: [
    DatePipe, 
    CommonModule, 
    RouterLink, 
    RouterModule,
    AccesoDenegadoComponent
  ],
  templateUrl: './tipo-test.html',
  styleUrls: ['./tipo-test.css']
})
export class TipoTestComponent implements OnInit {

  evaluaciones: ITipoTest[] = [];
  constructor(
    private tipoTestService: TipoTestService,
    private router: Router,
    private titleService: Title
  ) { }
  ngOnInit(): void {
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      Swal.showLoading();
      }
    });
    this.titleService.setTitle('Gestión de Tipos de Evaluaciones - PsycoAI');
    this.verificarSesion();
    this.cargarEvaluaciones();

    Swal.close();
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
    this.tipoTestService.getTipoTest().subscribe({
      next: (data) => {
        this.evaluaciones = data;
      },
      error: (error) => {
        Swal.fire('Error', error.message, 'error');
      }
    });
  }

  

  eliminarTipoTest(evaluacion: ITipoTest) {
    Swal.fire({
      title: '¿Estás seguro?',
      text: `¿Quieres eliminar el tipo de evaluación "${evaluacion.nombre}"?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.tipoTestService.eliminarTipoTest(evaluacion.id!).subscribe({
          next: () => {
            Swal.fire('Eliminado', 'El tipo de evaluación ha sido eliminado.', 'success');
            this.cargarEvaluaciones();
          },
          error: (error) => {
            Swal.fire('Error', error.message, 'error');
          }
        });
      }
    });
  }
}