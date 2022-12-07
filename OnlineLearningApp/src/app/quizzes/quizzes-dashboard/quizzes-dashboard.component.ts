import { DOCUMENT } from '@angular/common';
import {
  AfterViewChecked,
  Component,
  ElementRef,
  Inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  faChevronLeft,
  faCircleInfo,
  faEllipsis,
  faPenToSquare,
  faUsers,
} from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Team } from 'src/app/teams/interfaces/team.interface';
import { TeamService } from 'src/app/teams/services/team.service';
import { Quiz } from '../interfaces/quiz.interface';
import { QuizService } from '../services/quiz.service';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-quizzes-dashboard',
  templateUrl: './quizzes-dashboard.component.html',
  styleUrls: ['./quizzes-dashboard.component.scss'],
})
export class QuizzesDashboardComponent implements OnInit, AfterViewChecked {
  team: Team | undefined;
  quizzes: Quiz[] = [];
  error: string | undefined;
  siteContentElement: HTMLElement | null = null;
  mainContentElement: HTMLElement | null = null;
  currentScrollHeight: number = 0;
  selectQuizId: string | undefined;
  teamId: string | undefined;

  faChevronLeft = faChevronLeft;
  faEllipsis = faEllipsis;
  faCircleInfo = faCircleInfo;
  faPenToSquare = faPenToSquare;
  faUsers = faUsers;

  constructor(
    private teamService: TeamService,
    private authService: AuthService,
    private quizService: QuizService,
    private route: ActivatedRoute,
    @Inject(DOCUMENT) private document: Document
  ) {}

  @ViewChild('scrollBottom') private scrollBottom!: ElementRef;

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    if (!this.scrollBottom) return;

    if (this.currentScrollHeight !== this.scrollBottom.nativeElement.scrollTop)
      return;

    this.scrollBottom.nativeElement.scrollTop =
      this.scrollBottom.nativeElement.scrollHeight;
    this.currentScrollHeight = this.scrollBottom.nativeElement.scrollTop;
  }

  ngOnInit(): void {
    this.teamId = this.route.snapshot.paramMap.get('id')!;
    this.scrollToBottom();
    this.addStyles();
    this.getTeamById();
    this.getQuizzes();
  }

  connectToSignalR() {
    let connection = new HubConnectionBuilder()
      .withUrl('https://localhost:7067/hubs/quizHub', {
        accessTokenFactory: async () => {
          let expirationTime = new Date(
            this.authService.getTokenExpirationTime()!
          );
          expirationTime.setMinutes(expirationTime.getMinutes() - 5);
          if (new Date().getTime() >= expirationTime.getTime()) {
            let token = await lastValueFrom(this.authService.refreshToken());
            return token.token;
          }
          return this.authService.getJwtToken()!;
        },
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    connection.on('addToQuiz', (data: Quiz) => {
      this.quizzes.push(data);
    });

    connection.start();
  }

  getQuizzes() {
    this.quizService.getQuizzes(this.teamId!).subscribe({
      next: (data) => {
        this.error = undefined;
        this.quizzes = data;
        this.connectToSignalR();
      },
      error: (err) => {
        this.error = err;
      },
    });
  }

  addStyles() {
    this.siteContentElement = this.document.querySelector('.site-content');
    this.siteContentElement?.classList.add('quiz-site-content');
    this.mainContentElement = this.document.querySelector('.main-content');
    this.mainContentElement?.classList.add('overflow-auto');
  }

  getTeamById() {
    this.teamService
      .getTeamById(this.route.snapshot.paramMap.get('id')!)
      .subscribe({
        next: (data) => {
          this.error = undefined;
          this.team = data;
        },
        error: (err) => {
          this.error = err;
        },
      });
  }

  quizDateGreaterThanNow(quizEndDate: Date): boolean {
    if (new Date(quizEndDate).getTime() > new Date().getTime()) return true;
    return false;
  }
}
