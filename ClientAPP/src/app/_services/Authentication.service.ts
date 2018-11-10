import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { JwtHelperService } from '@auth0/angular-jwt';

import { UserAuth } from '../_models/UserAuth';
import { AuthVM } from '../_models/AuthVM';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private baseUrl: string = 'http://localhost:5000/api/authservice';

  public decodedToken: any;

  public jwtHelper = new JwtHelperService(); // need to import manually

  constructor(private http: HttpClient) { 
 
  }
  getToken(){
     const token = localStorage.getItem('token');
     return token;
  }
  
  login(authvm: AuthVM) {

    return this.http.post<UserAuth>(this.baseUrl + "/login", authvm)
    .pipe(map(user => {
      
        if (user && user.token) {

            localStorage.setItem('token', user.token);

            this.decodedToken = this.jwtHelper.decodeToken(user.token);

            console.log("loggedIn");

            console.log(this.decodedToken);

        
        }

        return user;
    }));

  }

  logout() {
    localStorage.removeItem('token');    
    
  }
  isLoggedIn()
  {   
    const token = localStorage.getItem('token');

    return  !this.jwtHelper.isTokenExpired(token)

  }

}
  
