import { Component, OnInit } from '@angular/core';
import { IStudent } from '../../models/istudent';
import { StudentService } from '../../services/student-service';
import { UserAuthentication } from '../../services/user-authentication';
import { IUser } from '../../models/iuser';
import { AdminService } from '../../services/admin-service';
import { InstructorService } from '../../services/instructor-service';

@Component({
  selector: 'app-info',
  imports: [],
  templateUrl: './info.html',
  styleUrl: './info.css'
})
export class Info implements OnInit {

  user!: IUser;
  constructor(private _authService: UserAuthentication, private _instructorService: InstructorService, private _studentService: StudentService, private _adminService: AdminService){}

  ngOnInit(): void {
    let userId: number = this._authService.getUserId();
    if(this._authService.getRole() == "Admin"){
      this._adminService.getAdminById(userId).subscribe({
        next: (data)=> this.user = data,
        error: (error) => console.log(error)

      })
    }
    else if(this._authService.getRole() == "Instructor"){
      this._instructorService.getInstructorById(userId).subscribe({
        next: (data)=> this.user = data,
        error: (er) => console.log(er)

      })
    }else if(this._authService.getRole() == "Student"){
      this._studentService.getStudentById(userId).subscribe({
        next: (data)=> this.user = data,
        error: (error) => console.log(error)

      })
    }
  }
}
