import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { catchError, throwError } from 'rxjs';
import { IEvaluadores } from '../../Interfaces/Configuraciones/ievaluadores';

@Injectable({
  providedIn: 'root'
})
export class EvaluadoresService {
  constructor(private http: HttpClient) { }
  private API_URL = environment.apiUrl;
  private CONTEXT = 'api/config';
  
  manejoErrores(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => {
      new Error(msg);
    });
  }
  getEvaluadores() {
    return this.http.get(`${this.API_URL}${this.CONTEXT}/Evaluador`).pipe(
      catchError(this.manejoErrores)
    );
  }
  obtenerUnEvaluador(id: number) {
    return this.http.get<IEvaluadores>(`${this.API_URL}${this.CONTEXT}/Evaluador/${id}`).pipe(
      catchError(this.manejoErrores)
    );
  }
  guardarEvaluador(evaluador: IEvaluadores) {
    return this.http
      .post<IEvaluadores>(`${this.API_URL}${this.CONTEXT}/Evaluador`, evaluador)
      .pipe(catchError(this.manejoErrores));
  }

  editarEvaluador(evaluador: IEvaluadores) {
    return this.http
      .put<IEvaluadores>(`${this.API_URL}${this.CONTEXT}/Evaluador/${evaluador.id}`, evaluador)
      .pipe(catchError(this.manejoErrores));
  }
  eliminarEvaluador(id: number) {
    return this.http
      .delete<void>(`${this.API_URL}${this.CONTEXT}/Evaluador/${id}`)
      .pipe(catchError(this.manejoErrores));
  }
}
