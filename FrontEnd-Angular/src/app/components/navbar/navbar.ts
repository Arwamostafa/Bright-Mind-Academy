import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserAuthentication } from '../../services/user-authentication';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar implements OnInit {

  // isAdminLogged: boolean = false;
  // isInstructorLogged: boolean = false;
  // isStudentLogged: boolean = false;

  constructor(public _authSer: UserAuthentication) {

  //   this.isAdminLogged = false;
  // this.isInstructorLogged= false;
  // this.isStudentLogged = false;
  }

  ngOnInit(): void {
    // this._authSer.getAdminAuthenticationService().subscribe({
    //   next: (status) => {this.isAdminLogged = status},
    //   error: (er) => {console.log(er)}

    // });

    // this._authSer.getInstructorAuthenticationService().subscribe({
    //   next: (status) => {this.isInstructorLogged = status},
    //   error: (er) => {console.log(er)}

    // });

    // this._authSer.getStudentAuthenticationService().subscribe({
    //   next: (status) => {this.isStudentLogged = status},
    //   error: (er) => {console.log(er)}

    // });


  }

  logout():void {
    this._authSer.logout();
  }



}
