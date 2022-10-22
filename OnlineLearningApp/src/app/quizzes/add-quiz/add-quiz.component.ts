import { Location } from '@angular/common';
import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AnswerType } from '../enums/answer-type.enum';
import { AddQuestion } from '../interfaces/add-question.interface';
import { Question } from '../interfaces/question.interface';
import { Quiz } from '../interfaces/quiz.interface';
import { QuestionService } from '../services/question.service';
import { QuizService } from '../services/quiz.service';
import { dateGreaterThanNowValidator } from '../validators/date-greater-than-now.validator';

@Component({
  selector: 'app-add-quiz',
  templateUrl: './add-quiz.component.html',
  styleUrls: ['./add-quiz.component.scss'],
})
export class AddQuizComponent {
  quizForm = this.fb.group({
    name: ['', [Validators.required]],
    endDate: ['', [Validators.required, dateGreaterThanNowValidator()]],
  });

  error: string | undefined;
  id: number = 0;
  answerType = AnswerType;
  questions: AddQuestion[] = [];
  questionToEdit: AddQuestion | undefined;
  selectedQuestionIdToEdit: number | undefined;
  isBeingProcessed: boolean = false;

  constructor(
    private fb: FormBuilder,
    private location: Location,
    private questionService: QuestionService,
    private quizService: QuizService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  get name() {
    return this.quizForm.get('name')!;
  }

  get nameIsInvalid() {
    return this.name.invalid && (this.name.dirty || this.name.touched);
  }

  get endDate() {
    return this.quizForm.get('endDate')!;
  }

  get endDateIsInvalid() {
    return this.endDate.invalid && (this.endDate.dirty || this.endDate.touched);
  }

  goBack() {
    this.location.back();
  }

  addedQuestion(question: AddQuestion) {
    this.questions.push(question);
  }

  editedQuestion(question: Question) {
    this.questions[this.selectedQuestionIdToEdit!] = question;
  }

  editQuestion(question: AddQuestion, id: number) {
    this.questionToEdit = question;
    this.selectedQuestionIdToEdit = id;
  }

  deleteQuestion(questionId: number) {
    this.questions.splice(questionId, 1);
  }

  addQuestions(quiz: Quiz) {
    this.questionService.addQuestion(quiz.id, this.questions).subscribe({
      next: (data) => {
        this.error = undefined;
        this.location.back();
        this.isBeingProcessed = false;
      },
      error: (err) => {
        this.error = err;
        this.isBeingProcessed = false;
      },
    });
  }

  onSubmit() {
    this.isBeingProcessed = true;
    this.quizService
      .addQuiz(this.route.snapshot.paramMap.get('id')!, {
        ...this.quizForm.value,
      })
      .subscribe({
        next: (data) => {
          this.error = undefined;
          this.addQuestions(data);
        },
        error: (err) => {
          this.error = err;
          this.isBeingProcessed = false;
        },
      });
  }
}
