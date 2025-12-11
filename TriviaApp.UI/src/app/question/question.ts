import { Component, OnInit } from '@angular/core';
import { Spinner } from "../components/spinner/spinner";
import { QuestionService } from '../../services/questions/question.service';
import { GetQuestionResponseModel } from '../../services/questions/models/get-question-response-model';
import { CheckQuestionResponseModel } from '../../services/questions/models/check-question-response-model';
import { CheckQuestionRequestModel } from '../../services/questions/models/check-question-request-model';

@Component({
  selector: 'app-question',
  imports: [Spinner],
  templateUrl: './question.html',
  styleUrl: './question.css',
})
export class Question implements OnInit {

  loadingQuestionMessages = [
    "Thinking of a good question to ask someone like you...",
    "Just one more question before you win your big prize...",
    "You think you're so smart, don't you? Let's see...",
  ]
  loadingQuestionMessage = "";

  checkingQuestionMessages = 
  [
    "Hold on, I'm consulting with the trivia gods...",
    "Sounds plausible... Let me check with the jury...",
    "Seems like a trick question, but I'll verify anyway...",
    "You really want that price, huh? Let me double-check...",
  ]
  checkingQuestionMessage = "";

  question: GetQuestionResponseModel | null = null;
  checkingAnswer = false;
  requestionResult: CheckQuestionResponseModel | null = null;

  constructor(private questionService: QuestionService) { }

  ngOnInit(): void {
    this.resetLoadingQuestionMessage();
    this.setTimeoutGetQuestion();
  }

  resetLoadingQuestionMessage() {
    const randomIndex = Math.floor(Math.random() * this.loadingQuestionMessages.length);
    this.loadingQuestionMessage = this.loadingQuestionMessages[randomIndex];
  }

  resetCheckingQuestionMessage() {
    const randomIndex = Math.floor(Math.random() * this.checkingQuestionMessages.length);
    this.checkingQuestionMessage = this.checkingQuestionMessages[randomIndex];
  }

  setTimeoutGetQuestion() {
    setTimeout(() => {
      this.getQuestion();
    }, 1000);
  }

  getQuestion(): void {
    this.questionService.getQuestion().subscribe({
      next: (response) => {

        if (response.success === false) {
          this.setTimeoutGetQuestion();
          return;
        }

        this.question = response;
      },
      error: (error) => {
        this.setTimeoutGetQuestion();
      }
    })
  }


  Submit(answer: string) {
    this.resetCheckingQuestionMessage();
    this.checkingAnswer = true;
    this.setTimeoutCheckQuestion(answer);
  }

  setTimeoutCheckQuestion(answer: string) {
    setTimeout(() => {
      this.checkQuestion(answer);
    }, 1000);
  }

  checkQuestion(answer: string) {
    var requestModel: CheckQuestionRequestModel = {
      id: this.question!.id!,
      answer: answer
    }

    this.questionService.checkQuestion(requestModel).subscribe({
      next: (response) => {
        this.requestionResult = response
      },
      error: (error) => {

      }
    })
  }

  ResetGame() {
    this.resetLoadingQuestionMessage();
    this.setTimeoutGetQuestion();
    this.checkingAnswer = false;
    this.question = null;
    this.requestionResult = null;
  }
}
