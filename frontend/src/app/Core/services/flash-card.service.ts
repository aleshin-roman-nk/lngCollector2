import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ObservedValueOf, catchError, first } from 'rxjs';
import { environment } from 'src/environment';
import { IFlashCardTitle } from '../Models/flash-card-title';
import { IUpdateStringPropertyDto } from '../Models/updatePropertyDto';
import { ICreateFlashCardDto, IFlashCard, IUpdateFlashCardDto } from '../Models/flash-card';
import { ICreateFlashCardAnswer, IFlashCardAnswer, UpdateCardAnswerDto } from '../Models/flash-card-answer';
import { IFlashCardCheck, IFlashCardCheckResult } from '../Models/flash-card-check';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class FlashCardService {

  constructor(
    private http: HttpClient,
    private errorService: ErrorHandlerService
    ) { }

  getCardsForPlay(nodeId: number, date: Date): Observable<IFlashCard[]> {
    //flashcard/ofnode/playing?nodeId=1&date=2024-01-01
    const url = `${environment.apiUrl}/flashcard/ofnode/playing`
    const params = new HttpParams()
      .set('nodeid', nodeId.toString())
      .set('date', date.toDateString());

    return this.http
    .get<IFlashCard[]>(url, { params })
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )

  }

  getSingleFlashCard(cardId: number): Observable<IFlashCard>{
    const url = `${environment.apiUrl}/flashcard/${cardId}`

    return this.http
    .get<IFlashCard>(url)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  createCardAnswer(newAnsw: ICreateFlashCardAnswer): Observable<IFlashCardAnswer>{
    const url = `${environment.apiUrl}/flashcard/answer`

    return this.http
    .post<IFlashCardAnswer>(url, newAnsw)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  createCard(newCard: ICreateFlashCardDto): Observable<IFlashCard>{
    const url = `${environment.apiUrl}/flashcard`

    return this.http
    .post<IFlashCard>(url, newCard)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  updateCard(crd: IUpdateFlashCardDto): Observable<IFlashCard>{
    const url = `${environment.apiUrl}/flashcard`

    return this.http
    .put<IFlashCard>(url, crd)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  updateCardAnswer(answ: UpdateCardAnswerDto): Observable<void>{
    const url = `${environment.apiUrl}/flashcard/answer`

    return this.http
    .put<void>(url, answ)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  deleteCardAnswer(id: number): Observable<void>{
    const url = `${environment.apiUrl}/flashcard/answer/${id}`

    return this.http
    .delete<void>(url)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  checkFlashCard(solution: IFlashCardCheck): Observable<IFlashCardCheckResult> {
    const url = `${environment.apiUrl}/flashcard/check`

    return this.http
    .post<IFlashCardCheckResult>(url, solution)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }
}
