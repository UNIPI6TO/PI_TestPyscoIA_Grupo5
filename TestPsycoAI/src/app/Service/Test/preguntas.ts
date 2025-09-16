
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IPreguntas } from '../../Interfaces/Evaluaciones/ipreguntas';

@Injectable({
  providedIn: 'root'
})
export class PreguntasService {
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
  actualizarPregunta(pregunta: IPreguntas): Observable<IPreguntas> {
    return this.http.put<IPreguntas>(`${this.API_URL}${this.CONTEXT}/pregunta`, pregunta).pipe(
      catchError(this.manejoErrores)
    );
  }
}
