import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { AddAnswer } from '../interfaces/add-answer.interface';
import { Answer } from '../interfaces/answer.interface';

@Injectable({
  providedIn: 'root',
})
export class AnswerService {
  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0 || error.status >= 500 || !error.error) {
      return throwError(() => 'Coś poszło nie tak');
    }
    return throwError(() => error.error);
  }

  addAnswer(questionId: string, addAnswer: AddAnswer) {
    return this.http
      .post<Answer>(`/api/Question/${questionId}/Answer`, addAnswer)
      .pipe(catchError(this.handleError));
  }
}
