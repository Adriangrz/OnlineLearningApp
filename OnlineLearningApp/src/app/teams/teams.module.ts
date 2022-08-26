import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeamsRoutingModule } from './teams-routing.module';
import { TeamsDashboardComponent } from './teams-dashboard/teams-dashboard.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { AddTeamComponent } from './add-team/add-team.component';
import { ManageTeamComponent } from './manage-team/manage-team.component';
import { TeamMemberDetailsComponent } from './team-member-details/team-member-details.component';
import { EditTeamNameComponent } from './edit-team-name/edit-team-name.component';
import { AddTeamMemberComponent } from './add-team-member/add-team-member.component';

@NgModule({
  declarations: [
    TeamsDashboardComponent,
    AddTeamComponent,
    ManageTeamComponent,
    TeamMemberDetailsComponent,
    EditTeamNameComponent,
    AddTeamMemberComponent,
  ],
  imports: [
    CommonModule,
    TeamsRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
  ],
})
export class TeamsModule {}
