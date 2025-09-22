import { QuizService } from './../../services/quiz-service';
import { LessonService } from './../../services/lesson-service';
import { SubjectService } from './../../services/subject-service';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { UnitService } from '../../services/unit-service';
import { IUnit } from '../../models/iunit';
import { CommonModule } from '@angular/common';
import { Ilesson } from '../../models/ilesson';
// import { QuizService } from '../../services/quiz-service';
import JSZip from 'jszip';
import { Iquiz } from '../../models/iquiz.ts';
@Component({
  selector: 'app-adding-unit-and-lesson',
  imports: [ FormsModule, ReactiveFormsModule, CommonModule ],
  templateUrl: './adding-unit-and-lesson.html',
  styleUrl: './adding-unit-and-lesson.css'
})
export class AddingUnitAndLesson implements OnInit {
  lesson:Ilesson={id: 0,
  title: '',
  description: '',
  unitId: 0,
  unitName:'null',
  videoUrl: "null",
  assigmentUrl: "null",
  pdfUrl: "null",



  assigmentDeadLine:new Date()}
   quizForm!: FormGroup;
  subjectsQuiz: any[] = [];
  unitsquiz: any[] = [];
  lessonsquiz: any[] = [];
  quizzes: Iquiz[] = [];
pdfFileData: File | null = null;
assignmentFileData: File | null = null;
  invalidVideoType: boolean = false;
invalidPdfType: boolean = false;
invalidAssignmentType: boolean = false;
  isDetails:boolean=false;
lessonForm!: FormGroup;
  unitForm!: FormGroup;
     QuizForm!: FormGroup;
 subjects: any[] = [];
   updateee:boolean=false;
  availableLessons: Ilesson[] = [];
 unitToUpdate: IUnit | null = null;
Units: IUnit[] = [];
 Lessons:Ilesson[] = [];
isDetailsQuiz = false;
  quizDetails!: Iquiz;
  updateMode = false;
  constructor(private fb: FormBuilder,private unitService: UnitService , private SubjectService : SubjectService , private LessonService:LessonService ,private QuizService:QuizService) {}

   ngOnInit(): void {

//!SECTION 1: Initialize Forms

//ANCHOR - Unit Form

    this.unitForm = this.fb.group({
     id: [''],
  title: ['', [Validators.required, Validators.minLength(3)]],
  description: ['', [Validators.required, Validators.minLength(10)]],
  subjectId: [null, [Validators.required, Validators.pattern('^[0-9]+$')]],
    });
//ANCHOR - Lesson Form

this.lessonForm = this.fb.group({
     id: [0],
  title: ['', [Validators.required, Validators.minLength(3)]],
  description: ['', [Validators.required, Validators.minLength(10)]],
  unitId: ['', Validators.required],
subjectId: ['', Validators.required],
  assignmentDeadline: [null]
    });

//ANCHOR - Quiz Form
    this.QuizForm = this.fb.group({
  id: [0],
  description: ['', [Validators.required, Validators.minLength(3)]],
  assignedBefore: [false],
  totalMarks: [0, Validators.required],
  subjectId: ['', Validators.required],
  unitId: ['', Validators.required],
  lessonId: ['', Validators.required],
  questions: this.fb.array([])  
});






//!SECTION 2: Load Subjects, Units, and Lessons

//ANCHOR - Load Subjects

          this.SubjectService.getAllSubject().subscribe({
      next: (response) => {
        this.subjects = response;
        console.log('Subjects fetched successfully:', this.subjects);
      },
      error: (error) => console.error('Error fetching subjects:', error)
    });

//ANCHOR - Load Units and Lessons

    this.unitService.getAllUnits().subscribe({
      next: (response) => {
        this.Units = response;
        console.log('Units fetched successfully:', this.Units);
      },
      error: (error) => console.error('Error fetching units:', error)
    });

//ANCHOR - Load Lessons
    this.LessonService.getAll().subscribe({
      next: (response) => {
        this.Lessons = response;
        console.log('Lessons fetched successfully:', response);
      },
      error: (error) => console.error('Error fetching lessons:', error)
    });

       this.loadQuizzes();


  }

