import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, delay, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environment';
import { ITerrain } from '../Models/terrain';
import { ResponseExt } from '../Models/response';

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

  getAll(): Observable<ResponseExt<ITerrain[]>> {
    return this.http.get<ResponseExt<ITerrain[]>>(`${environment.apiUrl}/terrain`)
/*       .pipe(
        delay(50),
        tap(resp => {
          this.items$.next(resp.Data)
          console.log(resp)
        }),
        catchError(this.errorHandler.bind(this))
      ) */
  }

  getOne(id: number): Observable<ITerrain> {
    return this.http
      //.get<ITerrain>(`${environment.apiUrl}/terrain`, {
      //  params: new HttpParams().append("id", id)
      //})

      .get<ITerrain>(`${environment.apiUrl}/terrain/${id}`)
  }

  create(terr: ITerrain): Observable<ITerrain> {
    return this.http.post<ITerrain>(`${environment.apiUrl}/terrain`, terr)
/*       .pipe(
        tap(resp => {
          const a = [...this.items$.value] 
          a.push(resp)
        })
      ) */
  }

  delete(tid: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/terrain/${tid}`)
  }

  private errorHandler(error: HttpErrorResponse) {
    console.log(error.message)
    return throwError(() => error.message)
  }
}
