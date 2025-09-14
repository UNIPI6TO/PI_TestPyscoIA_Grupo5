import { Injectable } from '@angular/core';
import { IConfigOpciones } from '../../Interfaces/Configuraciones/iconfig-opciones';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigOpcionesService {
  constructor(private http: HttpClient) { }
  private API_URL = environment.apiUrl;
  private CONTEXT = 'api/config';
  manejoErrores(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => {
      new Error(msg);
    });
  }
  agregarOpcion(opcion: IConfigOpciones): Observable<IConfigOpciones> {
    return this.http.post<IConfigOpciones>(`${this.API_URL}${this.CONTEXT}/ConfiguracionOpciones`, opcion)
      .pipe(
        catchError(this.manejoErrores)
      );
  }
  modificarOrden(opciones: IConfigOpciones[]): Observable<IConfigOpciones[]>[] {
    return opciones.map(opcion => this.http.put<IConfigOpciones[]>(`${this.API_URL}${this.CONTEXT}/ConfiguracionOpciones/${opcion.id}`, opcion).pipe(
      catchError(this.manejoErrores)
    ));
  }
  eliminarOpcion(id: number): Observable<void> {
    return this.http
      .delete<void>(`${this.API_URL}${this.CONTEXT}/ConfiguracionOpciones/${id}`)
      .pipe(catchError(this.manejoErrores));
  }
}
