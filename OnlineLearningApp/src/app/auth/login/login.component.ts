import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequestDataReturned } from '../interfaces/login-request-data-returned.interface';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]],
  });
  loginRequestDataReturned?: LoginRequestDataReturned;
  isBeingProcessed: boolean = false;

  get email() {
    return this.loginForm.get('email')!;
  }

  get emailIsValid() {
    return !(this.email.invalid && (this.email.dirty || this.email.touched));
  }
  get password() {
    return this.loginForm.get('password')!;
  }

  get passwordIsValid() {
    return !(
      this.password.invalid &&
      (this.password.dirty || this.password.touched)
    );
  }

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit() {
    this.isBeingProcessed = true;
    this.authService
      .login(this.loginForm.value.email, this.loginForm.value.password)
      .subscribe((data: LoginRequestDataReturned) => {
        this.loginRequestDataReturned = data;
        if (this.loginRequestDataReturned.isSuccess) {
          this.router.navigate(['/']);
          this.isBeingProcessed = false;
          return;
        }
        this.isBeingProcessed = false;
      });
  }
}
