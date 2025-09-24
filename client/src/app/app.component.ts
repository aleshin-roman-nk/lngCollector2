import { Component } from '@angular/core';
import { AuthService } from './Core/services/auth.service';
import { IAuthorizedUser, IUser } from './Core/Models/user';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { ErrorHandlerService } from './Core/services/error-handler.service';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'appLng-UI';

  user: IUser = {Email: "", Password: "", Username: ""}
  authUser: IAuthorizedUser | null = null

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private errorService: ErrorHandlerService
    ) {

  }

  ngOnInit() {
    this.authUser = this.authService.currentUser

    if (!this.authUser) this.router.navigate(['']);

    this.authService.onUserLogged.subscribe(user => {
      this.authUser = user
      this.router.navigate(['home']);
    })

    this.authService.onUserLogout.subscribe(() => {
      this.authUser = null
      this.router.navigate(['']);
    })

    this.errorService.httpErrorCode$.subscribe((err) => {
      if(err === 401){
        this.authService.logout()
      }})
  }

  register(user: IUser) {

    if (!user.Email || !user.Password) {
      this.errorService.displayError("Email and password are required")
      return
    }

    this.authService.register(user)
  }

  login(user: IUser) {

    if (!user.Email || !user.Password) {
      this.errorService.displayError("Email and password are required")
      return
    }

    this.authService.login(user)
  }

  logout(){
    this.authService.logout()
  }
}
