import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-team-member',
  templateUrl: './add-team-member.component.html',
  styleUrls: ['./add-team-member.component.scss'],
})
export class AddTeamMemberComponent {
  @Output() stopAddingTeamMember = new EventEmitter();

  teamMemberForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
  });

  constructor(private fb: FormBuilder, private cd: ChangeDetectorRef) {}

  get email() {
    return this.teamMemberForm.get('email')!;
  }

  get emailIsInvalid() {
    return this.email.invalid && (this.email.dirty || this.email.touched);
  }

  addTeamMember() {
    console.log(this.teamMemberForm.value);
  }
}
