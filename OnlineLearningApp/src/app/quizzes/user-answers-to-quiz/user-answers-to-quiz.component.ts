import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Answer } from '../interfaces/answer.interface';
import { Question } from '../interfaces/question.interface';
import { AnswerService } from '../services/answer.service';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-user-answers-to-quiz',
  templateUrl: './user-answers-to-quiz.component.html',
  styleUrls: ['./user-answers-to-quiz.component.scss'],
})
export class UserAnswersToQuizComponent implements OnInit {
  rateCompletedQuizForm = this.fb.group({
    grade: [5, [Validators.required, Validators.min(1), Validators.max(5)]],
  });

  error: string | undefined;
  answers: Answer[] = [];

  constructor(
    private fb: FormBuilder,
    private answerService: AnswerService,
    private quizService: QuizService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.answerService
      .getUserAnswers(
        this.route.snapshot.paramMap.get('quizId')!,
        this.route.snapshot.paramMap.get('userId')!
      )
      .subscribe({
        next: (data) => {
          this.error = undefined;
          this.answers = data;
        },
        error: (err) => {
          this.error = err;
        },
      });
  }

  get grade() {
    return this.rateCompletedQuizForm.get('grade')!;
  }

  get gradeIsValid() {
    return !(this.grade.invalid && (this.grade.dirty || this.grade.touched));
  }

  rateCompletedQuiz() {
    this.quizService
      .rateCompletedQuiz(
        this.route.snapshot.paramMap.get('quizId')!,
        this.route.snapshot.paramMap.get('userId')!,
        this.rateCompletedQuizForm.value.grade
      )
      .subscribe({
        next: () => {
          this.error = undefined;
          this.location.back();
        },
        error: (err) => {
          this.error = err;
        },
      });
  }
}
