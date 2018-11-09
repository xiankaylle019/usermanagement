import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthenticationService } from './_services/Authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  
  title = 'ClientAPP';

  private jwtHelper = new JwtHelperService(); // need to import manually

  constructor(private authService: AuthenticationService) { 
    
  }

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if(token){
      console.log(token);
        this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }

}
