import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IInstructor } from '../models/iinstructor';
import { map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IUser } from '../models/iuser';
import { UserAuthentication } from './user-authentication';

@Injectable({
  providedIn: 'root'
})
export class InstructorService {
    constructor(private _httpClient: HttpClient, private _authUser: UserAuthentication){}

private toFormData(instructor: IInstructor): FormData {
  const formData = new FormData();

  formData.append("FirstName", instructor.firstName);
  formData.append("LastName", instructor.lastName);
  formData.append("Email", instructor.email);
  formData.append("PhoneNumber", instructor.phoneNumber);
  formData.append("Gender", instructor.gender);
  formData.append("Address", instructor.address);


  if (instructor.password) {
    formData.append("Password", instructor.password);
  }


  if (instructor.Image) {
    formData.append("Image", instructor.Image, instructor.Image.name);
  }

  return formData;
  }

  getAllInstructors(): Observable<IInstructor[]>{
    return this._httpClient.get<any[]>(`${environment.baseUrl}/instructor`).pipe(
        map(response => {
          console.log('Raw API Response:', response);

          return response.map(item => ({
            id: item.id || item.Id,
            firstName: item.firstName || item.FirstName,
            lastName: item.lastName || item.LastName,
            email: item.email || item.Email,
            phoneNumber: item.phoneNumber || item.PhoneNumber,
            address: item.address || item.Address,
            gender: item.gender || item.Gender,
            ImageURL: item.imageURL || item.ImageURL  // تعامل مع جميع الاحتمالات
          } as IInstructor));
        })
      );
  }

  // getSubjectById(id: number): Observable<IISubject> {
  //   return this._httpClient.get<IISubject>(${environment.baseUrl}/subject/${id});
  // }

  // getSubjectByName(name: string): Observable<IISubject> {
  //   return this._httpClient.get<IISubject>(${environment.baseUrl}/subject/${name});
  // }

  addInstructor(_instructor: IInstructor): Observable<string> {

    const formData = this.toFormData(_instructor);
    return this._httpClient.post(`${environment.baseUrl}/instructor/addingInstructor`, formData , {
        responseType: 'text'
      });
  }

  updateInstructor(id: number, _instructor: IInstructor): Observable<string> {
  const formData = this.toFormData(_instructor);

     return this._httpClient.put(`${environment.baseUrl}/instructor/${id}`, formData, {
        responseType: 'text'
      });
  }

  deleteInstructor(id: number): Observable<string> {
    return this._httpClient.delete<string>(`${environment.baseUrl}/instructor/${id}`);
  }
  
  getInstructorById(id: number): Observable<IUser> {
    let token: string | null = this._authUser.getToken();
    return this._httpClient.get<IUser>(`${environment.baseUrl}/instructor/id/${id}`, {
      headers: new HttpHeaders({
        'content-type': 'application/json',
        'authorization': `Bearer ${token}`
      })
    });
  }
}




