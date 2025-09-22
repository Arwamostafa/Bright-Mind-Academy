import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IISubject } from '../models/isubject';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  constructor(private _httpClient: HttpClient){}

  getAllSubject(): Observable<IISubject[]>{
    return this._httpClient.get<IISubject[]>(`${environment.baseUrl}/subject`);
  }

  getSubjectById(id: number): Observable<IISubject> {
    return this._httpClient.get<IISubject>(`${environment.baseUrl}/Subject/id/${id}`);
  }

  getSubjectByName(name: string): Observable<IISubject> {
    return this._httpClient.get<IISubject>(`${environment.baseUrl}/subject/${name}`);
  }

  addSubject(_subject: IISubject): Observable<IISubject> {
    console.log("service");


    return this._httpClient.post<IISubject>(`${environment.baseUrl}/subject`, _subject,{
      headers: new HttpHeaders({
        'content-type': "application/json"
      })
    });
  }

  updateSubject(id: number, _subject: IISubject): Observable<string> {
    return this._httpClient.put<string>(`${environment.baseUrl}/subject/${id}`, JSON.stringify(_subject),{
      headers: new HttpHeaders({
        'content-type': "application/json"
      })
    });
  }

  deleteSubject(id: number): Observable<string> {
    return this._httpClient.delete<string>(`${environment.baseUrl}/subject/${id}`);
  }

  // }
  // addSubject(subject:IISubject):Observable<IISubject>
  // {
  //   return this.httpclient.post<IISubject>(`${"environment.baseURL"}/Subject`,subject)

  // }
  //  getSubjects():Observable<IISubject[]> {
  //     return this.httpclient.get<IISubject[]>(`${"environment.baseURL"}/Instructor`);
  //   }
  //   getSubjectById(id: number): Observable<IISubject> {
  //     return this.httpclient.get<IISubject>(`${"environment.baseURL"}/Instructor/${id}`);
  //   }
  //   updateSubject(id: number, updatedSubject: IISubject): Observable<IISubject> {
  //     return this.httpclient.put<IISubject>(`${"environment.baseURL"}/Instructor/${id}`, updatedSubject);
  //   }
  //   deleteSubject(id: number): Observable<IISubject> {
  //     return this.httpclient.delete<IISubject>(`${"environment.baseURL"}/Instructor/${id}`);
  //   }
}
