import { Component, OnInit } from '@angular/core';
import { IEvaluadores } from '../../../Interfaces/Configuraciones/ievaluadores';
import { EvaluadoresService } from '../../../Service/Configuraciones/evaluadores';
import Swal from 'sweetalert2';
import { Router, RouterLink } from '@angular/router';
import { IUsuario } from '../../../Interfaces/Login/iusuario';
import { AccesoDenegadoComponent } from '../../../layout/acceso-denegado/acceso-denegado';

@Component({
  selector: 'app-config-evaluadores',
  imports: [
    RouterLink,
    AccesoDenegadoComponent
  ],
  templateUrl: './config-evaluadores.html',
  styleUrls: ['./config-evaluadores.css']
})
export class ConfigEvaluadoresComponent implements OnInit {
  evaluadores: IEvaluadores[] = [];
  
  constructor(
    private evaluadoresService: EvaluadoresService,
    private router: Router
  ) { }

  ngOnInit(): void {
    // Mostrar spinner de carga con SweetAlert2
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      Swal.showLoading();
      }
    });
      this.verificarSesion();

      this.evaluadoresService.getEvaluadores().subscribe({
      next: (data) => {
      this.evaluadores = data as IEvaluadores[];
      Swal.close();
      },
      error: (err) => {
      Swal.close();
      Swal.fire('Error', 'Error al cargar evaluadores', 'error');
      console.error('Error al cargar evaluadores:', err);
      }
    });
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

  cargarEvaluadores() {
    this.evaluadoresService.getEvaluadores().subscribe({
      next: (data) => {
        this.evaluadores = data as IEvaluadores[];
        console.log('Evaluadores cargados:', this.evaluadores);
      },
      error: (err) => {
        console.error('Error al cargar evaluadores:', err);
      }
    });
  }

  eliminarEvaluador(evaluador: IEvaluadores) {
    Swal.fire({
      title: '¿Estás seguro?',
      text: `Esta acción eliminará al evaluador ${evaluador.nombre}.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.evaluadoresService.eliminarEvaluador(evaluador.id!).subscribe({
          next: () => {
            this.evaluadores = this.evaluadores.filter(e => e.id !== evaluador.id);
          },
          error: (err) => {
            Swal.fire('Error', 'Error al eliminar el evaluador', 'error');
            console.error('Error al eliminar el evaluador:', err);
          }
        });
      }
    });
  }

}
