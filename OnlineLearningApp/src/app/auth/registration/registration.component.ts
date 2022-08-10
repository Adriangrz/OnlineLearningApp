import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ResponseToRegistrationRequest } from '../interfaces/response-to-registration-request.interface';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent {
  registrationForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.pattern(/^\p{L}+$/u)]],
    lastName: ['', [Validators.required, Validators.pattern(/^\p{L}+$/u)]],
    email: ['', [Validators.required, Validators.email]],
    password: [
      '',
      [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.*[A-Za-z])/),
      ],
    ],
    siteRules: [false, Validators.requiredTrue],
  });
  responseToRegistrationRequest?: ResponseToRegistrationRequest;
  isBeingProcessed: boolean = false;

  get firstName() {
    return this.registrationForm.get('firstName')!;
  }

  get firstNameIsValid() {
    return !(
      this.firstName.invalid &&
      (this.firstName.dirty || this.firstName.touched)
    );
  }

  get lastName() {
    return this.registrationForm.get('lastName')!;
  }

  get lastNameIsValid() {
    return !(
      this.lastName.invalid &&
      (this.lastName.dirty || this.lastName.touched)
    );
  }
  get email() {
    return this.registrationForm.get('email')!;
  }

  get emailIsValid() {
    return !(this.email.invalid && (this.email.dirty || this.email.touched));
  }
  get password() {
    return this.registrationForm.get('password')!;
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
      .register({
        ...this.registrationForm.value,
      })
      .subscribe((data: ResponseToRegistrationRequest) => {
        this.responseToRegistrationRequest = data;
        if (this.responseToRegistrationRequest.isSuccess) {
          this.router.navigate(['/logowanie']);
          this.isBeingProcessed = false;
          return;
        }
        this.isBeingProcessed = false;
      });
    console.log(this.registrationForm.value);
  }
}
