import { Component } from '@angular/core';
import { ISubject } from '../../models/isubject';
import { ApiSubjectSrevice } from '../../services/api-subject-srevice';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-cart',
  imports: [CommonModule, RouterLink],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class Cart {

  subjects: ISubject[] = [];

  constructor(private _sub : ApiSubjectSrevice){}

  // ngOnInit(): void {
  //   this.subjects  = this._sub.getAllSubjects();
  // }
}
