import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from '../../../Service/Login/usuario';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicio-sesion',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './inicio-sesion.html',   
  styleUrls: ['./inicio-sesion.css']
})
export class InicioSesionComponent {
  // --- Login ---
  usuario: string = '';
  password: string = '';

  // --- Registro ---
  mostrarRegistro: boolean = false;
  cedula: string = '';
  nuevoUsuario: string = '';
  nuevaPassword: string = '';

  constructor(
    private usuarioService: UsuarioService,
    private router: Router
  ) {}

  login() {
    if (!this.usuario || !this.password) {
      Swal.fire({ icon: 'warning', title: 'Campos vacíos', text: 'Debes ingresar usuario y contraseña' });
      return;
    }

    this.usuarioService.iniciarSesion(this.usuario, this.password).subscribe(
      response => {
        const expires = Date.now() + 10 * 60 * 1000;
        document.cookie = `username=${encodeURIComponent(JSON.stringify(response))}; path=/; expires=${new Date(expires).toUTCString()}; SameSite=Strict; Secure`;
        sessionStorage.setItem('username', JSON.stringify(response));

        Swal.fire({ icon: 'success', title: 'Inicio de sesión exitoso', text: '¡Bienvenido!' });
        this.router.navigate(['']).then(() => window.location.reload());
      },
      error => {
        Swal.fire({ icon: 'error', title: 'Error', text: error?.message || 'Usuario o contraseña incorrectos' });
      }
    );
  }

  registrarPerfil() {
    if (!this.cedula || !this.nuevoUsuario || !this.nuevaPassword) {
      Swal.fire({
        icon: 'warning',
        title: 'Campos vacíos',
        text: 'Debe ingresar cédula, usuario y contraseña'
      });
      return;
    }

    const data = { cedula: this.cedula, usuario: this.nuevoUsuario, password: this.nuevaPassword };

    this.usuarioService.registrarPerfilPaciente(data).subscribe(
      (res: any) => {
        Swal.fire({
          icon: 'success',
          title: 'Perfil creado',
          text: res.message || 'Usuario registrado correctamente'
        });
        // limpiar y volver al login
        this.cedula = '';
        this.nuevoUsuario = '';
        this.nuevaPassword = '';
        this.mostrarRegistro = false;
      },
      error => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: error?.message || error?.error?.message || 'No se pudo registrar el perfil'
        });
      }
    );
  }
}
