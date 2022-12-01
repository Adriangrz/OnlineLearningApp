export interface QuizDetails {
  id: string;
  name: string;
  createdDate: Date;
  endDate: Date;
  teamId: string;
  isUserAssigned: boolean;
  isDone: boolean | null;
  grade: number | null;
}
