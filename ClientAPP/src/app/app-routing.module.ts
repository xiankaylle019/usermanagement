import { AppComponent } from './app.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';

import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [    
  { 
    path: '', redirectTo: '/login', pathMatch: 'full' 
  }
  ,
  { 
    path: 'login', component: LoginComponent    
  },
  {
    path: 'register', component: RegistrationComponent  
  },
  {
    path: 'usermanagement',    
    loadChildren: './usermanagement/usermanagement.module#UsermanagementModule',
    canActivate: [AuthGuard]        
  }
  // ,
  // {
  //   path: 'usermanagement',
  //   loadChildren: './usermanagement/usermanagement.module#UsermanagementModule',
  //   canLoad: [AuthGuard]
  // }

];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  declarations: []
})
export class AppRoutingModule { }
