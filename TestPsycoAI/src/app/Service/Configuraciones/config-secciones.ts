import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IConfigSecciones } from '../../Interfaces/Configuraciones/iconfig-secciones';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigSeccionesService {
  constructor(private http: HttpClient) { }
  private API_URL = environment.apiUrl;
  private CONTEXT = 'api/config';
  
  manejoErrores(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => {
      new Error(msg);
    });
  }

  agregarSeccion(seccion: any): Observable<IConfigSecciones> {
    return this.http
      .post<IConfigSecciones>(`${this.API_URL}${this.CONTEXT}/ConfiguracionSecciones`, seccion)
      .pipe(catchError(this.manejoErrores));
  }
  eliminarSeccion(id: number): Observable<void> {
    return this.http
      .delete<void>(`${this.API_URL}${this.CONTEXT}/ConfiguracionSecciones/${id}`)
      .pipe(catchError(this.manejoErrores));
  }
}
