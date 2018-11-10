import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';


import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services/Authentication.service';

@Injectable({
  providedIn: 'root'
})
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthenticationService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(this.authService.isLoggedIn())
    {
      const token = this.authService.getToken();
      req = req.clone({
        setHeaders: { 
            Authorization: 'Bearer ${token}'
        }
    });
    }
    return next.handle(req);
  }

  
}
