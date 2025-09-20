import { Component } from '@angular/core';
import { IUsuario } from '../../../../Interfaces/Login/iusuario';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AccesoDenegadoComponent } from '../../../../layout/acceso-denegado/acceso-denegado';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UsuarioService } from '../../../../Service/Login/usuario';
import Swal from 'sweetalert2';
import { IPaciente } from '../../../../Interfaces/ipaciente';
import { PacienteService } from '../../../../Service/paciente';

@Component({
  selector: 'app-configurar-usuario-paciente',
  imports: [
    FormsModule,
    CommonModule,
    AccesoDenegadoComponent,
    RouterLink,
  ],
  templateUrl: './configurar-usuario-paciente.html',
  styleUrls: ['./configurar-usuario-paciente.css']
})
export class ConfigurarUsuarioPacienteComponent {
 
  paciente: IPaciente = {
    id: 0,
    creado: new Date(),
    eliminado: false,
    nombre: '',
    idCiudad: 0,
    cedula: '',
    email: '',
    fechaNacimiento: '',
    direccion: ''
  };  
  usuario: IUsuario = {
    id: 0,
    usuario: '',
    password: '',
    rol: 'PACIENTE',
    idEvaluador: null,
    idPaciente: null,
    creado: new Date(),
    
    eliminado: false
  };
  nuevoUsuario: boolean = true;
  confirmPassword: string = '';
  constructor( private router: Router,
    private pacienteService: PacienteService,
    private parametros: ActivatedRoute,
    private usuarioService: UsuarioService
  ) {
  }

  ngOnInit(): void {
    this.verificarSesion();
    
    this.cargarPaciente();
    
  }

  rolesValidos: string[] = ['ADMIN','EVALUADOR'];
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

  cargarUsuarioPaciente() {
    if (this.paciente && this.paciente.id) {
      this.usuarioService.obtenerUsuarioPaciente(this.paciente.id).subscribe({
        next: (data) => {
          if (data) {
            this.usuario = data;
            this.nuevoUsuario = false;
          }
        },
        error: (error) => {
          console.error('Error al cargar el usuario del evaluador:', error);
          this.nuevoUsuario = true;
            // Sugerir el nombre de usuario en base al nombre del paciente
            if (this.paciente && this.paciente.nombre) {
            // Elimina espacios y convierte a minúsculas
            this.usuario.usuario = this.paciente.nombre.replace(/\s+/g, '').toLowerCase();
            } else {
            this.usuario.usuario = '';
            }
        }
      });
    }
  }

  cargarPaciente() {
    let pacienteId = 0;
      this.parametros.params.subscribe((parametros) => {
      if (parametros['idPaciente']) {
        pacienteId = +parametros['idPaciente']; // Convertir a número
      }
    });
    if (pacienteId) {
      this.pacienteService.obtenerUnPaciente(pacienteId).subscribe({
        next: (data) => {
          if (data) {
            this.paciente = data;
            this.cargarUsuarioPaciente();
          }
        },
        error: (error) => {
          console.error('Error al cargar el paciente :', error);
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
    if (this.nuevoUsuario) {
      // Lógica para crear un nuevo usuario
      this.usuario.idPaciente = this.paciente.id;
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
      this.usuario.idPaciente = this.paciente.id;
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
