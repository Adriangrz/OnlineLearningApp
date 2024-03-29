import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QuizzesRoutingModule } from './quizzes-routing.module';
import { QuizzesDashboardComponent } from './quizzes-dashboard/quizzes-dashboard.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AddQuizComponent } from './add-quiz/add-quiz.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddQuizQuestionComponent } from './add-quiz-question/add-quiz-question.component';
import { EditQuizQuestionComponent } from './edit-quiz-question/edit-quiz-question.component';
import { CodeEditorComponent } from './code-editor/code-editor.component';
import { QuizDetailsComponent } from './quiz-details/quiz-details.component';
import { QuizComponent } from './quiz/quiz.component';
import { QuizQuestionsComponent } from './quiz-questions/quiz-questions.component';
import { UserAnswersToQuizComponent } from './user-answers-to-quiz/user-answers-to-quiz.component';
import { ListQuizUsersComponent } from './list-quiz-users/list-quiz-users.component';

@NgModule({
  declarations: [
    QuizzesDashboardComponent,
    AddQuizComponent,
    AddQuizQuestionComponent,
    EditQuizQuestionComponent,
    CodeEditorComponent,
    QuizDetailsComponent,
    QuizComponent,
    QuizQuestionsComponent,
    UserAnswersToQuizComponent,
    ListQuizUsersComponent,
  ],
  imports: [
    CommonModule,
    QuizzesRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
  ],
})
export class QuizzesModule {}
