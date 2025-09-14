import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { catchError, throwError } from 'rxjs';

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
    return this.http.get(`${this.API_URL}${this.CONTEXT}/Evaluadores`).pipe(
      catchError(this.manejoErrores)
    );
  }

}
