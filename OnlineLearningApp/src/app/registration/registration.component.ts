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
  constructor(private fb: FormBuilder) {}

  onSubmit() {
    console.log(this.registrationForm.value);
  }
}
