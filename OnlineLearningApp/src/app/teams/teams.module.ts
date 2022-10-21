import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeamsRoutingModule } from './teams-routing.module';
import { TeamsDashboardComponent } from './teams-dashboard/teams-dashboard.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { AddTeamComponent } from './add-team/add-team.component';
import { ManageTeamComponent } from './manage-team/manage-team.component';
import { TeamMemberDetailsComponent } from './team-member-details/team-member-details.component';
import { TeamNameComponent } from './team-name/team-name.component';
import { TeamMembersComponent } from './team-members/team-members.component';
import { AuthModule } from '../auth/auth.module';

@NgModule({
  declarations: [
    TeamsDashboardComponent,
    AddTeamComponent,
    ManageTeamComponent,
    TeamMemberDetailsComponent,
    TeamNameComponent,
    TeamMembersComponent,
  ],
  imports: [
    CommonModule,
    TeamsRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
  ],
})
export class TeamsModule {}
