import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { environment } from 'src/environment';
import { IAuthorizationResult, IAuthorizedUser, IUser } from '../Models/user';
import { BehaviorSubject, Observable, catchError, throwError } from 'rxjs';
import { UserLocalStoreService } from './user-local-store.service';
import { ErrorHandlerService } from 'src/app/Core/services/error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public onUserLogout: EventEmitter<void> = new EventEmitter<void>()
  public onUserLogged: EventEmitter<IAuthorizedUser> = new EventEmitter<IAuthorizedUser>()

  public currentUser: IAuthorizedUser | null = null

  constructor(
    private http: HttpClient,
    private authRWService: UserLocalStoreService,
    private errorService: ErrorHandlerService) {

    const userData = authRWService.Read()
    if (userData) {
      this.currentUser = userData.authUser
    }

   }

   register(user: IUser){
    const url = `${environment.apiUrl}/auth/register`

    this.http.post<IAuthorizationResult>(url, user)
    .pipe(
      catchError(error => this.errorService.httpErrorHandle(error))
    )
    .subscribe(token => {
      this.authRWService.Write(token)
      this.onUserLogged.emit(token.authUser)
    })
   }

   login(user: IUser){
    const url = `${environment.apiUrl}/auth/login`

    this.http.post<IAuthorizationResult>(url, user)
    .pipe(
      catchError(error => this.errorService.httpErrorHandle(error))
    )
    .subscribe(token => {
      this.authRWService.Write(token)
      this.onUserLogged.emit(token.authUser)
    })
   }

   logout(){
    this.authRWService.Forget()
     this.onUserLogout.emit()
   }
}
