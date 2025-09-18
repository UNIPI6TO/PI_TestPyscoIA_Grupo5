import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { IInicioSesion } from '../../Interfaces/Login/iusuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private API_URL = environment.apiUrl;
  private CONTEXT = 'api/Usuario';

  constructor(private http: HttpClient) { }
  
  manejoErrores(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => {
      new Error(msg);
      console.error(msg);
    });
  }
  manejoErroresHttp(error: any) {
    const msg = error.error?.message || error.error || error.statusText || 'Error de red';
    return throwError(() => new Error(msg));
  }

  iniciarSesion(username: string, password: string) {
    const payload: IInicioSesion = {
      usuario: username,
      password: password
    };
    return this.http.post(`${this.API_URL}${this.CONTEXT}/Login`, payload).pipe(
      catchError(this.manejoErroresHttp)
    );
  }

}
