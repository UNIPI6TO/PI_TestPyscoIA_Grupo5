import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,  Router,  RouterLink } from '@angular/router';
import { IUsuario } from '../../Interfaces/Login/iusuario';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrls: ['./header.css']
})
export class Header implements OnInit {

  constructor(private router: Router) { }
  
  ngOnInit(): void {
    this.obtenerSesion();
  }
  sesion: IUsuario | null = null;
  iniciadaSesion: boolean = false;
  obtenerSesion(){
    const match = document.cookie.match(new RegExp('(^| )username=([^;]+)'));
    if (match) {
      const username = JSON.parse(decodeURIComponent(match[2]));
      this.sesion = username;
      
      this.iniciadaSesion = true;

      
    } else {
      this.sesion = null;
      this.iniciadaSesion = false;
    }
  }

  cerrarSesion() {
    this.sesion = null;
    this.iniciadaSesion = false;
    document.cookie = 'username=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT; SameSite=Strict; Secure';
    sessionStorage.removeItem('username');
    this.router.navigate(['/iniciar-sesion']);
  }
  validarConocenos(): boolean {
    // Obtener la ruta actual del navegador
    const rutaActual = this.router.url;
   
    return rutaActual === '/conocenos';
  }
}
