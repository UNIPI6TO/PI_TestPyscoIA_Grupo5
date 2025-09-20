import { Component } from '@angular/core';
import { IUsuario } from '../../../../Interfaces/Login/iusuario';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AccesoDenegadoComponent } from '../../../../layout/acceso-denegado/acceso-denegado';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { EvaluadoresService } from '../../../../Service/Configuraciones/evaluadores';
import { IEvaluadores } from '../../../../Interfaces/Configuraciones/ievaluadores';
import { UsuarioService } from '../../../../Service/Login/usuario';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-configurar-usuario',
  imports: [
    FormsModule,
    CommonModule,
    AccesoDenegadoComponent,
    RouterLink,
  ],
  templateUrl: './configurar-usuario.html',
  styleUrls: ['./configurar-usuario.css']
})
export class ConfigurarUsuarioComponent {
 
  evaluador: IEvaluadores = {
    id: 0,
    creado: new Date(),
    eliminado: false,
    nombre: '',
    idCiudad: 0,
    cedula: '',
    telefono: '',
    cargo: '',
    especialidad: '',
    email: ''
  };  
  usuario: IUsuario = {
    id: 0,
    usuario: '',
    password: '',
    rol: 'EVALUADOR',
    idEvaluador: null,
    idPaciente: null,
    creado: new Date(),
    
    eliminado: false
  };
  nuevoUsuario: boolean = true;
  confirmPassword: string = '';
  constructor( private router: Router,
    private evaluadoresService: EvaluadoresService,
    private parametros: ActivatedRoute,
    private usuarioService: UsuarioService
  ) {
  }

  ngOnInit(): void {
    this.verificarSesion();
    
    this.cargarEvaluador();
    
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

  cargarUsuarioEvaluador() {
    if (this.evaluador && this.evaluador.id) {
      this.usuarioService.obtenerUsuarioEvaluador(this.evaluador.id).subscribe({
        next: (data) => {
          if (data) {
            this.usuario = data;
            this.nuevoUsuario = false;
          }
        },
        error: (error) => {
          console.error('Error al cargar el usuario del evaluador:', error);
          this.nuevoUsuario = true;
            // Sugerir el nombre de usuario en base al nombre del evaluador
            if (this.evaluador && this.evaluador.nombre) {
            // Elimina espacios y convierte a minúsculas
            this.usuario.usuario = this.evaluador.nombre.replace(/\s+/g, '').toLowerCase();
            } else {
            this.usuario.usuario = '';
            }
        }
      });
    }
  }

  cargarEvaluador() {
    let evaluadorId = 0;
      this.parametros.params.subscribe((parametros) => {
      if (parametros['idEvaluador']) {
        evaluadorId = +parametros['idEvaluador']; // Convertir a número
      }
    });
    if (evaluadorId) {
      this.evaluadoresService.obtenerUnEvaluador(evaluadorId).subscribe({
        next: (data) => {
          if (data) {
            this.evaluador = data;
            this.cargarUsuarioEvaluador();
          }
        },
        error: (error) => {
          console.error('Error al cargar el evaluador:', error);
        }
      });
    }
  }
  onSubmit() {
    this.usuarioService.usuarioExiste(this.usuario.usuario).subscribe({
          next: (data) => {
            if (data && this.nuevoUsuario) {  
              Swal.fire({
                icon: 'error',
                title: 'Error',
                text: `El nombre de usuario ${this.usuario.usuario} ya existe. Por favor, elige otro.`
              });
              return;
            }
          }
    });
    // Aquí puedes manejar el envío del formulario, por ejemplo, enviando los datos a un servicio
    if (this.nuevoUsuario) {
      // Lógica para crear un nuevo usuario
      this.usuario.idEvaluador = this.evaluador.id;
      console.log(JSON.stringify(this.usuario));
      this.usuarioService.crearUsuario(this.usuario).subscribe({
        next: (data) => {
          
          Swal.fire({
            icon: 'success',
            title: 'Usuario creado',
            text: 'El usuario ha sido creado exitosamente.'
          }); 
        },
        error: (error) => {
          console.error('Error al crear usuario:', error);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Hubo un problema al crear el usuario. Inténtalo de nuevo.'
          });
        }
      });
    }else{
      // Lógica para actualizar un usuario existente
      this.usuario.idEvaluador = this.evaluador.id;
      this.usuarioService.actualizarUsuario(this.usuario.id, this.usuario).subscribe({
        next: (data) => {
          Swal.fire({
            icon: 'success',
            title: 'Usuario actualizado',
            text: 'El usuario ha sido actualizado exitosamente.'
          }); 
        },
        error: (error) => {
          console.error('Error al actualizar usuario:', error);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Hubo un problema al actualizar el usuario. Inténtalo de nuevo.'
          });
        }
      });
    }
  }
  goBack() {
    window.history.back();
  }
}
