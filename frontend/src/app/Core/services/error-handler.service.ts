import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  error$ = new Subject<string>()
  httpErrorCode$ = new Subject<number>()

  httpErrorHandle(error: HttpErrorResponse): Observable<never> {

    let errorMessage = 'An unknown error occurred!';

    if(!error.error){
      errorMessage = error.statusText
    }
    else if (error.error instanceof ErrorEvent) {
        // Client-side error
        errorMessage = `Error: ${error.error.message}`;
    } else {
        // Server-side error
        if (typeof error.error === 'string') {
            // If the server sends a plain string as an error message
            errorMessage = error.error;
        } else {
            // If the server sends a more complex object, adjust this accordingly
            errorMessage = error.error.message || 'Something went wrong on the server';
        }
    }

    this.error$.next(errorMessage)

    if(error.status) this.httpErrorCode$.next(error.status)

    return throwError(() => error.message)
  }

  displayError(message: string){
    this.error$.next(message)
  }

  clear() {
    this.error$.next('')
  }
}
