import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { catchError, Observable, throwError } from 'rxjs';
import { IConfigPreguntas } from '../../Interfaces/Configuraciones/iconfig-preguntas';

@Injectable({
  providedIn: 'root'
})
export class ConfigPreguntasService {
  
  constructor(private http: HttpClient) { }
  private API_URL = environment.apiUrl;
  private CONTEXT = 'api/config';
  manejoErrores(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => {
      new Error(msg);
    });
  }  
  agregarPregunta(pregunta: IConfigPreguntas): Observable<IConfigPreguntas> {
    return this.http.post<IConfigPreguntas>(`${this.API_URL}${this.CONTEXT}/ConfiguracionPreguntas`, pregunta)
      .pipe(
        catchError(this.manejoErrores)
      );
  }
  editarPregunta(pregunta: IConfigPreguntas): Observable<IConfigPreguntas> {
    return this.http.put<IConfigPreguntas>(`${this.API_URL}${this.CONTEXT}/ConfiguracionPreguntas/${pregunta.id}`, pregunta)
      .pipe(
        catchError(this.manejoErrores)
      );
  }
  
  eliminarPregunta(id: number): Observable<void> {
    return this.http
      .delete<void>(`${this.API_URL}${this.CONTEXT}/ConfiguracionPreguntas/${id}`)
      .pipe(catchError(this.manejoErrores));
  }
}
