import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CodeEditorComponent } from '../code-editor/code-editor.component';
import { AnswerType } from '../enums/answer-type.enum';
import { CodeLanguage } from '../enums/code-language.enum';
import { Answer } from '../interfaces/answer.interface';
import { Question } from '../interfaces/question.interface';
import { CodeService } from '../services/code.service';
import { QuestionService } from '../services/question.service';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss'],
})
export class QuestionComponent implements OnInit {
  @ViewChild('userCode') userCode!: CodeEditorComponent;

  multipleChoiceAnswerForm = this.fb.group({
    multipleChoiceOptions: this.fb.array([]),
  });

  textAnswerForm = this.fb.group({
    text: ['', []],
  });

  question: Question | undefined;
  error: string | undefined;
  answers: Answer[] = [];
  currentQuestionNumber: number = 1;
  totalQuestionsCount: number = 1;
  isCodeExecuting: boolean = false;
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
    private route: ActivatedRoute,
    private fb: FormBuilder
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
    let answer: Answer = {
      value: selectOptions,
      questionId: this.question!.id,
      code: null,
      codeLanguage: null,
    };
    this.answers.push(answer);
  }

  answerAsText() {
    let answer: Answer = {
      value: this.textAnswerForm.get('text')?.value,
      questionId: this.question!.id,
      code: null,
      codeLanguage: null,
    };
    this.answers.push(answer);
  }

  answerAsCode() {}

  saveQuestion() {
    if (this.question!.answerType === 'MultipleChoiceAnswer')
      this.answerAsMultipleChoice();
    if (this.question!.answerType === 'Text') this.answerAsText();
  }

  nextQuestion() {
    this.saveQuestion();
    if (!(this.currentQuestionNumber < this.totalQuestionsCount)) return;

    this.currentQuestionNumber += 1;
    this.getQuestion(this.currentQuestionNumber);
  }

  endQuiz() {
    this.saveQuestion();
    console.log(this.answers);
  }
}
