import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { IUsuario } from '../../Interfaces/Login/iusuario';

@Component({
  selector: 'app-home',
  standalone: true, 
  imports: [
    RouterLink
  ],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class HomeComponent {

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
}
