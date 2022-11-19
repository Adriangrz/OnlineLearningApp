import { User } from 'src/app/teams/interfaces/user.interface';

export interface QuizUser {
  user: User;
  quizId: string;
  isDone: boolean;
}
