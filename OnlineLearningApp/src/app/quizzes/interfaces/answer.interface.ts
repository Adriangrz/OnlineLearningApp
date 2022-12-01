import { Question } from './question.interface';

export interface Answer {
  id: string;
  value: string;
  codeLanguage: string | null;
  code: string | null;
  questionId: string;
  question: Question;
}
