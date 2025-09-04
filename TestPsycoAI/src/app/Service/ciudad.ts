import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { ICiudad } from '../Interfaces/iciudad'; // Asegúrate de que esta ruta sea correcta
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CiudadService {
  private API_URL = environment.apiUrl;
  private CONTEXT = '/api/config';

  constructor(private http: HttpClient) { }

  getCiudades(): Observable<ICiudad[]> {
    return this.http
      .get<ICiudad[]>(`${this.API_URL}${this.CONTEXT}/Ciudad`)
      .pipe(catchError(this.manejoErrores));
  }

  manejoErrores(error: any) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    console.error(msg);
    return throwError(() => new Error(msg));
  }
}