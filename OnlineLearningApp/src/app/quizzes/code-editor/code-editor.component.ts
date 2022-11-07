import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnInit,
  ViewChild,
} from '@angular/core';

import * as ace from 'ace-builds';
import 'ace-builds/src-noconflict/ext-language_tools';
import 'ace-builds/src-noconflict/mode-csharp';
import 'ace-builds/src-noconflict/mode-c_cpp';
import 'ace-builds/src-noconflict/mode-javascript';
import 'ace-builds/src-noconflict/mode-python';
import 'ace-builds/src-noconflict/theme-dracula';
import { CodeLanguage } from '../enums/code-language.enum';
import { AddQuestion } from '../interfaces/add-question.interface';

const THEME = 'ace/theme/dracula';

@Component({
  selector: 'app-code-editor',
  templateUrl: './code-editor.component.html',
  styleUrls: ['./code-editor.component.scss'],
})
export class CodeEditorComponent implements AfterViewInit, OnChanges {
  @Input() codeLanguage: string | undefined;
  @Input() code: string | null = '';
  @Input() isReadOnly: boolean = false;
  @ViewChild('codeEditor') codeEditorElmRef!: ElementRef;
  codeEditor!: ace.Ace.Editor;

  constructor() {}

  ngAfterViewInit() {
    ace.require('ace/ext/language_tools');
    const element = this.codeEditorElmRef.nativeElement;
    const editorOptions = this.getEditorOptions();

    this.codeEditor = ace.edit(element, editorOptions);
    this.codeEditor.setTheme(THEME);
    this.codeEditor.getSession().setMode('ace/mode/javascript');
    this.codeEditor.setShowFoldWidgets(true);
    this.codeEditor.setReadOnly(this.isReadOnly);
    if (this.code) this.codeEditor.setValue(this.code);
  }

  private getEditorOptions(): Partial<ace.Ace.EditorOptions> & {
    enableBasicAutocompletion?: boolean;
  } {
    const basicEditorOptions: Partial<ace.Ace.EditorOptions> = {
      highlightActiveLine: true,
      minLines: 10,
      maxLines: Infinity,
    };

    const extraEditorOptions = {
      enableBasicAutocompletion: true,
    };
    const margedOptions = Object.assign(basicEditorOptions, extraEditorOptions);
    return margedOptions;
  }

  ngOnChanges() {
    this.changeCodeLanguage();
    this.setValue(this.code);
  }

  setValue(code: string | null) {
    if (!this.codeEditor) return;
    this.codeEditor.setValue(code ? code : '');
  }

  changeCodeLanguage() {
    if (!this.codeEditor) return;
    this.codeEditor
      .getSession()
      .setMode(`ace/mode/${this.codeLanguage!.toLowerCase()}`);
  }

  getValue() {
    return this.codeEditor.getValue();
  }
}
