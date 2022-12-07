import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/guards/auth.guard';
import { AddQuizComponent } from './add-quiz/add-quiz.component';
import { ListQuizUsersComponent } from './list-quiz-users/list-quiz-users.component';
import { QuizQuestionsComponent } from './quiz-questions/quiz-questions.component';
import { QuizComponent } from './quiz/quiz.component';
import { QuizzesDashboardComponent } from './quizzes-dashboard/quizzes-dashboard.component';
import { UserAnswersToQuizComponent } from './user-answers-to-quiz/user-answers-to-quiz.component';

const routes: Routes = [
  {
    path: 'zespoły/:id',
    component: QuizzesDashboardComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'zespoły/:id/utwórz-test',
    component: AddQuizComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'zespoły/:id/test/:quizId',
    component: QuizComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'zespoły/:id/test/:quizId/użytkownicy/:userId/odpowiedzi',
    component: UserAnswersToQuizComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'zespoły/:id/test/:quizId/użytkownicy',
    component: ListQuizUsersComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'zespoły/:id/test/:quizId/pytania',
    component: QuizQuestionsComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class QuizzesRoutingModule {}
