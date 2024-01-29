import { Injectable } from '@angular/core';
import { IAuthorizationResult } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class UserLocalStoreService {
  constructor()
  {
  }

  public Read(): IAuthorizationResult | null {
    const authData = localStorage.getItem('authData');
    if (authData)
      return JSON.parse(authData)
    else
      return null
  }

  public Write(authDt: IAuthorizationResult) {
    localStorage.setItem('authData', JSON.stringify(authDt));
  }

  public Forget(){
    localStorage.removeItem('authData')
  }
}
