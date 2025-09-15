import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IEvaluacion } from '../../Interfaces/Evaluaciones/ievaluacion';

@Injectable({
  providedIn: 'root'
})
export class EvaluacionesService {
   private API_URL = environment.apiUrl;
  private CONTEXT = 'api/Evaluacion';

  constructor(private http: HttpClient) { }
    manejoErrores(error: any) {
      const msg = error.error?.message || error.statusText || 'Error de red';
      console.error(msg);
      return throwError(() => new Error(msg));
    }
  obtenerEvaluacionesPorPaciente(idPaciente: number): Observable<IEvaluacion[]> {
    return this.http.get<IEvaluacion[]>(`${this.API_URL}${this.CONTEXT}/Paciente/${idPaciente}`).pipe(
      catchError(this.manejoErrores)
    );

  }
}