  //!SECTION 3:Unit
  //ANCHOR - Submit Unit Form
   onSubmit(): void {

    if(this.unitForm.valid) {
      if(this.unitForm.value.id) {
        const { id, ...unitWithoutId } = this.unitForm.value;
        this.unitService.updateUnit(this.unitForm.value.id, unitWithoutId).subscribe({
          next: (response) => {console.log('Unit updated successfully:', response)
            this.unitForm.reset();
      this.loadUnits();
          } ,
          error: (error) => {
          console.error('Error adding unit:', error);
  if (error.error && error.error.errors) {
    console.log('Validation errors:', error.error.errors);
  }
          }
        });
        this.unitForm.reset();
        this.loadUnits();
      }else {
        const { id, ...unitWithoutId } = this.unitForm.value;
      const unit:IUnit = this.unitForm.value;
      this.unitService.addUnit(unitWithoutId).subscribe({
        next: (response) =>{console.log('Unit added successfully:', response)
           this.unitForm.reset();
      this.loadUnits();} ,
        error: (error) => console.error('Error adding unit:', error)
      });
      this.unitForm.reset();
      this.loadUnits();
    }

      }
    }
  //ANCHOR - Update Unit
   UPDATEUnit(id:number): void {
    this.unitService.getUnitById(id).subscribe({
      next: (unit) =>this.unitForm.patchValue({
      id: unit.id,
      title: unit.title,
      description: unit.description,
      subjectId: unit.subjectId
    }),
      error: (error) => console.error('Error fetching unit:', error)
    });
  }
  //ANCHOR - Delete Unit
   DELETE(id: number): void {
    this.unitService.deleteUnit(id).subscribe({
      next: () => {
        console.log('Unit deleted successfully');
        this.Units = this.Units.filter(unit => unit.id !== id);
      },
      error: (error) => console.error('Error deleting unit:', error)
    });
    }
    //ANCHOR - load units
  loadUnits(): void {
  this.unitService.getAllUnits().subscribe({
    next: (response) => this.Units = response,
    error: (error) => console.error('Error fetching units:', error)
  });


}
  //!SECTION 4 : Lesson
  //ANCHOR - Submit Lesson Form
onLessonSubmit(): void {

  Object.keys(this.lessonForm.controls).forEach(key => {
    const control = this.lessonForm.get(key);
    if (control?.invalid) {
      // console.log(${key} is invalid:, control.errors);
    }
  });



  const formValid = this.lessonForm.get('title')?.valid &&
                   this.lessonForm.get('description')?.valid &&
                   this.lessonForm.get('unitId')?.valid;

  const videoValid = this.videoFileData && !this.invalidVideoType;



  if (formValid && videoValid && !this.invalidPdfType && !this.invalidAssignmentType) {
    const formData = new FormData();

    formData.append('title', this.lessonForm.get('title')?.value || '');
    formData.append('description', this.lessonForm.get('description')?.value || '');
    formData.append('unitId', this.lessonForm.get('unitId')?.value || '');


if (this.videoFileData) {
  formData.append('videoUrl', this.videoFileData);
} else if (this.oldVideoUrl && this.oldVideoUrl.trim() !== '') {
  formData.append('videoUrl', this.oldVideoUrl);
}

if (this.pdfFileData) {
  formData.append('pdfUrl', this.pdfFileData);
} else if (this.oldPdfUrl && this.oldPdfUrl.trim() !== '') {
  formData.append('pdfUrl', this.oldPdfUrl);
}

if (this.assignmentFileData) {
  formData.append('assigmentUrl', this.assignmentFileData);
} else if (this.oldAssignmentUrl && this.oldAssignmentUrl.trim() !== '') {
  formData.append('assigmentUrl', this.oldAssignmentUrl);
}

    const deadline = this.lessonForm.get('assignmentDeadline')?.value;
    if (deadline) {

      const formattedDate = new Date(deadline).toISOString();
      formData.append('assigmentDeadLine', formattedDate);
    }


    formData.forEach((value, key) => {
      if (value instanceof File) {
        // console.log(${key}: File - ${value.name} (${value.size} bytes, ${value.type}));
      } else {
        // console.log(`${key}: ${value}`);
      }
    });

    const lessonId = this.lessonForm.get('id')?.value;

    if (lessonId && lessonId > 0) {
     //NOTE - update
      console.log('Updating lesson with ID:', lessonId);
      this.LessonService.updateLesson(lessonId, formData).subscribe({
        next: (response) => {
          console.log('Lesson updated successfully:', response);
          this.resetLessonForm();
        },
        error: (error) => {
          console.error('=== UPDATE ERROR DETAILS ===');
          console.error('Full Error:', error);
          console.error('Error Status:', error.status);
          console.error('Error Message:', error.message);
          console.error('Error Body:', error.error);

          if (error.error && error.error.errors) {
            console.error('Validation Errors:', error.error.errors);
            Object.keys(error.error.errors).forEach(key => {
              // console.error(Field: ${key}, Errors:, error.error.errors[key]);
            });
          }
        }
      });
    } else {
      //NOTE -  Add new lesson
      console.log('Adding new lesson');
      this.LessonService.addLesson(formData).subscribe({
        next: (response) => {
          console.log('Lesson added successfully:', response);
          this.resetLessonForm();
        },
        error: (error) => {

          console.error('Full Error:', error);
          console.error('Error Status:', error.status);
          console.error('Error Message:', error.message);
          console.error('Error Body:', error.error);

          if (error.error && error.error.errors) {
            console.error('Validation Errors:', error.error.errors);
            Object.keys(error.error.errors).forEach(key => {
              // console.error(Field: ${key}, Errors:, error.error.errors[key]);
            });
          }


          if (error.status === 400) {
            console.error('Bad Request - Check field names and data types');
            console.error('Make sure field names match exactly with backend DTO');
          } else if (error.status === 413) {
            console.error('Payload too large - Check file sizes');
          } else if (error.status === 415) {
            console.error('Unsupported media type - Check file formats');
          }
        }
      });
    }
  } else {

    console.error('Form Valid:', formValid);
    console.error('Video Valid:', videoValid);
    console.error('Invalid PDF Type:', this.invalidPdfType);
    console.error('Invalid Assignment Type:', this.invalidAssignmentType);

    this.markFormGroupTouched();
  }
}


//ANCHOR - Input Validation
private markFormGroupTouched(): void {
  Object.keys(this.lessonForm.controls).forEach(key => {
    this.lessonForm.get(key)?.markAsTouched();
  });
}

//ANCHOR - reset LessonForm
private resetLessonForm(): void {
  this.lessonForm.reset();
  this.lessonForm.patchValue({ id: 0 }); // إعادة تعيين الـ id إلى 0
  this.videoFileData = null;
  this.pdfFileData = null;
  this.assignmentFileData = null;
  this.invalidVideoType = false;
  this.invalidPdfType = false;
  this.invalidAssignmentType = false;
  this.ngOnInit(); // إعادة تحميل البيانات
}

oldVideoUrl: string | null = null;
oldPdfUrl: string | null = null;
oldAssignmentUrl: string | null = null;

//ANCHOR - Update lesson
UpdateLesson(id: number) {

  this.LessonService.getById(id).subscribe({
    next: (lesson) => {

      this.lessonForm.patchValue({
        id: lesson.id,
        title: lesson.title,
        description: lesson.description,
        unitId: lesson.unitId,
        assignmentDeadline: lesson.assigmentDeadLine
          ? new Date(lesson.assigmentDeadLine).toISOString().split('T')[0]
          : null
      });


      if (lesson.unitId) {
        this.unitService.getUnitById(lesson.unitId).subscribe({
          next: (unit) => {
            if (unit && unit.subjectId) {
              this.lessonForm.patchValue({ subjectId: unit.subjectId });
            }
          },
          error: (err) => console.error('Error fetching unit:', err)
        });
      }
this.updateee=true

      this.oldVideoUrl = lesson.videoUrl || null;
      this.oldPdfUrl = lesson.pdfUrl || null;
      this.oldAssignmentUrl = lesson.assigmentUrl || null;

      this.videoFileData = null;
      this.pdfFileData = null;
      this.assignmentFileData = null;
      this.invalidVideoType = false;
      this.invalidPdfType = false;
      this.invalidAssignmentType = false;

    },
    error: (error) => console.error('Error fetching lesson:', error)
  });

}
  //ANCHOR - Delete lesson
  DeleteLesson(id:number){
    this.LessonService.deleteLesson(id).subscribe({
    next:()=>{
      console.log('lessom deleted sucessfully');
      this.Lessons=this.Lessons.filter(lesson=>lesson.id!==id);
    },
    error:(error)=>console.error('error deleting lesson :',error)
    });
  }



//SECTION -  File Validation and Download
//ANCHOR - Validate Video File
validateVideoFile(event: Event): void {
  const input = event.target as HTMLInputElement;

  if (input.files && input.files.length > 0) {
    const file = input.files[0];
    const isVideo = file.type.startsWith('video/');

    this.invalidVideoType = !isVideo;
  }
}

//ANCHOR - Compress Video File
videoFileData: File | null = null;

async onVideoFileChange(event: Event) {

  const input = event.target as HTMLInputElement;
  const file = input.files?.[0];

  if (file) {
    console.log('Video file selected:', file.name, file.size, file.type);

    const isVideo = file.type.startsWith('video/');
    this.invalidVideoType = !isVideo;

    if (!this.invalidVideoType) {
      try {
        const zipFile = await this.compressFileToZip(file, `video_${Date.now()}.zip`);
        this.videoFileData = new File([zipFile], `video_${Date.now()}.zip`, { type: 'application/zip' });
        console.log('Video file compressed successfully:', this.videoFileData.size);
      } catch (error) {
        console.error('Error compressing video file:', error);
        this.invalidVideoType = true;
        this.videoFileData = null;
      }
    } else {
      console.error('Invalid video file type:', file.type);
      this.videoFileData = null;
    }
  } else {
    this.videoFileData = null;
    this.invalidVideoType = false;
  }
}


//ANCHOR - compress Assignment And Resourse File
async onPdfFileChange(event: any) {
const input = event.target as HTMLInputElement;
  if (!input.files || input.files.length === 0) return;

  const file = input.files[0];
  const isPdf = file.type === 'application/pdf';

  if (!isPdf) {
    if (input.id === 'AssigmentInput') {
      this.invalidAssignmentType = true;
      this.assignmentFileData = null;
    } else if (input.id === 'pdfInput') {
      this.invalidPdfType = true;
      this.pdfFileData = null;
    }
    return;
  }


  try {
    const zipFile = await this.compressFileToZip(file, `${input.id}_${Date.now()}.zip`);
    const zippedFile = new File([zipFile], `${input.id}_${Date.now()}.zip`, { type: 'application/zip' });

    if (input.id === 'AssigmentInput') {
      this.invalidAssignmentType = false;
      this.assignmentFileData = zippedFile;
    } else if (input.id === 'pdfInput') {
      this.invalidPdfType = false;
      this.pdfFileData = zippedFile;
    }
  } catch (error) {
    console.error('Error compressing file:', error);
    if (input.id === 'AssigmentInput') {
      this.invalidAssignmentType = true;
      this.assignmentFileData = null;
    } else if (input.id === 'pdfInput') {
      this.invalidPdfType = true;
      this.pdfFileData = null;
    }
  }
}


//ANCHOR -  File Selected
onFileSelected(event: any, controlName: string) {
  const file = event.target.files[0];
  if (file) {
    this.lessonForm.get(controlName)?.setValue(file);
  }
}


//ANCHOR - Download pdf

