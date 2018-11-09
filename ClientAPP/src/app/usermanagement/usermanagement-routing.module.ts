import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProfileComponent } from './profile/profile.component';
import { UsersComponent } from './users/users.component';
import { UsermanagementComponent } from './usermanagement.component';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [

  {
    path: '',
    redirectTo: "dashboard",
    pathMatch: "full"
  },
  { 
    path: 'dashboard',
    component: UsermanagementComponent,
    canActivate: [AuthGuard],
    children: [{ path: "", component: DashboardComponent }]
  },
  { 
    path: 'users',
    component: UsermanagementComponent,
    canActivate: [AuthGuard],
    children: [{ path: "", component: UsersComponent }]
  },
  { 
    path: 'profile',
    component: UsermanagementComponent,
    canActivate: [AuthGuard],
    children: [{ path: "", component: ProfileComponent }]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsermanagementRoutingModule { }
