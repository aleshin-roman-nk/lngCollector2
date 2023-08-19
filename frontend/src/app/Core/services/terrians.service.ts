import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, delay, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environment';
import { ITerrain } from '../Models/terrain';
import { ApiResponseWithContent } from '../Models/response';

@Injectable({
  providedIn: 'root'
})
export class TerriansService {

  constructor(private http: HttpClient) { }

  //items: ITerrain[] = []
  //items$: BehaviorSubject<ITerrain[]> = new BehaviorSubject<ITerrain[]>([])

  private instance: number = 0;
  getNextInstanceNumber(): number{
    return this.instance++;
  }

  getAll(): Observable<ApiResponseWithContent<ITerrain[]>> {
    return this.http.get<ApiResponseWithContent<ITerrain[]>>(`${environment.apiUrl}/terrain`)
/*       .pipe(
        delay(50),
        tap(resp => {
          this.items$.next(resp.Data)
          console.log(resp)
        }),
        catchError(this.errorHandler.bind(this))
      ) */
  }

  getOne(id: number): Observable<ApiResponseWithContent<ITerrain>> {
    return this.http
      .get<ApiResponseWithContent<ITerrain>>(`${environment.apiUrl}/terrain/${id}`)
  }

  create(terr: ITerrain): Observable<ApiResponseWithContent<ITerrain>> {
    return this.http.post<ApiResponseWithContent<ITerrain>>(`${environment.apiUrl}/terrain`, terr)
/*       .pipe(
        tap(resp => {
          const a = [...this.items$.value] 
          a.push(resp)
        })
      ) */
  }

  delete(tid: number): Observable<Response> {
    return this.http.delete<Response>(`${environment.apiUrl}/terrain/${tid}`)
  }

  private errorHandler(error: HttpErrorResponse) {
    console.log(error.message)
    return throwError(() => error.message)
  }
}
