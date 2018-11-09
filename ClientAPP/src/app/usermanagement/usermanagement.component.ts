import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../_services/Authentication.service';
import { UserAuth } from './../_models/UserAuth';

@Component({
  selector: 'app-usermanagement',
  templateUrl: './usermanagement.component.html',
  styleUrls: ['./usermanagement.component.css']
})
export class UsermanagementComponent implements OnInit {
  currentUser: UserAuth;
  decodedUser: any;

  constructor(
    private router: Router,
    private authService: AuthenticationService) 
    {     
      
   
      if(!authService.isLoggedIn())
        this.router.navigateByUrl('/login');

       this.decodedUser = authService.decodedToken;
    
    }

  ngOnInit() {}

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  
}
