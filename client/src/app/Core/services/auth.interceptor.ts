import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environment';
import { UserLocalStoreService } from './user-local-store.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authRWService: UserLocalStoreService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {

      const authData = this.authRWService.Read()

      if(authData){
        request = request.clone({
          setHeaders: { Authorization: `Bearer ${authData.token}` },
        })
      }

    return next.handle(request);
  }
}
