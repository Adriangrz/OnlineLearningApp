import {
  AfterContentChecked,
  AfterViewChecked,
  AfterViewInit,
  Component,
  DoCheck,
  ElementRef,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { CodeEditorComponent } from '../code-editor/code-editor.component';
import { AnswerType } from '../enums/answer-type.enum';
import { CodeLanguage } from '../enums/code-language.enum';
import { AddQuestion } from '../interfaces/add-question.interface';
import { Question } from '../interfaces/question.interface';

@Component({
  selector: 'app-edit-quiz-question',
  templateUrl: './edit-quiz-question.component.html',
  styleUrls: ['./edit-quiz-question.component.scss'],
})
export class EditQuizQuestionComponent implements OnChanges {
  @ViewChild(CodeEditorComponent)
  private codeEditorComponent!: CodeEditorComponent;
  @Input() questionToEdit: AddQuestion | undefined;
  @Output() editedQuestion = new EventEmitter<Question>();

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

  get code() {
    return this.questionForm.get('code')!;
  }

  constructor(private fb: FormBuilder) {}

  ngOnChanges() {
    this.changeQuestionToEdit();
  }

  changeQuestionToEdit() {
    if (!this.questionToEdit) return;
    this.questionForm.patchValue({
      text: this.questionToEdit.text,
      code: this.questionToEdit.code,
      codeLanguage: this.questionToEdit.codeLanguage,
      answerType: Object.values(this.answerTypeEnum)[
        this.questionToEdit.answerType
      ],
    });
    this.questionToEdit.multipleChoiceOptions.forEach((element) => {
      this.multipleChoiceOptions.push(
        this.fb.control(element, Validators.required)
      );
    });
    this.isCodeAdd = this.questionToEdit.code ? true : false;
    this.questionCode = this.questionToEdit.code;
  }

  get multipleChoiceOptions() {
    return this.questionForm.controls['multipleChoiceOptions'] as FormArray;
  }

  editQuestion() {
    if (this.isCodeAdd && this.codeEditorComponent.getValue() !== '')
      this.questionForm.controls['code'].setValue(
        this.codeEditorComponent.getValue()
      );
    this.answerType.setValue(
      Object.values(this.answerTypeEnum).indexOf(this.answerType.value)
    );
    this.editedQuestion.emit(this.questionForm.value);
    this.isCodeAdd = false;
  }

  addAnswerOption() {
    this.multipleChoiceOptions.push(this.fb.control('', Validators.required));
  }

  deleteAnswerOption(answerOptionIndex: number) {
    this.multipleChoiceOptions.removeAt(answerOptionIndex);
  }

  deleteCode() {
    this.questionCode = '';
    this.isCodeAdd = false;
  }

  addCode() {
    this.isCodeAdd = true;
  }
}
