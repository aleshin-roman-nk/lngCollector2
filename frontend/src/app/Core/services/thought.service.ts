import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IThought } from '../Models/thought';
import { environment } from 'src/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IThExpression } from '../Models/thexpression';

@Injectable({
  providedIn: 'root'
})
export class ThoughtService {

  constructor(private http: HttpClient) { }

  getThought(id: number): Observable<IThought>{
    const url = `${environment.apiUrl}/thought/${id}`;

    return this.http.get<IThought>(url)
  }

  createExpression(thoughtId: number, thexp: IThExpression): Observable<IThExpression>  {
    const url = `${environment.apiUrl}/expression?thoughtId=${thoughtId}`;

    return this.http.post<IThExpression>(url, thexp)
  }

  updateExpression(thexp: IThExpression): Observable<IThExpression> {
    const url = `${environment.apiUrl}/expression/${thexp.id}/text`;
/*     const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }; */

    return this.http.put<IThExpression>(url, { value: thexp.text })
  }

  deleteExpression(id: number): Observable<string> {
    const url = `${environment.apiUrl}/expression/${id}`;

    return this.http.delete<string>(url)
  }
}