 downloadPdf(urlfile:string) {
  this.LessonService.downloadFile(urlfile);
}

 //ANCHOR - compress File To Zip
 async compressFileToZip(file: File, zipFileName: string): Promise<Blob> {
try {
    // console.log(Starting compression for ${file.name}...);

    if (typeof JSZip === 'undefined') {
      throw new Error('JSZip library is not loaded');
    }

    const zip = new JSZip();

    zip.file(file.name, file);

    console.log('File added to ZIP, generating...');

    const content = await zip.generateAsync({
      type: 'blob',
      compression: 'DEFLATE',
      compressionOptions: {
        level: 6
      }
    });

    // console.log(Compression completed. Original: ${file.size} bytes, Compressed: ${content.size} bytes);

    return content;
  } catch (error) {
    console.error('Compression failed:', error);
    throw error;
  }
}

//ANCHOR - Get Lesson
GetById(id:number){
 this.isDetails=true
 this.LessonService.getById(id).subscribe({
  next:(response)=>{
this.lesson=response
  }
 })
}

closeDetails(){
  this.isDetails=false
}



//!SECTION 6 - tabs
 activeTab = 0;

tabs = [
  { label: 'Details Of Lesson', key: 'description', icon: 'fas fa-info-circle' },
  { label: 'Resources', key: 'pdf', icon: 'fas fa-file-pdf' },
  { label: 'Assignment', key: 'assignment', icon: 'fas fa-tasks' }
];

selectTab(index: number, event: Event) {
  event.preventDefault();
  this.activeTab = index;

  setTimeout(() => {
    const tabBar = document.querySelector('.tabs') as HTMLElement;
    const selector = tabBar.querySelector('.selector') as HTMLElement;
    const activeLink = tabBar.querySelectorAll('a')[index] as HTMLElement;

    if (activeLink && selector) {
      selector.style.width = `${activeLink.offsetWidth}px`;
      selector.style.left = `${activeLink.offsetLeft}px`;
    }
  }, 0);
}


loadUnitsBySubjectId(subjectId: number) {
  this.unitService.getUnitsBySubjectId(subjectId).subscribe({
    next: (units) => {
      this.Units = units;
      this.lessonForm.patchValue({ unitId: '' });
    },
    error: (err) => {
      console.error('Error fetching units:', err);
    }
  })};

onSubjectChange(event: Event) {
  const subjectId = +(event?.target as HTMLSelectElement).value;
  if (subjectId) {
    this.loadUnitsBySubjectId(subjectId);
  }
}

//!SECTION 7: Quiz

