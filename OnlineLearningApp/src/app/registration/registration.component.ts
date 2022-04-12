import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent {
  registrationForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
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

  constructor(private fb: FormBuilder) {}

  onSubmit() {
    console.log(this.registrationForm.value);
  }
}
