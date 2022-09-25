import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { faPen } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Team } from '../interfaces/team.interface';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-team-name',
  templateUrl: './team-name.component.html',
  styleUrls: ['./team-name.component.scss'],
})
export class TeamNameComponent implements OnInit, OnChanges {
  @Output() newTeamName = new EventEmitter<string>();
  @Input()
  teamName: string = '';
  @Input()
  teamId: string = '';
  @Input()
  adminEmail: string = '';

  error: string | undefined;
  loggedInUser: string | null = null;
  isTeamNameBeingEdited: boolean = false;
  isBeingProcessed: boolean = false;

  teamNameForm = this.fb.group({
    name: [this.teamName, [Validators.required]],
  });

  faPen = faPen;

  constructor(
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private teamService: TeamService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loggedInUser = this.authService.getLoggedInUser;
  }

  ngOnChanges() {
    this.teamNameForm.get('name')?.setValue(this.teamName);
  }

  get name() {
    return this.teamNameForm.get('name')!;
  }

  get nameIsInvalid() {
    return this.name.invalid && (this.name.dirty || this.name.touched);
  }

  editTeamName() {
    this.isBeingProcessed = true;
    this.teamService
      .updateTeamName(this.teamId, this.teamNameForm.value.name)
      .subscribe({
        next: (team: Team) => {
          this.newTeamName.emit(team.name);
          this.isTeamNameBeingEdited = false;
          this.isBeingProcessed = false;
        },
        error: (err) => {
          this.error = err;
          this.isBeingProcessed = false;
        },
      });
  }
}
