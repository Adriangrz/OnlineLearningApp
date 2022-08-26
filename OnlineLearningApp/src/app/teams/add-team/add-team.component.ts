import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-team',
  templateUrl: './add-team.component.html',
  styleUrls: ['./add-team.component.scss'],
})
export class AddTeamComponent {
  teamForm = this.fb.group({
    name: ['', [Validators.required]],
    image: [null, [Validators.required]],
  });

  constructor(private fb: FormBuilder, private cd: ChangeDetectorRef) {}

  get image() {
    return this.teamForm.get('image')!;
  }

  get name() {
    return this.teamForm.get('name')!;
  }

  get imageIsInvalid() {
    return this.image.invalid && (this.image.dirty || this.image.touched);
  }

  get nameIsInvalid() {
    return this.name.invalid && (this.name.dirty || this.name.touched);
  }

  onTeamImageChange(event: any) {
    if (!(event.target.files && event.target.files.length)) {
      this.image.setErrors({
        required: true,
      });
      this.image.markAsTouched;
      return;
    }

    const [file] = event.target.files;

    if (!file.type.startsWith('image')) {
      this.image.setErrors({
        image: true,
      });
      this.cd.markForCheck();
      return;
    }

    if (file.size > 2097152) {
      this.image.setErrors({
        tooBig: true,
      });
      return;
    }

    this.teamForm.patchValue({
      image: file,
    });
    this.cd.markForCheck();
  }

  addTeam() {
    console.log(this.teamForm.value);
  }
}
