import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { AddQuestion } from '../interfaces/add-question.interface';
import { PagedResult } from '../interfaces/paged-result.interface';
import { Question } from '../interfaces/question.interface';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0 || error.status >= 500 || !error.error) {
      return throwError(() => 'Coś poszło nie tak');
    }
    return throwError(() => error.error);
  }

  addQuestion(quizId: string, addQuestions: AddQuestion[]) {
    return this.http
      .post<Question>(`/api/Quiz/${quizId}/Question`, addQuestions)
      .pipe(catchError(this.handleError));
  }

  getQuestions(quizId: string, pageNumber: number, pageSize: number) {
    return this.http
      .get<PagedResult<Question>>(`/api/Quiz/${quizId}/Question`, {
        params: { PageNumber: pageNumber, PageSize: pageSize },
      })
      .pipe(catchError(this.handleError));
  }
}
