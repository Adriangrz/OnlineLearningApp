import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, delay, switchMap, tap, throwError } from 'rxjs';
import { CodeResult } from '../interfaces/code-result.interface';

@Injectable({
  providedIn: 'root',
})
export class CodeService {
  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    return throwError(() => 'Wykonanie kodu nie powiodło się');
  }

  sendCode(code: string, language: string, codeArguments: string) {
    let languageId = 50;
    switch (language) {
      case 'Javascript':
        languageId = 63;
        break;
      case 'C++':
        languageId = 54;
        break;
      case 'C#':
        languageId = 51;
        break;
      case 'Python':
        languageId = 70;
        break;
      default:
        break;
    }
    let codeData = {
      cpu_extra_time: null,
      cpu_time_limit: null,
      enable_network: null,
      enable_per_process_and_thread_memory_limit: null,
      enable_per_process_and_thread_time_limit: null,
      expected_output: null,
      language_id: languageId,
      max_file_size: null,
      max_processes_and_or_threads: null,
      memory_limit: null,
      number_of_runs: null,
      source_code: code,
      stack_limit: null,
      stdin: codeArguments,
      wall_time_limit: null,
    };
    return this.http
      .post<{ token: string }>(`/submissions`, codeData)
      .pipe(catchError(this.handleError));
  }

  getCodeResult(token: string) {
    return this.http
      .get<CodeResult>(`/submissions/${token}`)
      .pipe(delay(1000), catchError(this.handleError));
  }
}
