import { Injectable } from '@angular/core';
import { ITipoTest } from '../../Interfaces/Configuraciones/itipo-test';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TipoTestService {

    constructor(private http: HttpClient) { }
    private API_URL = environment.apiUrl;
    private CONTEXT = 'api/evaluacion';
    getTipoTest(): Observable<ITipoTest[]> {
        var tiposTest = this.http
          .get<ITipoTest[]>(this.API_URL + this.CONTEXT + '/TipoTest')
          .pipe(catchError(this.manejoErrores));
        console.log(tiposTest);
        return tiposTest;
    }
    getTipoTestById(id: string): Observable<ITipoTest> {
      var tipoTest = this.http
        .get<ITipoTest>(this.API_URL + this.CONTEXT + '/TipoTest/' + id)
        .pipe(catchError(this.manejoErrores));
      console.log(tipoTest);
      return tipoTest;
    }
    updateTipoTest(tipoTest: ITipoTest): Observable<void> {
      return this.http
        .put<void>(this.API_URL + this.CONTEXT + '/TipoTest/' + tipoTest.id, tipoTest)
        .pipe(catchError(this.manejoErrores));
    }

    createTipoTest(tipoTest: ITipoTest): Observable<void> {
      return this.http
        .post<void>(this.API_URL + this.CONTEXT + '/TipoTest', tipoTest)
        .pipe(catchError(this.manejoErrores));
    }
    eliminarTipoTest(id: number): Observable<void> {
      return this.http
        .delete<void>(this.API_URL + this.CONTEXT + '/TipoTest/' + id)
        .pipe(catchError(this.manejoErrores));
    }

    manejoErrores(error: any) {
      const msg = error.error?.message || error.statusText || 'Error de red';
      console.error(msg);
      return throwError(() => new Error(msg));
    }


}
