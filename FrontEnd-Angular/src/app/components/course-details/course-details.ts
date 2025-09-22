import { QuizService } from './../../services/quiz-service';
import { Component, OnInit, Input, ViewChild , ElementRef, OnChanges, SimpleChanges, output } from '@angular/core';

import 'aos/dist/aos.css';
import { CommonModule } from '@angular/common';
import { Quiz } from "../quiz/quiz";

import { Ilesson } from '../../models/ilesson';
import { LessonService } from '../../services/lesson-service';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';


@Component({
  selector: 'app-course-details',
  imports: [CommonModule, Quiz,CommonModule, ReactiveFormsModule],
  templateUrl: './course-details.html',
  styleUrl: './course-details.css'
})
export class CourseDetails implements OnInit, OnChanges {

 @Input() lessonId!: number ;
 @Input() lessonIdAssigment!: number;
  lessonData: Ilesson | null = null;
  currentTab: string = 'overview';
quizForm: FormGroup;
submitted = false;
score = 0;
questions: any[] = [];
totalMarks = 0;

@ViewChild('resultPdf') resultPdf!: ElementRef;

  constructor(private lessonService: LessonService ,private QuizService:QuizService,   private formBuilder: FormBuilder,
) {
    this.quizForm = this.formBuilder.group({});

    
  }

  ngOnInit(): void {

  
    // this.QuizService.getQuizByLessonId(this.lessonId).subscribe((data: any) => {
    //   this.totalMarks = data.totalMarks;
    //   this.questions = data.questions;

    //   this.questions.forEach((q: any) => {
    //     this.quizForm.addControl(q.id.toString(), this.formBuilder.control(''));
    //   });
    // });
  

    this.loadLesson();
  }

  ngOnChanges(changes: SimpleChanges) {
   if (changes['lessonId'] && changes['lessonId'].currentValue) {
  this.loadLesson();
}
  }

  loadLesson() {
  if (this.lessonId) {
    this.lessonService.getById(this.lessonId).subscribe({
      next: (lesson) => {
        this.lessonData = lesson;
        this.currentTab = 'overview';

        this.QuizService.getQuizByLessonId(this.lessonId).subscribe((data: any) => {
          this.totalMarks = data.totalMarks;
          this.questions = data.questions;

          this.questions.forEach((q: any) => {
            this.quizForm.addControl(q.id.toString(), this.formBuilder.control(''));
          });
        });
      },
      error: (err) => {
        console.error('Error loading lesson', err);
        this.lessonData = null;
      }
    });
  }
}
  setTab(tabName: string) {
    console.log('Switching tab to:', tabName);
  this.currentTab = tabName;
}

  onDownload() {
    if (this.lessonData?.pdfUrl) {
      this.lessonService.downloadFile(this.lessonData.pdfUrl);
    }
  }

getCorrectOptionId(q: any): string {
  return q.options.find((o: any) => o.isCorrect)?.id?.toString() ?? '';
}

getCorrectOptionName(q: any): string {
  return q.options.find((o: any) => o.isCorrect)?.name ?? '';
}

submitQuiz() {
  this.submitted = true;
  this.score = this.QuizService.calculateScore(this.quizForm.value);
}

resetQuiz() {
  this.quizForm.reset();
  this.submitted = false;
  this.score = 0;
}

downloadPDF() {
  const element = this.resultPdf.nativeElement;
  html2canvas(element).then(canvas => {
    const imgData = canvas.toDataURL('image/png');
    const pdf = new jsPDF('p', 'mm', 'a4');
    const imgProps = pdf.getImageProperties(imgData);
    const pdfWidth = pdf.internal.pageSize.getWidth();
    const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;
    pdf.addImage(imgData, 'PNG', 0, 0, pdfWidth, pdfHeight);
    pdf.save('quiz-result.pdf');
  });
}
}