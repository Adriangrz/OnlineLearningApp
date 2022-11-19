import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { CodeEditorComponent } from '../code-editor/code-editor.component';
import { AnswerType } from '../enums/answer-type.enum';
import { CodeLanguage } from '../enums/code-language.enum';
import { AddAnswer } from '../interfaces/add-answer.interface';
import { Answer } from '../interfaces/answer.interface';
import { Question } from '../interfaces/question.interface';
import { AnswerService } from '../services/answer.service';
import { CodeService } from '../services/code.service';
import { QuestionService } from '../services/question.service';

@Component({
  selector: 'app-quiz-questions',
  templateUrl: './quiz-questions.component.html',
  styleUrls: ['./quiz-questions.component.scss'],
})
export class QuizQuestionsComponent implements OnInit {
  @ViewChild('userCode') userCode!: CodeEditorComponent;

  multipleChoiceAnswerForm = this.fb.group({
    multipleChoiceOptions: this.fb.array([]),
  });

  textAnswerForm = this.fb.group({
    text: ['', []],
  });

  question: Question | undefined;
  error: string | undefined;
  userAnswers: [Question, AddAnswer][] = [];
  currentQuestionNumber: number = 1;
  totalQuestionsCount: number = 1;
  isCodeExecuting: boolean = false;
  areAnswersBeingProcessed: boolean = false;
  codeResult: string = '';
  codeArgumentsFormControl = new FormControl('');

  get multipleChoiceOptions() {
    return this.multipleChoiceAnswerForm.controls[
      'multipleChoiceOptions'
    ] as FormArray;
  }

  constructor(
    private questionService: QuestionService,
    private codeService: CodeService,
    private answerService: AnswerService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.getQuestion(this.currentQuestionNumber);
  }

  getQuestion(num: number) {
    this.questionService
      .getQuestions(this.route.snapshot.paramMap.get('quizId')!, num, 1)
      .subscribe({
        next: (data) => {
          this.error = undefined;
          this.totalQuestionsCount = data.totalItemsCount;
          this.question = data.items[0];
          if (this.question!.answerType === 'MultipleChoiceAnswer')
            this.question.multipleChoiceOptions.forEach((element) => {
              this.multipleChoiceOptions.push(this.fb.control(false));
            });
        },
        error: (err) => {
          this.error = err;
        },
      });
  }

  executeCode() {
    console.log(this.codeArgumentsFormControl.value);
    this.isCodeExecuting = true;
    this.codeService
      .sendCode(
        this.userCode.getValue(),
        this.question!.codeLanguage,
        this.codeArgumentsFormControl.value
      )
      .subscribe({
        next: (response) => {
          this.getCodeResult(response.token);
        },
        error: (err) => {
          this.isCodeExecuting = false;
          this.error = err;
        },
      });
  }

  getCodeResult(token: string) {
    this.codeService.getCodeResult(token).subscribe({
      next: (response) => {
        if (response.status.id < 3) {
          this.getCodeResult(token);
        }
        this.isCodeExecuting = false;
        this.codeResult = response.stdout;
      },
      error: (err) => {
        this.isCodeExecuting = false;
        this.error = err;
      },
    });
  }

  answerAsMultipleChoice() {
    let selectOptions: string = '';
    this.question!.multipleChoiceOptions.forEach((element, i) => {
      if (this.multipleChoiceOptions.value[i] == true) {
        selectOptions += `${selectOptions != '' ? ',' : ''}${element}`;
      }
    });
    let answer: AddAnswer = {
      value: selectOptions,
      code: null,
      codeLanguage: null,
    };
    this.userAnswers.push([this.question!, answer]);
  }

  answerAsText() {
    let answer: AddAnswer = {
      value: this.textAnswerForm.get('text')?.value,
      code: null,
      codeLanguage: null,
    };
    this.userAnswers.push([this.question!, answer]);
  }

  answerAsCode() {
    let answer: AddAnswer = {
      value: this.codeResult,
      code: this.userCode.getValue(),
      codeLanguage: this.question!.codeLanguage,
    };
    this.userAnswers.push([this.question!!, answer]);
  }

  saveQuestion() {
    if (this.question!.answerType === 'MultipleChoiceAnswer')
      this.answerAsMultipleChoice();
    if (this.question!.answerType === 'Text') this.answerAsText();
    if (this.question!.answerType === 'Code') this.answerAsCode();
  }

  nextQuestion() {
    this.saveQuestion();
    if (!(this.currentQuestionNumber < this.totalQuestionsCount)) return;

    this.currentQuestionNumber += 1;
    this.getQuestion(this.currentQuestionNumber);
  }

  endQuiz() {
    this.areAnswersBeingProcessed = true;
    this.saveQuestion();

    forkJoin(
      Array.from(this.userAnswers, (element) =>
        this.answerService.addAnswer(element[0].id, element[1])
      )
    ).subscribe({
      next: (data) => {
        this.error = undefined;
        this.router.navigate([
          `/zespoły/${this.route.snapshot.paramMap.get('id')!}`,
        ]);
        this.areAnswersBeingProcessed = false;
      },
      error: (err) => {
        this.error = err;
        this.areAnswersBeingProcessed = false;
      },
    });
  }
}
