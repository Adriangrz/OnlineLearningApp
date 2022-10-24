import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faBackward, faGraduationCap } from '@fortawesome/free-solid-svg-icons';
import { Quiz } from '../interfaces/quiz.interface';
import { QuizService } from '../services/quiz.service';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss'],
})
export class QuizComponent implements OnInit {
  quiz: Quiz | undefined;
  error: string | undefined;

  faGraduatingCap = faGraduationCap;
  faBackward = faBackward;

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private quizService: QuizService
  ) {}

  ngOnInit() {
    this.quizService
      .getQuiz(
        this.route.snapshot.paramMap.get('quizId')!,
        this.route.snapshot.paramMap.get('id')!
      )
      .subscribe({
        next: (data) => {
          this.quiz = data;
          this.error = undefined;
        },
        error: (err) => {
          this.error = err;
        },
      });
  }

  goBack() {
    this.location.back();
  }

  quizDateGreaterThanNow(quizEndDate: Date): boolean {
    if (new Date(quizEndDate).getTime() > new Date().getTime()) return true;
    return false;
  }
}
