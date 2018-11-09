import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { UsermanagementRoutingModule } from './usermanagement-routing.module';
import { UsermanagementComponent } from './usermanagement.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UsersComponent } from './users/users.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  imports: [
    CommonModule,
    UsermanagementRoutingModule  
  ],
  declarations: [
    UsermanagementComponent, 
    DashboardComponent, 
    UsersComponent, 
    ProfileComponent]
})
export class UsermanagementModule { }
