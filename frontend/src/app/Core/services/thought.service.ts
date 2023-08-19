import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IThought } from '../Models/thought';
import { environment } from 'src/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IThExpression } from '../Models/thexpression';
import { ApiResponse, ApiResponseWithContent as ApiResponseWithContent } from '../Models/response';
import { IUpdateStringPropertyDto } from '../Models/updatePropertyDto';

@Injectable({
  providedIn: 'root'
})
export class ThoughtService {

  constructor(private http: HttpClient) { }

  getThought(id: number): Observable<ApiResponseWithContent<IThought>>{
    const url = `${environment.apiUrl}/thought/${id}`;

    return this.http.get<ApiResponseWithContent<IThought>>(url)
  }

  createExpression(thoughtId: number, thexp: IThExpression): Observable<ApiResponseWithContent<IThExpression>>  {
    const url = `${environment.apiUrl}/expression?thoughtId=${thoughtId}`;

    return this.http.post<ApiResponseWithContent<IThExpression>>(url, thexp)
  }

  updateExpression(thexp: IThExpression): Observable<ApiResponseWithContent<IThExpression>> {
    const url = `${environment.apiUrl}/expression/`;
/*     const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }; */

    const o: IUpdateStringPropertyDto = {
      id: thexp.id,
      name: "text",
      value: thexp.text
    }

    return this.http.patch<ApiResponseWithContent<IThExpression>>(url, o)
  }

  updateThoughtStrginProperty(id: number, propName: string, newValue: string): Observable<ApiResponse>{
    const url = `${environment.apiUrl}/thought/`;

    const o: IUpdateStringPropertyDto = {
      id: id,
      name: propName,
      value: newValue
    }

    return this.http.patch<ApiResponse>(url, o)
  }

  deleteExpression(id: number): Observable<ApiResponse> {
    const url = `${environment.apiUrl}/expression/${id}`;

    return this.http.delete<ApiResponse>(url)
  }
}
