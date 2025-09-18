import { Component } from '@angular/core';
import { ActivatedRoute,  Router,  RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {

  constructor(private router: Router) { }

  validarConocenos(): boolean {
    // Obtener la ruta actual del navegador
    const rutaActual = this.router.url;
    console.log('Ruta de navegación:', rutaActual);
    return rutaActual === '/conocenos';
  }
}
