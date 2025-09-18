import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ITipoTest } from '../../../../Interfaces/Configuraciones/itipo-test';
import { TipoTestService } from '../../../../Service/Configuraciones/tipo-test';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IUsuario } from '../../../../Interfaces/Login/iusuario';
import { AccesoDenegadoComponent } from '../../../../layout/acceso-denegado/acceso-denegado';

@Component({
  selector: 'app-editar-tipo-test',
  imports: [RouterLink,CommonModule,FormsModule, AccesoDenegadoComponent],
  templateUrl: './editar-tipo-test.html',
  styleUrls: ['./editar-tipo-test.css']
})
export class EditarTipoTestComponent {

  tipoTest: ITipoTest;

  constructor(
    private tipoTestService: TipoTestService, 
    private parametro: ActivatedRoute,
    private router: Router
  ) {
    this.tipoTest = {
      id: 0,
      nombre: '',
      descripcion: '',
      instrucciones: '',
      creado: new Date(),
      actualizado: new Date(), 
      eliminado: false
    };
  }

  

  ngOnInit(): void {
    Swal.fire({
      title: 'Cargando...',
      allowOutsideClick: false,
      didOpen: () => {
      Swal.showLoading();
      }
    });

    const id = this.parametro.snapshot.paramMap.get('id');
    if (id) {
      this.verificarSesion();
      this.tipoTestService.getTipoTestById(id).subscribe({
      next: (data) => {
        this.tipoTest = data;
        Swal.close();
      },
      error: (error) => {
        Swal.close();
        Swal.fire('Error', error.message, 'error');
      }
      });
    } else {
      Swal.close();
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

  onSubmit(): void {
    this.tipoTestService.updateTipoTest(this.tipoTest).subscribe({
      next: () => {
        Swal.fire('Éxito', 'Tipo de test actualizado correctamente', 'success');
      },
      error: (error) => {
        Swal.fire('Error', error.message, 'error');
      }
    });
  }
}
