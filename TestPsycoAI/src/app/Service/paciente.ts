import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IPaciente } from '../Interfaces/ipaciente';

@Injectable({
  providedIn: 'root'
})

export class PacienteService {
  private API_URL = environment.apiUrl;
  private CONTEXT = '/api';
  constructor(private http: HttpClient) {}
  todos(): Observable<IPaciente[]> {
    var pacientes = this.http
      .get<IPaciente[]>(this.API_URL + this.CONTEXT + '/Paciente')
      .pipe(catchError(this.manejoErrores));
    console.log(pacientes);
    return pacientes;
  }
    manejoErrores(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => {
      new Error(msg);
    });
  }
  guardar(paciente: IPaciente): Observable<IPaciente> {
    return this.http.post<IPaciente>(this.API_URL + this.CONTEXT + '/Paciente', paciente).pipe(catchError(this.manejoErrores));
  }
}
