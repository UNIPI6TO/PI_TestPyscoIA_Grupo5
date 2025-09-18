import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ITipoTest } from '../../../../Interfaces/Configuraciones/itipo-test';
import { TipoTestService } from '../../../../Service/Configuraciones/tipo-test';
import Swal from 'sweetalert2';

import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccesoDenegadoComponent } from '../../../../layout/acceso-denegado/acceso-denegado';
import { IUsuario } from '../../../../Interfaces/Login/iusuario';

@Component({
  selector: 'app-nuevo-tipo-test',
  imports: [
    RouterLink,
    FormsModule,
    CommonModule,
    AccesoDenegadoComponent
  ],
  templateUrl: './nuevo-tipo-test.html',
  styleUrls: ['./nuevo-tipo-test.css']
})
export class NuevoTipoTestComponent implements OnInit {
  constructor(
    private tipoTestService: TipoTestService,
    private router: Router
  ) { }
  tipoTest: ITipoTest = {
    id: 0,
    nombre: '',
    descripcion: '',
    instrucciones: '',
    creado: new Date(),
    
    eliminado: false
  };
  ngOnInit(): void {
    this.verificarSesion();
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

  

  onSubmit() {
    this.tipoTestService.createTipoTest(this.tipoTest).subscribe({
      next: () => {
        Swal.fire('Éxito', 'Tipo de evaluación creado correctamente', 'success');
        this.router.navigate(['/configuraciones/tipo-test']);
      },
      error: (error) => {
        Swal.fire('Error', error.message, 'error');
      }
    });
  }
}
