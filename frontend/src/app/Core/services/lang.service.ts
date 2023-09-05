import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environment';
import { ApiResponseWithContent } from '../Models/response';
import { Observable } from 'rxjs';
import { ILanguage } from '../Models/language';

@Injectable({
  providedIn: 'root'
})
export class LangService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<ApiResponseWithContent<ILanguage[]>>{
    const url = `${environment.apiUrl}/languages`;

    return this.http.get<ApiResponseWithContent<ILanguage[]>>(url)
  }
}
