import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { QuizDetails } from '../interfaces/quiz-details.interface';
import { Quiz } from '../interfaces/quiz.interface';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-quiz-details',
  templateUrl: './quiz-details.component.html',
  styleUrls: ['./quiz-details.component.scss'],
})
export class QuizDetailsComponent implements OnChanges {
  @Input() quizId: string | undefined;
  @Input() teamId: string | undefined;

  quiz: QuizDetails | undefined;
  error: string | undefined;

  constructor(private quizService: QuizService) {}

  ngOnChanges() {
    if (!this.quizId || !this.teamId) return;
    this.quizService.getQuiz(this.quizId, this.teamId).subscribe({
      next: (data) => {
        console.log(data);
        this.quiz = data;
        this.error = undefined;
      },
      error: (err) => {
        this.error = err;
      },
    });
  }
}
