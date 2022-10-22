import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { AddQuiz } from '../interfaces/add-quiz.interface';
import { Quiz } from '../interfaces/quiz.interface';

@Injectable({
  providedIn: 'root',
})
export class QuizService {
  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0 || error.status >= 500 || !error.error) {
      return throwError(() => 'Coś poszło nie tak');
    }
    return throwError(() => error.error);
  }

  addQuiz(teamId: string, addQuiz: AddQuiz) {
    return this.http
      .post<Quiz>(`/api/Team/${teamId}/Quiz`, addQuiz)
      .pipe(catchError(this.handleError));
  }

  getQuizzes(teamId: string) {
    return this.http
      .get<Quiz[]>(`/api/Team/${teamId}/Quiz`)
      .pipe(catchError(this.handleError));
  }
}
