import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Output,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-team-name',
  templateUrl: './edit-team-name.component.html',
  styleUrls: ['./edit-team-name.component.scss'],
})
export class EditTeamNameComponent {
  @Output() stopEditingTeamName = new EventEmitter();

  teamNameForm = this.fb.group({
    name: ['', [Validators.required]],
  });

  constructor(private fb: FormBuilder, private cd: ChangeDetectorRef) {}

  get name() {
    return this.teamNameForm.get('name')!;
  }

  get nameIsInvalid() {
    return this.name.invalid && (this.name.dirty || this.name.touched);
  }

  editTeamName() {
    console.log(this.teamNameForm.value);
  }
}
