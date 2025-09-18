import { Component, OnInit } from '@angular/core';
import { IConfigEvaluacionesResumen } from '../../../../Interfaces/Configuraciones/iconfig-evaluaciones-resumen';
import { ConfigEvaluacionesService } from '../../../../Service/Configuraciones/config-evaluaciones';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { IUsuario } from '../../../../Interfaces/Login/iusuario';
import { AccesoDenegadoComponent } from '../../../../layout/acceso-denegado/acceso-denegado';

@Component({
  selector: 'app-config-evaluaciones-detalle',
  imports: [CommonModule, FormsModule, RouterLink, AccesoDenegadoComponent],
  templateUrl: './config-evaluaciones-detalle.html',
  styleUrls: ['./config-evaluaciones-detalle.css']
})
export class ConfigEvaluacionesDetalleComponent implements OnInit {
  evaluacion: IConfigEvaluacionesResumen;
  constructor(private configuracionesService: ConfigEvaluacionesService, private route: ActivatedRoute,private router: Router) {
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


}
