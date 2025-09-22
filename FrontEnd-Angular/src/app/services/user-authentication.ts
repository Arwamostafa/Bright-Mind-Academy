import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserAuthentication {

  // private adminAuthService: BehaviorSubject<boolean>
  // private instructorAuthService: BehaviorSubject<boolean>
  // private studentAuthService: BehaviorSubject<boolean>

  // userRole!: string ;
  // userId!: number;
  // userName!: string;


  constructor() {
    // this.adminAuthService = new BehaviorSubject<boolean>(false);
    // this.instructorAuthService = new BehaviorSubject<boolean>(false);
    // this.studentAuthService = new BehaviorSubject<boolean>(false);
  }

  saveAuthData(token: string):void{
    localStorage.setItem('token',token);
    // this.getRole();
    // localStorage.setItem('role',role);
    // localStorage.setItem('name',userName);
    // localStorage.setItem('id',userId.toString());

    // this.userRole = role;
    // this.userId = userId;
    // this.userName = userName;
  }

  getToken():string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null{
    const token = this.getToken();

    if(!token){
      return "";
    }else{

      const payload = JSON.parse(atob(token.split('.')[1]));
      // console.log(payload.role);
      return payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }


    // console.log(payload);
    // console.log(payload.id);
    // console.log(payload.role);
    // console.log(payload.fullName);
    // console.log(payload.userName);



  }

  getUserName(): string {
    const token = this.getToken();

    if(!token){
      return "";
    }else{

      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.userName;
    }
  }

  getFullName(): string {
    const token = this.getToken();

    if(!token){
      return "";
    }else{

      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.fullName;
    }
  }

  getUserId(): number  {
    const token = this.getToken();

    if(!token){
      return 0;
    }else{

      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.id;
    }
  }

  isAdminLogged(): boolean {
    return this.getRole() === "Admin";
  }

  isInstructorLogged(): boolean {
    return this.getRole() === "Instructor";
  }

  isStudentLogged(): boolean {
    return this.getRole() === "Student";
  }

  logout(): void {
    localStorage.removeItem('token');
    // this.userRole = "";
    // this.userId = 0;
    // this.userName = "";
  }

}



  // login(_user: ILoginUser) : Observable<IResponse>{
  //     return this._httpClient.post<IResponse>(`${environment.baseUrl}/login/login`, _user,{
  //       headers: new HttpHeaders({
  //         'content-type': "application/json"
  //       })
  //     });

    // localStorage.setItem("token","qwe123!@#");
    // this.authService.next(true);
  // }
  // loginAdmin(){
  //   this.adminAuthService.next(true);
  // }

  // logoutAdmin(){
  //   localStorage.removeItem("token");
  //   // this.authService.next(false);

  //   this.adminAuthService.next(false);
  // }

  // loginInstructor(){
  //   this.instructorAuthService.next(true);
  // }

  // logoutInstructor(){
  //   localStorage.removeItem("token");
  //   // this.authService.next(false);
  //   this.instructorAuthService.next(false);
  // }

  // loginStudent(){
  //   this.studentAuthService.next(true);
  // }

  // logoutStudent(){
  //   localStorage.removeItem("token");
  //   // this.authService.next(false);
  //   this.studentAuthService.next(false);
  // }

  // getAdminAuthenticationService(): BehaviorSubject<boolean>{
  //   return this.adminAuthService;
  // }
  // getInstructorAuthenticationService(): BehaviorSubject<boolean>{
  //   return this.instructorAuthService;
  // }
  // getStudentAuthenticationService(): BehaviorSubject<boolean>{
  //   return this.studentAuthService;
  // }

