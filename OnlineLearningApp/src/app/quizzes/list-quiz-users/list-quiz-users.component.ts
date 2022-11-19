import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/teams/interfaces/user.interface';
import { QuizUser } from '../interfaces/quiz-user.interface';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-list-quiz-users',
  templateUrl: './list-quiz-users.component.html',
  styleUrls: ['./list-quiz-users.component.scss'],
})
export class ListQuizUsersComponent implements OnChanges {
  @Input()
  quizId: string | undefined;
  error: string | undefined;
  quizUsers: QuizUser[] = [];

  constructor(
    private quizService: QuizService,
    private route: ActivatedRoute
  ) {}

  ngOnChanges() {
    this.clear();
    this.getQuizMembers();
  }

  clear() {
    this.quizUsers = [];
  }

  getQuizMembers() {
    if (!this.quizId) return;
    this.quizService.getQuizMembers(this.quizId).subscribe({
      next: (data) => {
        this.error = undefined;
        this.quizUsers = data;
      },
      error: (err) => {
        this.error = err;
      },
    });
  }
}
