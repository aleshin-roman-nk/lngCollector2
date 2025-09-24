import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { ErrorHandlerService } from './error-handler.service';
import { Observable, catchError } from 'rxjs';
import { ICreateResearchTextDto, IResearchText, IUpdateResearchTextDto } from '../Models/research-text';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class ResearchTextService {

  //onCreate: EventEmitter<IResearchText> = new EventEmitter<IResearchText>()

  constructor(
    private http: HttpClient,
    private errorService: ErrorHandlerService
  ) { }

  create(dto: ICreateResearchTextDto): Observable<IResearchText> {
    const url = `${environment.apiUrl}/research-text`

    return this.http
      .post<IResearchText>(url, dto)
      .pipe(
        catchError(error => this.errorService.httpErrorHandle(error))
      )
  }

  update(dto: IUpdateResearchTextDto): Observable<void> {
    const url = `${environment.apiUrl}/research-text`

    return this.http
      .put<void>(url, dto)
      .pipe(
        catchError(error => this.errorService.httpErrorHandle(error))
      )
  }

  delete(id: number): Observable<void> {
    const url = `${environment.apiUrl}/research-text/${id}`

    return this.http
      .delete<void>(url)
      .pipe(
        catchError(error => this.errorService.httpErrorHandle(error))
      )
  }

}
