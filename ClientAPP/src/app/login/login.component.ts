import { Component, OnInit } from "@angular/core";
import { AuthVM } from "../_models/AuthVM";
import { AuthenticationService } from "../_services/Authentication.service";
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  submitted = false;
  model: AuthVM;   

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthenticationService
  ) {
    if(authService.isLoggedIn())
        this.router.navigateByUrl("/usermanagement/dashboard");
   }

  ngOnInit() {

    this.loginForm = this.formBuilder.group({
      username: [null, Validators.required],
      password: [null, Validators.required]
    });
  }

  get formControl() {
    return this.loginForm.controls;
  }

  private prepareSave(): AuthVM {
    return new AuthVM().deserialize(this.loginForm.value);
  }

  logIn() {
    
    this.model = this.prepareSave();
    this.submitted = true;
 
    if (this.loginForm.invalid) return;   
    
    console.log(this.model);

    this.authService.login(this.model).subscribe(
      data => {
        console.log("logged in successfully");             
      },
      error => {    
        console.log("failed to login");

      },() => {
          this.router.navigateByUrl("/usermanagement/dashboard");
      }
    );
  }
}
