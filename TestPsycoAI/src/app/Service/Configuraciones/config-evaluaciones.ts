import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { catchError, Observable, throwError } from 'rxjs';
import { IConfigEvaluacionesResumen } from '../../Interfaces/Configuraciones/iconfig-evaluaciones-resumen';
import { IConfigEvaluaciones } from '../../Interfaces/Configuraciones/iconfig-evaluaciones';

@Injectable({
  providedIn: 'root'
})
export class ConfigEvaluacionesService {

    constructor(private http: HttpClient) { }
    private API_URL = environment.apiUrl;
    private CONTEXT = 'api/config';
  manejoErrores(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => {
      new Error(msg);
    });
  }

  getConfigEvaluaciones(): Observable<IConfigEvaluacionesResumen[]> {
    var configEvaluaciones = this.http
      .get<IConfigEvaluacionesResumen[]>(this.API_URL + this.CONTEXT + '/ConfiguracionTest/resumen')
      .pipe(catchError(this.manejoErrores));
    return configEvaluaciones;
  }
  getUnaConfigEvaluacion(id: number): Observable<IConfigEvaluacionesResumen> {
    var configEvaluacion = this.http
      .get<IConfigEvaluacionesResumen>(`${this.API_URL}${this.CONTEXT}/ConfiguracionTest/detalle/${id}`)
      .pipe(catchError(this.manejoErrores));
    return configEvaluacion;
  }
  eliminarEvaluacion(id: number): Observable<void> {
    return this.http
      .delete<void>(`${this.API_URL}${this.CONTEXT}/ConfiguracionTest/${id}`)
      .pipe(catchError(this.manejoErrores));
  }
  editarEvaluacion(evaluacion: IConfigEvaluaciones): Observable<void> {
    return this.http
      .put<void>(`${this.API_URL}${this.CONTEXT}/ConfiguracionTest/${evaluacion.id}`, evaluacion)
      .pipe(catchError(this.manejoErrores));
  }
  agregarEvaluacion(nuevaEvaluacion: IConfigEvaluaciones) {
    return this.http
      .post<IConfigEvaluaciones>(`${this.API_URL}${this.CONTEXT}/ConfiguracionTest`, nuevaEvaluacion)
      .pipe(catchError(this.manejoErrores));
  }
}
