import { Component } from '@angular/core';
import { IUsuario } from '../../../../Interfaces/Login/iusuario';
import { ActivatedRoute, Router } from '@angular/router';
import { AccesoDenegadoComponent } from '../../../../layout/acceso-denegado/acceso-denegado';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from '../../../../Service/Login/usuario';

@Component({
  selector: 'app-configurar-password',
  imports: [
    AccesoDenegadoComponent,
    CommonModule,
    FormsModule
  ],
  templateUrl: './configurar-password.html',
  styleUrl: './configurar-password.css'
})
export class ConfigurarPasswordComponent {

  usuario: IUsuario = {
    id: 0,
    usuario: '',
    password: '',
    rol: 'PACIENTE',
    idEvaluador: null,
    idPaciente: null,
    creado: new Date(),
    eliminado: false
  }
  confirmPassword: string = '';
  nombreUsuario: string = '';
  idUsuario: number = 0;
  rol: string = '';
  showPassword: boolean = false;
  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
  constructor( 
    private router: Router,
    private parametros: ActivatedRoute,
    private usuarioService: UsuarioService
    
  ) {}
  ngOnInit(): void {
    this.verificarSesion();
    this.cargarDatosUsuario();

  }
  cambiarPassword() {
    if (this.usuario.password !== this.confirmPassword) {
      alert('Las contraseñas no coinciden');
      return;
    }
    this.usuarioService.actualizarUsuario(this.usuario.id, this.usuario).subscribe({
      next: () => {
        alert('Contraseña cambiada con éxito');
        this.router.navigate(['/perfil']);
      },
      error: (err) => {
        console.error('Error al cambiar contraseña:', err);
      }
    });
  }

  cargarDatosUsuario() {
    this.usuarioService.cargarUnUsuario(this.sesion?.id!).subscribe({
      next: (data) => {
        this.usuario = data as IUsuario;
        this.nombreUsuario = this.usuario.usuario;
      },
      error: (err) => {
        console.error('Error al cargar usuario:', err);
      }
    });
  }
  rolesValidos: string[] = ['ADMIN','EVALUADOR','PACIENTE'];
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
        
        return;
      }
      if(!this.validarSiEsElMismoIdSesion(this.sesion!)){
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
  validarSiEsElMismoIdSesion(sesion: IUsuario): boolean {
    
    this.parametros.params.subscribe((parametros) => {
    if (parametros['idUsuario']) {
        this.idUsuario = +parametros['idUsuario']; // Convertir a número
    }

    });
    return sesion.id === this.idUsuario;
  }
}
