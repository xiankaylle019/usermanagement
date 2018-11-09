import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from '../_services/Authentication.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  /**
   *
   */
  constructor(
    private authService: AuthenticationService, 
    private router: Router) {
 
  }
  canActivate(): boolean {
    
    console.log("pasok : " + this.authService.isLoggedIn());

    if(this.authService.isLoggedIn())
      return true;

      this.router.navigate(['/login']);

      return false;
  }
}
