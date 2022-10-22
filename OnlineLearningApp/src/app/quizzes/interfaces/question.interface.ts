export interface Question {
  id: string;
  text: string;
  code: string;
  codeLanguage: string;
  multipleChoiceOptions: string[];
  answerType: number;
  hasImages: boolean;
  quizId: string;
}
