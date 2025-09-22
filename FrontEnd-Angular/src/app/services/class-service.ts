import { Injectable } from '@angular/core';
import { IClass } from '../models/iclass.ts';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.js';

@Injectable({
  providedIn: 'root'
})
export class ClassService {

  constructor(private _httpClient: HttpClient){}

  getAllClasses(): Observable<IClass[]>{
    return this._httpClient.get<IClass[]>(`${environment.baseUrl}/class`);
  }

  getClassById(id: number): Observable<IClass> {
    return this._httpClient.get<IClass>(`${environment.baseUrl}/class/${id}`);
  }

  getClassByName(name: string): Observable<IClass> {
    return this._httpClient.get<IClass>(`${environment.baseUrl}/class/${name}`);
  }

  addClass(_class: IClass): Observable<IClass> {

    return this._httpClient.post<IClass>(`${environment.baseUrl}/class`, JSON.stringify(_class),{
      headers: new HttpHeaders({
        'content-type': "application/json"
      })
    });
  }

  updateClass(id: number, _class: IClass): Observable<string> {
    return this._httpClient.put<string>(`${environment.baseUrl}/class/${id}`, JSON.stringify(_class),{
      headers: new HttpHeaders({
        'content-type': "application/json"
      })
    });
  }

  deleteClass(id: number): Observable<string> {
    return this._httpClient.delete<string>(`${environment.baseUrl}/class/${id}`);
  }





}
