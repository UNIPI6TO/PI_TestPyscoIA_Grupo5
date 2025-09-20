import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from '../../../Service/Login/usuario';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';


@Component({
  selector: 'app-inicio-sesion',
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './inicio-sesion.html',
  styleUrls: ['./inicio-sesion.css']
})
export class InicioSesionComponent {
  constructor(
    private usuarioService: UsuarioService,
    private router: Router
  ) {}
  usuario: string = '';
  password: string = '';
  login() {
    this.usuarioService.iniciarSesion(this.usuario, this.password).subscribe(
      response => {
        console.log('Respuesta del servidor:', response);
        
        const expires = Date.now() + 60 * 60 * 1000;
        document.cookie = `username=${encodeURIComponent(JSON.stringify(response))}; path=/; expires=${new Date(expires).toUTCString()}; SameSite=Strict; Secure`;
        sessionStorage.setItem('username', JSON.stringify(response));
        
        Swal.fire({
          icon: 'success',
          title: 'Inicio de sesión exitoso',
          text: 'Bienvenido!'
        });
        this.router.navigate(['']).then(() => {
          window.location.reload();
        });
      },
      error => {
       
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'Usuario o contraseña incorrectos'
        });
      }
    );
  }
  
}
