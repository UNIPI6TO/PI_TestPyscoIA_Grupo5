import { Injectable } from '@angular/core';
import { IConfigEvaluaciones } from '../../Interfaces/Configuraciones/iconfig-evaluaciones';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { catchError, Observable, throwError } from 'rxjs';
import { IConfigEvaluacionesResumen } from '../../Interfaces/Configuraciones/iconfig-evaluaciones-resumen';

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
}
