import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IStudent } from '../models/istudent';
import { UserAuthentication } from './user-authentication';
import { IUser } from '../models/iuser';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private _httpClient: HttpClient,private _authUser: UserAuthentication){}

  // getAllSubject(): Observable<IISubject[]>{
  //   return this._httpClient.get<IISubject[]>(`${environment.baseUrl}/subject`);
  // }

  getStudentById(id: number): Observable<IUser> {
    let token: string | null = this._authUser.getToken();
    return this._httpClient.get<IUser>(`${environment.baseUrl}/student/id/${id}`, {
      headers: new HttpHeaders({
        'content-type': 'application/json',
        'authorization': `Bearer ${token}`
      })
    });
  }

  // getSubjectByName(name: string): Observable<IISubject> {
  //   return this._httpClient.get<IISubject>(`${environment.baseUrl}/subject/${name}`);
  // }



  // updateSubject(id: number, _subject: IISubject): Observable<string> {
  //   return this._httpClient.put<string>(`${environment.baseUrl}/subject/${id}`, JSON.stringify(_subject),{
  //     headers: new HttpHeaders({
  //       'content-type': "application/json"
  //     })
  //   });
  // }

  // deleteSubject(id: number): Observable<string> {
  //   return this._httpClient.delete<string>(`${environment.baseUrl}/subject/${id}`);
  // }

    addStudent(_student: IStudent): Observable<string> {

    return this._httpClient.post<string> (`${environment.baseUrl}/student/studentRegister`, _student,{
      headers: new HttpHeaders({
        'content-type': "application/json"
      })
    });
  }
}




