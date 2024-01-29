import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, delay, first, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environment';
import { ITerrain, ITerrainUpdateDto } from '../Models/terrain';
import { ErrorHandlerService } from 'src/app/Core/services/error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class TerriansService {

  constructor(
    private http: HttpClient,
    private errorService: ErrorHandlerService
    ) { }

  private instance: number = 0;
  getNextInstanceNumber(): number{
    return this.instance++;
  }

  getAll(): Observable<ITerrain[]> {
    return this.http
    .get<ITerrain[]>(`${environment.apiUrl}/terrain`)
    .pipe(
      delay(10),
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  getOne(id: number): Observable<ITerrain> {
    return this.http
      .get<ITerrain>(`${environment.apiUrl}/terrain/${id}`)
      .pipe(
        first(),
        catchError(error => this.errorService.httpErrorHandle(error))
      )
  }

  create(terr: ITerrain): Observable<ITerrain> {
    return this.http
    .post<ITerrain>(`${environment.apiUrl}/terrain`, terr)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  delete(tid: number): Observable<void> {
    return this.http
    .delete<void>(`${environment.apiUrl}/terrain/${tid}`)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  update(terr: ITerrainUpdateDto): Observable<void>{
    console.log("update(terr: ITerrainUpdateDto)")
    return this.http
    .put<void>(`${environment.apiUrl}/terrain`, terr)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }
}
