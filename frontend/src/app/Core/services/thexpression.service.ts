import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IThExpression } from '../Models/thexpression';
import { Observable } from 'rxjs';
import { ApiResponse, ApiResponseWithContent } from '../Models/response';
import { environment } from 'src/environment';
import { IUpdateStringPropertyDto } from '../Models/updatePropertyDto';

@Injectable({
  providedIn: 'root'
})
export class ThexpressionService {

  constructor(private http: HttpClient) { }

  createExpression(thoughtId: number, thexp: IThExpression): Observable<ApiResponseWithContent<IThExpression>>  {
    const url = `${environment.apiUrl}/expression?thoughtId=${thoughtId}`;

    return this.http.post<ApiResponseWithContent<IThExpression>>(url, thexp)
  }

  updateStringProperty(id: number, propName: string, newValue: string): Observable<ApiResponseWithContent<IThExpression>> {
    const url = `${environment.apiUrl}/expression/`;

    const o: IUpdateStringPropertyDto = {
      id: id,
      name: propName,
      value: newValue
    }

    return this.http.patch<ApiResponseWithContent<IThExpression>>(url, o)
  }

  deleteExpression(id: number): Observable<ApiResponse> {
    const url = `${environment.apiUrl}/expression/${id}`;

    return this.http.delete<ApiResponse>(url)
  }
}
