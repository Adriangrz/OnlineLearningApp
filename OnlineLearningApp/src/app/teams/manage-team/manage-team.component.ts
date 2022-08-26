import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {
  faCircleInfo,
  faEllipsis,
  faPen,
  faTrash,
  faPlus,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-manage-team',
  templateUrl: './manage-team.component.html',
  styleUrls: ['./manage-team.component.scss'],
})
export class ManageTeamComponent {
  isTeamNameBeingEdited: boolean = false;
  isUserBeingAdded: boolean = false;

  faCircleInfo = faCircleInfo;
  faTrash = faTrash;
  faEllipsis = faEllipsis;
  faPen = faPen;
  faPlus = faPlus;

  constructor() {}

  stopEditingTeamName() {
    this.isTeamNameBeingEdited = false;
  }

  stopAddingTeamMember() {
    this.isUserBeingAdded = false;
  }
}