  get questions(): FormArray {
  return this.QuizForm.get('questions') as FormArray;
}

 
  newOption(): FormGroup {
    return this.fb.group({
      id: [0],
      name: ['', Validators.required],
      isCorrect: [false],
    });
  }

  newQuestion(): FormGroup {
    return this.fb.group({
      id: [0],
      content: ['', Validators.required],
      mark: [0, Validators.required],
      options: this.fb.array([this.newOption()]),
    });
  }

  addQuestion(): void {
    this.questions.push(this.newQuestion());
  }

  removeQuestion(i: number): void {
    this.questions.removeAt(i);
  }
getOptions(qIndex: number): FormArray {
  return this.questions.at(qIndex).get('options') as FormArray;
}

  addOption(qIndex: number): void {
    const opts = this.questions.at(qIndex).get('options') as FormArray;
    opts.push(this.newOption());
  }

  removeOption(qIndex: number, oIndex: number): void {
    const opts = this.questions.at(qIndex).get('options') as FormArray;
    opts.removeAt(oIndex);
  }

  loadSubjects() {
    this.SubjectService.getAllSubject().subscribe(res => (this.subjectsQuiz = res));
  }


  onSubjectChangeQuiz(event: any) {
    const subjectId = event.target.value;
    this.unitService.getUnitsBySubjectId(subjectId).subscribe(res => (this.unitsquiz = res));
  }

