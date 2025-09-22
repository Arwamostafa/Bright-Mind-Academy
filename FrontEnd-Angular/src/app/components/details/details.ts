import { SubjectService } from './../../services/subject-service';
import { Component, OnInit } from '@angular/core';
import { ApiSubjectSrevice } from '../../services/api-subject-srevice';
import { IISubject, ISubject } from '../../models/isubject';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-details',
  imports: [CurrencyPipe, RouterLink],
  templateUrl: './details.html',
  styleUrl: './details.css'
})
export class Details implements OnInit{

  currentId : number = 0;
  studentId:number=6;
  subject!: ISubject | undefined ;
  constructor(private apiSubjectService: ApiSubjectSrevice, private activatedRoute: ActivatedRoute ){}

  ngOnInit(){
    this.currentId = Number(this.activatedRoute.snapshot.paramMap.get("id"));
    this.apiSubjectService.getSubjectById(this.currentId).subscribe({
      next:(response)=>{
        console.log(response);

          this.subject=response
      }
    });
  }



}
