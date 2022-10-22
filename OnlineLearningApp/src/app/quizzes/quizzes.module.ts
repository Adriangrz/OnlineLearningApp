import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QuizzesRoutingModule } from './quizzes-routing.module';
import { QuizzesDashboardComponent } from './quizzes-dashboard/quizzes-dashboard.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [QuizzesDashboardComponent],
  imports: [
    CommonModule,
    QuizzesRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
  ],
})
export class QuizzesModule {}
