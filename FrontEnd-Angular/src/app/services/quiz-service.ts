import { Injectable } from '@angular/core';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { IQuestion, Iquiz } from '../models/iquiz.ts';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private questions: IQuestion[] = [];

  constructor(private http: HttpClient) {}

 getquiz(id?: number): Observable<Iquiz> {
    return this.http.get<Iquiz>(`${environment.baseUrl}/Quiz/getById/${id}`).pipe(
      tap(q => this.questions = q?.questions ?? [])
    );
  }

  getAllQuizzes(): Observable<Iquiz[]> {
    return this.http.get<Iquiz[]>(`${environment.baseUrl}/Quiz/getall`);
  }

  getQuizByLessonId(lessonId?: number): Observable<Iquiz> {
    return this.http.get<Iquiz>(`${environment.baseUrl}/Quiz/bylesson/${lessonId}`).pipe(
      tap(q => this.questions = q?.questions ?? [])
    );
  }

  addQuiz(quizData: Iquiz): Observable<any> {
    return this.http.post(`${environment.baseUrl}/Quiz`, quizData);
  }

  updateQuiz(id: number, quizData: Iquiz): Observable<any> {
    return this.http.put(`${environment.baseUrl}/Quiz/update/${id}`, quizData);
  }

  deleteQuiz(id?: number): Observable<any> {
    return this.http.delete(`${environment.baseUrl}/delete?id=${id}`);
  }

  getQuestions(): IQuestion[] { return this.questions; }

  calculateScore(answers: Record<string, string>): number {
    let score = 0;
    this.questions?.forEach(q => {
      if (!q?.id) return;
      const correct = q.options?.find(o => o.isCorrect);
      if (answers[q.id.toString()] === correct?.id?.toString()) {
        score += q.mark ?? 1;
      }
    });
    return score;
  }
}