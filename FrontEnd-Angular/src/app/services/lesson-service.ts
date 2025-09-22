import { environment } from './../../environments/environment';
import { Ilesson } from './../models/ilesson';
import { ILessonCreate } from './../models/i-lesson-create';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class LessonService {
  constructor(private http: HttpClient) {}

  // Get All
  getAll(): Observable<Ilesson[]> {
    return this.http.get<Ilesson[]>(`${environment.baseUrl}/Lesson/GetAll`);
  }

  // Get By Id
  getById(id: number): Observable<Ilesson> {
    return this.http.get<Ilesson>(`${environment.baseUrl}/Lesson/GetById/${id}`);
  }

  // Add Lesson (with file upload)
  addLesson(formData: FormData): Observable<string> {
  return this.http.post(`${environment.baseUrl}/Lesson/Add`, formData, { responseType: 'text' });
}

// Update Lesson
updateLesson(id: number, formData: FormData): Observable<string> {
  return this.http.put(`${environment.baseUrl}/Lesson/Update/${id}`, formData, { responseType: 'text' });
}

// Delete Lesson
deleteLesson(id: number): Observable<string> {
  return this.http.delete(`${environment.baseUrl}/Lesson/Delete/${id}`, { responseType: 'text' });
}
  // Get Lessons by Unit Name
  getByUnitName(unitName: string): Observable<Ilesson[]> {
    return this.http.get<Ilesson[]>(`${environment.baseUrl}/Lesson/by-lesson-name/${unitName}`);
  }

  // Get Lessons by Unit Id
  getByUnitId(unitId: number): Observable<Ilesson[]> {
    return this.http.get<Ilesson[]>(`${environment.baseUrl}/Lesson/by-unit-id/${unitId}`);
  }
//download
 downloadFile(fileUrl: string) {
  window.open(
    `${environment.baseUrl}/Lesson/download/by-url?fileUrl=${encodeURIComponent(fileUrl)}`,
    '_blank'
  );
}
}
