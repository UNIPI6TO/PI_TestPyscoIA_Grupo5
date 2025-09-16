import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IEvaluacion } from '../../Interfaces/Evaluaciones/ievaluacion';

@Injectable({
  providedIn: 'root'
})
export class EvaluacionesService {
  private API_URL = environment.apiUrl;
  private CONTEXT = 'api/Evaluacion';

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
  obtenerEvaluacionesPorPaciente(idPaciente: number): Observable<IEvaluacion[]> {
    return this.http.get<IEvaluacion[]>(`${this.API_URL}${this.CONTEXT}/Paciente/${idPaciente}`).pipe(
      catchError(this.manejoErrores)
    );

  }
  obtenerEvaluacionesPorPacientesActivas(idPaciente: number): Observable<IEvaluacion[]> {
    return this.http.get<IEvaluacion[]>(`${this.API_URL}${this.CONTEXT}/paciente-activo/${idPaciente}`).pipe(
      catchError(this.manejoErrores)
    );
  }
  generarEvaluacion(idPaciente: number, idConfiguracionTest: number, idEvaluador: number): Observable<IEvaluacion> {
    const payload = {
      idPaciente: idPaciente,
      idConfiguracionTest: idConfiguracionTest,
      idEvaluador: idEvaluador
    };
    return this.http.post<IEvaluacion>(`${this.API_URL}${this.CONTEXT}/generar-evaluacion`, payload).pipe(
      catchError(this.manejoErroresHttp)
    );
  }
  eliminarEvaluacion(id: number): Observable<void> {
    return this.http
      .delete<void>(`${this.API_URL}${this.CONTEXT}/${id}`)
      .pipe(catchError(this.manejoErrores));
  }
  cargarEvaluacionId(id: number): Observable<IEvaluacion> {
    return this.http.get<IEvaluacion>(`${this.API_URL}${this.CONTEXT}/${id}`).pipe(
      catchError(this.manejoErrores)
    );

  }
  actualizarEvaluacion(evaluacion: IEvaluacion): Observable<IEvaluacion> {
    return this.http.put<IEvaluacion>(`${this.API_URL}${this.CONTEXT}/${evaluacion.id}`, evaluacion).pipe(
      catchError(this.manejoErroresHttp)
    );
  }
}

