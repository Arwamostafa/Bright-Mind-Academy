import { environment } from './../../environments/environment';
import { IUnit } from './../models/iunit';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UnitService {

   constructor(private _httpClient: HttpClient){}

   getAllUnits(): Observable<IUnit[]> {
    return this._httpClient.get<IUnit[]>(`${environment.baseUrl}/Unit/GetAll`);
  }

  getUnitById(id: number): Observable<IUnit> {
    return this._httpClient.get<IUnit>(`${environment.baseUrl}/Unit/GetById/${id}`);
  }

  addUnit(unit:  Omit<IUnit, 'id'>): Observable<IUnit > {
    return this._httpClient.post<IUnit>(`${environment.baseUrl}/Unit/Create`, unit);
  }

 updateUnit(id: number, unit: IUnit): Observable<void> {
  const headers = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  return this._httpClient.put<void>(
    `${environment.baseUrl}/Unit/Update/${id}`,
    unit,
    { headers }
  );
}

  deleteUnit(id: number): Observable<void> {
    return this._httpClient.delete<void>(`${environment.baseUrl}/Unit/Delete/${id}`);
  }

  getUnitByLessonId(lessonId: number): Observable<IUnit> {
    return this._httpClient.get<IUnit>(`${environment.baseUrl}/Unit/by-lesson/${lessonId}`);

  }

  getUnitsBySubjectId(subjectId: number): Observable<IUnit[]> {
    return this._httpClient.get<IUnit[]>(`${environment.baseUrl}/Unit/BySubjectId/${subjectId}`);
  }


  getUnitsBySubjectName(subjectName: string): Observable<IUnit[]> {
    return this._httpClient.get<IUnit[]>(`${environment.baseUrl}/Unit/BySubjectName/${subjectName}`);
  }
}
