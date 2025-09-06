import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IPaciente } from '../Interfaces/ipaciente';
import { ICiudad } from '../Interfaces/iciudad';

@Injectable({
  providedIn: 'root'
})

export class PacienteService {
  private API_URL = environment.apiUrl;
  private CONTEXT = 'api/config';
  constructor(private http: HttpClient) {}
  getPacientes(): Observable<IPaciente[]> {
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

  guardarPaciente(paciente: IPaciente): Observable<IPaciente> {
    return this.http
      .post<IPaciente>(this.API_URL + this.CONTEXT + '/Paciente', paciente)
      .pipe(catchError(this.manejoErrores));
  }
}
