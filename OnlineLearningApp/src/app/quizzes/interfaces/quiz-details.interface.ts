export interface QuizDetails {
  id: string;
  name: string;
  createdDate: Date;
  endDate: Date;
  teamId: string;
  isDone: boolean | null;
}
