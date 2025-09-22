
import { Component , ViewChild , ElementRef  } from '@angular/core';

import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../services/quiz-service';
import { PDFService } from '../../services/pdfservice';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
@Component({
  selector: 'app-quiz',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './quiz.html',
  styleUrl: './quiz.css'
})
export class Quiz {

  // quizForm: FormGroup;
  // submitted = false;
  // score = 0;
  // questions: any[] = [];
  // totalMarks = 0;

  // @ViewChild('resultPdf') resultPdf!: ElementRef;

  // constructor(
  //   private formBuilder: FormBuilder,
  //   private elementRef: ElementRef,
  //   private QuizService: QuizService
  // ) {
  //   this.quizForm = this.formBuilder.group({});

  //   const quizId = 1;
  //   this.QuizService.loadQuestionsFromApi(quizId).subscribe((data: any) => {
  //     this.totalMarks = data.totalMarks;
  //     this.questions = data.questions;

  //     this.questions.forEach((q: any) => {
  //       this.quizForm.addControl(q.id.toString(), this.formBuilder.control(''));
  //     });
  //   });
  // }

  // getCorrectOptionId(q: any): string {
  //   return q.options.find((o: any) => o.isCorrect)?.id?.toString() ?? '';
  // }

  // getCorrectOptionName(q: any): string {
  //   return q.options.find((o: any) => o.isCorrect)?.name ?? '';
  // }

  // submitQuiz() {
  //   this.submitted = true;
  //   this.score = this.QuizService.calculateScore(this.quizForm.value);
  // }

  // resetQuiz() {
  //   this.quizForm.reset();
  //   this.submitted = false;
  //   this.score = 0;
  // }

  // downloadPDF() {
  //   const element = this.resultPdf.nativeElement;
  //   html2canvas(element).then(canvas => {
  //     const imgData = canvas.toDataURL('image/png');
  //     const pdf = new jsPDF('p', 'mm', 'a4');
  //     const imgProps = pdf.getImageProperties(imgData);
  //     const pdfWidth = pdf.internal.pageSize.getWidth();
  //     const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;
  //     pdf.addImage(imgData, 'PNG', 0, 0, pdfWidth, pdfHeight);
  //     pdf.save('quiz-result.pdf');
  //   });
  // }
  }







