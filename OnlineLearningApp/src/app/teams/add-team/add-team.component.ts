import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Team } from '../interfaces/team.interface';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-add-team',
  templateUrl: './add-team.component.html',
  styleUrls: ['./add-team.component.scss'],
})
export class AddTeamComponent {
  @Output() addedTeam = new EventEmitter<Team>();
  @ViewChild('addTeamModal') addTeamModal!: ElementRef;

  teamForm = this.fb.group({
    name: ['', [Validators.required]],
    /*     image: [null, [Validators.required]], */
  });

  isBeingProcessed: boolean = false;
  error: string | undefined;

  constructor(
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private teamService: TeamService
  ) {}

  /*   get image() {
    return this.teamForm.get('image')!;
  } */

  get name() {
    return this.teamForm.get('name')!;
  }

  /*   get imageIsInvalid() {
    return this.image.invalid && (this.image.dirty || this.image.touched);
  } */

  get nameIsInvalid() {
    return this.name.invalid && (this.name.dirty || this.name.touched);
  }

  /* onTeamImageChange(event: any) {
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
 */
  addTeam() {
    this.isBeingProcessed = true;
    this.teamService.addTeam({ ...this.teamForm.value }).subscribe({
      next: (data) => {
        this.error = undefined;
        this.addedTeam.emit(data);
        this.addTeamModal.nativeElement.click();
        this.isBeingProcessed = false;
      },
      error: (err) => {
        this.error = err;
        this.isBeingProcessed = false;
      },
    });
  }
}