  onUnitChangeQuiz(event: any) {
    const unitId = event.target.value;
    this.LessonService.getByUnitId(unitId).subscribe(res => (this.lessonsquiz = res));
  }

  private buildQuizPayload(): Iquiz {
   return {
    description: this.QuizForm.value.description,
    assignedBefore: this.QuizForm.value.assignedBefore,
    totalMarks: this.QuizForm.value.totalMarks,
    lessonId: this.QuizForm.value.lessonId,
    questions: this.QuizForm.value.questions.map((q: any) => ({
      content: q.content,
      mark: q.mark,
      options: q.options.map((o: any) => ({
        name: o.name,
        isCorrect: o.isCorrect
      }))
    }))
  };
}

onQuizSubmit() {
  if (this.QuizForm.invalid) {
    this.QuizForm.markAllAsTouched();
    return;
  }

  const quizPayload = this.buildQuizPayload();

  if (this.QuizForm.value.id && this.QuizForm.value.id > 0) {
    const quizId = this.QuizForm.value.id;
    this.QuizService.updateQuiz(quizId, quizPayload).subscribe({
      next: () => {
         this.loadQuizzes();
        this.resetForm();
      },
    });
  } else {
    this.QuizService.addQuiz(quizPayload).subscribe({
      next: () => {
        this.loadQuizzes();
        this.resetForm();
      },
    });
  }
}

  
  loadQuizzes() {
    this.QuizService.getAllQuizzes().subscribe(res => (this.quizzes = res));
  }

editQuiz(quiz: Iquiz) {
this.updateMode = true;

  this.QuizForm.patchValue({
    id: quiz.id,
    description: quiz.description,
    assignedBefore: quiz.assignedBefore,
    totalMarks: quiz.totalMarks,
    lessonId: quiz.lessonId
  });

const lessonId = quiz.lessonId ?? 0;
if (lessonId) {
  this.LessonService.getById(lessonId).subscribe(lesson => {
    if (lesson) {
     
      this.QuizForm.patchValue({ 
        lessonId: lesson.id, 
        unitId: lesson.unitId 
      });

      this.LessonService.getByUnitId(lesson.unitId).subscribe(lessons => {
        this.lessonsquiz = lessons;

        this.QuizForm.patchValue({ lessonId: lesson.id });
      });

      this.unitService.getUnitById(lesson.unitId).subscribe(unit => {
        if (unit) {
          this.QuizForm.patchValue({ subjectId: unit.subjectId });

          this.unitService.getUnitsBySubjectId(unit.subjectId).subscribe(units => {
            this.unitsquiz = units;

            this.QuizForm.patchValue({ unitId: lesson.unitId });
          });
        }
      });
    }
  });}


  this.questions.clear();
  quiz.questions?.forEach(q => {
    const qGroup = this.newQuestion();
    qGroup.patchValue({ content: q.content, mark: q.mark });
    this.questions.push(qGroup);

    const opts = qGroup.get('options') as FormArray;
    opts.clear();
    q.options?.forEach(o => {
      const oGroup = this.newOption();
      oGroup.patchValue({ name: o.name, isCorrect: o.isCorrect });
      opts.push(oGroup);
    });
  });}

  deleteQuiz(id?: number) {
    this.QuizService.deleteQuiz(id).subscribe(() => {
      this.quizzes = this.quizzes.filter(q => q.id !== id);
    });
  }

  showDetails(quiz: Iquiz) {
    this.quizDetails = quiz;
    this.isDetailsQuiz = true;
  }

  closeDetailsQuiz() {
    this.isDetailsQuiz = false;
  }

  resetForm() {
    this.quizForm.reset();
    this.quizForm.patchValue({ id: 0 });
    this.questions.clear();
    this.updateMode = false;
  }
  quizTabs = [
  { key: 'details', label: 'Details', icon: 'bi bi-info-circle' },
  { key: 'questions', label: 'Questions', icon: 'bi bi-list-check' }
];

activeQuizTab = 0;

selectQuizTab(i: number, event: Event) {
  event.preventDefault();
  this.activeQuizTab = i;
}


}