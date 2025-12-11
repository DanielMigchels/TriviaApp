export interface GetQuestionResponseModel {
  success: boolean;
  id?: string;
  type?: number;
  question?: string;
  answers?: string[];
  difficulty?: string;
  category?: string;
}