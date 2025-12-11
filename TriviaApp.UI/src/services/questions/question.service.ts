import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetQuestionResponseModel } from './models/get-question-response-model';
import { CheckQuestionRequestModel } from './models/check-question-request-model';
import { CheckQuestionResponseModel } from './models/check-question-response-model';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  
  private apiUrl = '/api/question';

  constructor(private http: HttpClient) { }

  getQuestion(): Observable<GetQuestionResponseModel> {
    return this.http.get<GetQuestionResponseModel>(`${this.apiUrl}`);
  }

  checkQuestion(requestModel: CheckQuestionRequestModel): Observable<CheckQuestionResponseModel> {
    return this.http.post<CheckQuestionResponseModel>(`${this.apiUrl}`, requestModel);
  }
}
