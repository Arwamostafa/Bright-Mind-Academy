export interface IOption {
  id?: number;
  name: string;
  isCorrect: boolean;
}

export interface IQuestion {
  id?: number;
  content: string;
  mark: number;
  options: IOption[];
}

export interface Iquiz {
  id?: number;
  description: string;
  assignedBefore: boolean;
  totalMarks: number;
  lessonId?: number;
  questions: IQuestion[];
}
