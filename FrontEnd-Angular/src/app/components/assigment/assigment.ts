import { Component , ViewChild , ElementRef  } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AssigmentService } from '../../services/assigment-service';
import { PDFService } from '../../services/pdfservice';
@Component({
  selector: 'app-assigment',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './assigment.html',
  styleUrl: './assigment.css'
})
export class Assigment {
quizForm: FormGroup;
  submitted = false;
  score = 0;
  questions: any[] = [];
constructor(private formBuilder: FormBuilder , private elementRef: ElementRef , private AssigmentService : AssigmentService , private pdfService: PDFService) {
this.questions = this.AssigmentService.getQuestions();
    this.quizForm = this.formBuilder.group({});

    this.questions.forEach(q => {
      this.quizForm.addControl(q.id.toString(), this.formBuilder.control(''));
    });

 }
@ViewChild('printThis', { static: false }) printThis!: ElementRef;


  submitQuiz() {
    this.submitted = true;
    this.score = this.AssigmentService.calculateScore(this.quizForm.value);
  }

  resetQuiz() {
    this.quizForm.reset();
    this.submitted = false;
    this.score = 0;
  }
  onDownload() {
  const fileUrl = 'https://drive.google.com/uc?export=download&id=1u64QJPg7tXjUjtYqfAVL7eaCQmSyLAli';
  this.pdfService.downloadFromUrl(fileUrl, 'quiz-result.pdf');
}


}
