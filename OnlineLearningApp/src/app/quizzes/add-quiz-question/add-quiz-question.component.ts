import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { CodeEditorComponent } from '../code-editor/code-editor.component';
import { AnswerType } from '../enums/answer-type.enum';
import { CodeLanguage } from '../enums/code-language.enum';
import { AddQuestion } from '../interfaces/add-question.interface';
import { Question } from '../interfaces/question.interface';

@Component({
  selector: 'app-add-quiz-question',
  templateUrl: './add-quiz-question.component.html',
  styleUrls: ['./add-quiz-question.component.scss'],
})
export class AddQuizQuestionComponent {
  @ViewChild(CodeEditorComponent)
  private codeEditorComponent!: CodeEditorComponent;
  @ViewChild('addQuestionModal') addQuestionModal!: ElementRef;
  @Output() addedQuestion = new EventEmitter<AddQuestion>();

  answerTypeEnum = AnswerType;
  codeLanguageEnum = CodeLanguage;
  isCodeAdd: boolean = false;
  questionCode: string | null = '';

  questionForm = this.fb.group({
    text: ['', Validators.required],
    code: [null],
    codeLanguage: [this.codeLanguageEnum.Javascript],
    multipleChoiceOptions: this.fb.array([]),
    answerType: [this.answerTypeEnum.Text, Validators.required],
  });

  get answerType() {
    return this.questionForm.get('answerType')!;
  }

  get codeLanguage() {
    return this.questionForm.get('codeLanguage')!;
  }

  constructor(private fb: FormBuilder) {}

  get multipleChoiceOptions() {
    return this.questionForm.controls['multipleChoiceOptions'] as FormArray;
  }

  resetForm() {
    this.questionForm.reset({
      text: '',
      code: null,
      codeLanguage: this.codeLanguageEnum.Javascript,
      answerType: this.answerTypeEnum.Text,
    });
    this.multipleChoiceOptions.clear();
  }

  addQuestion() {
    if (this.isCodeAdd)
      this.questionForm.controls['code'].setValue(
        this.codeEditorComponent.getValue()
      );
    this.answerType.setValue(
      Object.values(this.answerTypeEnum).indexOf(this.answerType.value)
    );
    this.addedQuestion.emit(this.questionForm.value);
    if (this.isCodeAdd) this.questionCode = '';
    this.isCodeAdd = false;
    this.resetForm();
  }

  addAnswerOption() {
    this.multipleChoiceOptions.push(this.fb.control('', Validators.required));
  }

  deleteAnswerOption(answerOptionIndex: number) {
    this.multipleChoiceOptions.removeAt(answerOptionIndex);
  }

  cancel() {
    if (this.isCodeAdd) this.questionCode = '';
    this.isCodeAdd = false;
    this.resetForm();
  }

  deleteCode() {
    this.questionCode = '';
    this.isCodeAdd = false;
  }

  addCode() {
    this.isCodeAdd = true;
  }
}
