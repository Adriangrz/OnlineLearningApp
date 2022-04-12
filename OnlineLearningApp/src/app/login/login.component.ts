import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
  });

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

  constructor(private fb: FormBuilder) {}

  onSubmit() {
    console.log(this.loginForm.value);
  }
}
