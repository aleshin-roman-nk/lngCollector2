import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environment';
import { Observable, catchError } from 'rxjs';
import { ILanguage } from '../Models/language';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class LangService {

  constructor(
    private http: HttpClient,
    private errorService: ErrorHandlerService
    ) { }

  getAll(): Observable<ILanguage[]>{
    const url = `${environment.apiUrl}/languages`;

    return this.http
    .get<ILanguage[]>(url)
    .pipe(
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }
}
