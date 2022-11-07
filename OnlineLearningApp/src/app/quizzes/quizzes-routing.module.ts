import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddQuizComponent } from './add-quiz/add-quiz.component';
import { QuestionComponent } from './question/question.component';
import { QuizComponent } from './quiz/quiz.component';
import { QuizzesDashboardComponent } from './quizzes-dashboard/quizzes-dashboard.component';

const routes: Routes = [
  { path: 'zespoły/:id', component: QuizzesDashboardComponent },
  { path: 'zespoły/:id/utwórz-test', component: AddQuizComponent },
  { path: 'zespoły/:id/test/:quizId', component: QuizComponent },
  {
    path: 'zespoły/:id/test/:quizId/pytania',
    component: QuestionComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class QuizzesRoutingModule {}
