import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IInicioSesion, IUsuario } from '../../Interfaces/Login/iusuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private API_URL = environment.apiUrl;
  private CONTEXT = 'api/Usuario';

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

  iniciarSesion(username: string, password: string) {
    const payload: IInicioSesion = {
      usuario: username,
      password: password
    };
    return this.http.post(`${this.API_URL}${this.CONTEXT}/Login`, payload).pipe(
      catchError(this.manejoErroresHttp)
    );
  }
  obtenerUsuarioEvaluador(idEvaluador: number): Observable<IUsuario> {
    return this.http.get<IUsuario>(`${this.API_URL}${this.CONTEXT}/Evaluador/${idEvaluador}`).pipe(
      catchError(this.manejoErrores)
    );
  }
  obtenerUsuarioPaciente(idPaciente: number): Observable<IUsuario> {
    return this.http.get<IUsuario>(`${this.API_URL}${this.CONTEXT}/Paciente/${idPaciente}`).pipe(
      catchError(this.manejoErrores)
    );
  }
  crearUsuario(usuario: IUsuario): Observable<IUsuario> {
    console.log(`${this.API_URL}${this.CONTEXT}`);
    return this.http.post<IUsuario>(`${this.API_URL}${this.CONTEXT}`, usuario).pipe(
      catchError(this.manejoErrores)
    );
  }
  actualizarUsuario(id: number, usuario: IUsuario): Observable<IUsuario> {
    return this.http.put<IUsuario>(`${this.API_URL}${this.CONTEXT}/${id}`, usuario).pipe(
      catchError(this.manejoErrores)
    );
  }
  usuarioExiste(usuario: string): Observable<IUsuario> {
    return this.http.get<IUsuario>(`${this.API_URL}${this.CONTEXT}/Existe/${usuario}`).pipe(
      catchError(this.manejoErrores)
    );
  }
  cargarUnUsuario(id: number): Observable<IUsuario> {
    return this.http.get<IUsuario>(`${this.API_URL}${this.CONTEXT}/${id}`).pipe(
      catchError(this.manejoErrores)
    );
  }

}
