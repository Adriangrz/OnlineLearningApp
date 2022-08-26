import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {
  faBoxArchive,
  faCircleInfo,
  faEllipsis,
  faGear,
  faPen,
  faPlus,
  faTrash,
} from '@fortawesome/free-solid-svg-icons';

declare var bootstrap: any;

@Component({
  selector: 'app-teams-dashboard',
  templateUrl: './teams-dashboard.component.html',
  styleUrls: ['./teams-dashboard.component.scss'],
})
export class TeamsDashboardComponent {
  faEllipsis = faEllipsis;
  faGear = faGear;
  faBoxArchive = faBoxArchive;
  faPlus = faPlus;

  constructor() {}
}
