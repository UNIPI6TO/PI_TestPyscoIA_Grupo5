import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IDatosEntradaAI } from '../../Interfaces/Evaluaciones/idatos-entrada-ai';
import { IRespuestaAI } from '../../Interfaces/Evaluaciones/irespuesta-ai';

@Injectable({
  providedIn: 'root'
})
export class EvaluacionAIService {
  private API_URL = environment.apiUrl;
  private CONTEXT = 'AI/Predecir/';

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
  predecirResultadosAI(datosEntradaAI: IDatosEntradaAI, nombreEvaluacion: string): Observable<IRespuestaAI> {
    nombreEvaluacion = nombreEvaluacion.normalize('NFD').replace(/[\u0300-\u036f]/g, '');
    nombreEvaluacion = nombreEvaluacion.charAt(0).toUpperCase() + nombreEvaluacion.slice(1).toLowerCase();
    console.log(`${this.API_URL}${this.CONTEXT}${nombreEvaluacion}`);
    console.log(JSON.stringify(datosEntradaAI));
    var response = this.http.post<IRespuestaAI>(`${this.API_URL}${this.CONTEXT}${nombreEvaluacion}`, datosEntradaAI).pipe(
      catchError(this.manejoErrores)
    );
    console.log(response);
    return response;
  }

}
