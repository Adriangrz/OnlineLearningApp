export interface Question {
  id: string;
  text: string;
  code: string;
  codeLanguage: string;
  multipleChoiceOptions: string[];
  answerType: string;
  hasImages: boolean;
  quizId: string;
}
