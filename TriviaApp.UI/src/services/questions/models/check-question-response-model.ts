export interface CheckQuestionResponseModel {
  success: boolean;
  wasAnswerCorrect?: boolean;
  correctAnswer?: string;
}