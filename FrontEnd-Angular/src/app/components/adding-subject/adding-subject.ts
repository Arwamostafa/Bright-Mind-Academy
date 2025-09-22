

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray,FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IISubject } from '../../models/isubject';
import { IUnit } from '../../models/iunit';
// import { ILesson } from '../../models/lesson';
// import { IQuiz } from '../../models/iquiz.ts';
import { ClassService } from '../../services/class-service';
import { TrackService } from '../../services/track-service';
import { IClass } from '../../models/iclass.ts';
import { ITrack } from '../../models/itrack.ts';
import { SubjectService } from '../../services/subject-service';
import { AddingUnitAndLesson } from '../adding-unit-and-lesson/adding-unit-and-lesson';
import { IInstructor } from '../../models/iinstructor';
import { InstructorService } from '../../services/instructor-service';

@Component({
  selector: 'app-adding-subject',
  imports: [ReactiveFormsModule, CommonModule, AddingUnitAndLesson],
  templateUrl: './adding-subject.html',
  styleUrl: './adding-subject.css'
})
export class AddingSubject implements OnInit {
  academicYear: number = 0;
  editSubjectId: number | null = null;
  SubjectForm: FormGroup
  instructors: IInstructor[] = [];
  classes!:IClass[];
  tracks!:ITrack[];
  subjects!: IISubject[];
  // [   {id: 1, title: "physics", description: 'ayahaga', instructorName: 'Muhammad',className:'class1',trackName:'track1'},
  //     {id: 2, title: "Math", description: 'ayahaga', instructorName: 'Ahmed',className:'class2',trackName:'track2'}
  // ];

  // unit

  // editUnitId: number | null = null;
  // UnitForm: FormGroup
  // UnitSubjects: string[] = ["physics", "Math", "chemistry"]
  // units: IUnit[] =[
  //     {id: 1, title: "Motion", description: 'ayahaga',subjectId:2 ,subjectName: 'physics'},
  //     {id: 2, title: "Straight line", description: 'ayahaga',subjectId:2 ,subjectName: 'Math'}
  // ];

  // lesson

  // editId: number | null = null;
  // LessonForm: FormGroup
  // lessonSubjects: string[] = ["physics", "Math", "chemistry"]
  // lessonUnits:string[]=['unit1','unit2','unit3']
  // lessons: ILesson[]=[
  //     {id: 1, title: "Motion", description: 'ayahaga', pdfTitle: 'physics',videoTitle:'title1',assignmentDeadline:'12/10/2025',assignmentTitle:'ass1',unitTitle:'unit1',subjectTitle:'phy'},
  //     {id: 2, title: "Motion2", description: 'ayahaga2', pdfTitle: 'physics2',videoTitle:'title2',assignmentDeadline:'11-10-2025',assignmentTitle:'ass2',unitTitle:'unit2',subjectTitle:'Math'},
  // ];

  /* quiz */

  // editQuizId: number | null = null;
  // quizLessons: string[] = ["lesson1", "lesson2", "lesson3"];
  // QuizForm:FormGroup
  // quizes:IQuiz[]=[
  //   {
  //     id:1,lessonTitle:'lesson',quizname:'quizname',quizTotalMarks:20,questions:['q1,q2'],options:['option1','option2'],correctAnswer:'ans1',mark:12
  //   }
  // ];

  constructor(private _instructorService: InstructorService,private _classService: ClassService, private _trackService: TrackService, private _subjectService: SubjectService) {
    this.SubjectForm = new FormGroup(
      {
        subjectName: new FormControl('', [Validators.required, Validators.minLength(3)]),
        price: new FormControl('', [Validators.required, Validators.minLength(3)]),
        subjectDescription: new FormControl('', [Validators.required, Validators.minLength(5)]),
        instructorID: new FormControl('', [Validators.required]),
        classID:new FormControl('',[Validators.required]),
        trackID:new FormControl('',[Validators.required])
      }
    )

  //   this.UnitForm = new FormGroup(
  //     {
  //       title: new FormControl('', [Validators.required, Validators.minLength(3)]),
  //       description: new FormControl('', [Validators.required, Validators.minLength(10)]),
  //       subjectName: new FormControl('', [Validators.required])
  //     }
  //   )

  //   this.LessonForm = new FormGroup(
  //     {
  //       subjectTitle:new FormControl('',[Validators.required]),
  //       unitTitle:new FormControl('',[Validators.required]),
  //       title: new FormControl('', [Validators.required, Validators.minLength(3)]),
  //       description: new FormControl('', [Validators.required, Validators.minLength(50)]),
  //       pdfTitle:new FormControl('',[Validators.required]),
  //       videoTitle:new FormControl('',[Validators.required]),
  //       assignmentTitle:new FormControl('',[Validators.required]),
  //       assignmentDeadline:new FormControl('',[Validators.required]),
  //     }
  //   )

  //   this.QuizForm= new FormGroup({
  //   lessonTitle: new FormControl('', Validators.required),
  //   quizname: new FormControl('', Validators.required),
  //   quizTotalMarks: new FormControl('', Validators.required),
  //   questions:new FormArray([]),
  //   options:new FormArray([])
  // });

  }

  ngOnInit(): void{
    this._classService.getAllClasses().subscribe({
      next: (next) => {
        // console.log(next);
        this.classes = next
      }
    });

    this._trackService.getAllTracks().subscribe({
      next: (data) => {

        this.tracks = data ;
        // console.log(this.tracks);
      }
    });

    this._subjectService.getAllSubject().subscribe({
      next: (data) => {
        console.log("sdjnjksd");
        console.log(data);
        console.log("sdjnjksd");

        this.subjects = data;
      }
    });

    this._instructorService.getAllInstructors().subscribe({
      next: (data) => {
        this.instructors = data;
      },
      error: (err) => {
        console.log(err.err.errors);
      }

    })

  }
  // get unitTitle() { return this.SubjectForm.get('title'); }
  // get unitDescription() { return this.SubjectForm.get('description'); }
  // get unitInstructorName() { return this.SubjectForm.get('instructorName'); }
  DeleteSubject(id: number) {
    // this.subjects = this.subjects.filter(subject => subject.subjectID !== id);
    // If the deleted subject is being edited, reset the form

    console.log(id);

    this._subjectService.deleteSubject(id).subscribe({
          next : (data) => {

            console.log(data);
          },
          error: (err) => {
          console.error("Failed to delete subject:", err)
        }
    });
    this.ngOnInit();
    if (this.editSubjectId === id) {
      this.SubjectForm.reset();
      this.editSubjectId = null;
    }
  }

    get subjectName() { return this.SubjectForm.get('subjectName'); }
    get price() { return this.SubjectForm.get('price'); }
    get subjectDescription() { return this.SubjectForm.get('description');}

  UpdateSubject(id: number) {
    const subjectToUpdate = this.subjects.find(subject => subject.subjectID === id);
    if (subjectToUpdate) {
      this.SubjectForm.patchValue(subjectToUpdate);
      this.editSubjectId = id;  // Set edit mode
    }
  }

  addSubject() {
    if (this.SubjectForm.valid) {
      const formData = this.SubjectForm.value;

      if (this.editSubjectId !== null) {
        // Update existing subject
        const index = this.subjects.findIndex(i => i.subjectID === this.editSubjectId);
        if (index > -1) {
          // this.subjects[index] = { id: this.editSubjectId, ...formData };
          console.log(formData);

          this._subjectService.updateSubject(this.editSubjectId, formData).subscribe({
            next: (data)=>{
              console.log(data);

            },
            error: (err) => {
              console.error("Failed to update subject:", err)
            }
          })
          this.ngOnInit();
        }
        this.editSubjectId = null; // Exit edit mode
      } else {
        // Add new Subject
        // const newId = this.subjects.length > 0 ? Math.max(...this.subjects.map(i => i.id)) + 1 : 1;
        // this.subjects.push({ id: newId, ...formData });
        console.log(formData);
        console.log("sdfgsdf");


        this._subjectService.addSubject(formData).subscribe({
          next : (data) => {

            console.log(data);
          },
          error: (err) => {
          console.error("Failed to add subject:", err)
        }
        })
        this.ngOnInit();
      }
      this.SubjectForm.reset();

    } else {
      console.log('Form is invalid');
    }
  }

  // changeYear(val: any){
  //   this.academicYear = val.value;
  //   console.log(this.academicYear);
  //   this.on

  // }



/////////////////////////////////





  // // get subjectName() { return this.UnitForm.get('subjectName'); }
  // DeleteUnit(id: number) {
  //   this.units = this.units.filter(unit => unit.id !== id);
  //   // If the deleted unit is being edited, reset the form
  //   if (this.editUnitId === id) {
  //     this.UnitForm.reset();
  //     this.editUnitId = null;
  //   }
  // }
  // UpdateUnit(id: number) {
  //   const unitToUpdate = this.units.find(unit => unit.id === id);
  //   if (unitToUpdate) {
  //     this.UnitForm.patchValue(unitToUpdate);
  //     this.editUnitId = id;  // Set edit mode
  //   }
  // }
  // onSubmitUnit() {
  //   if (this.UnitForm.valid) {
  //     const formData = this.UnitForm.value;

  //     if (this.editUnitId !== null) {
  //       // Update existing unit
  //       const index = this.units.findIndex(i => i.id === this.editUnitId);
  //       if (index > -1) {
  //         this.units[index] = { id: this.editUnitId, ...formData };
  //       }
  //       this.editUnitId = null; // Exit edit mode
  //     } else {
  //       // Add new unit
  //       const newId = this.units.length > 0 ? Math.max(...this.units.map(i => i.id)) + 1 : 1;
  //       this.units.push({ id: newId, ...formData });
  //     }

  //     this.UnitForm.reset();
  //   } else {
  //     console.log('Form is invalid');
  //   }
  // }


// lesson





  // get lessonTitle() { return this.LessonForm.get('title'); }
  // get lessonDescription() { return this.LessonForm.get('description'); }
  // get pdfTitle(){return this.LessonForm.get('pdfTitle')}
  // get videoTitle(){return this.LessonForm.get('videoTitle')}
  // get assignmentTitle(){return this.LessonForm.get('assignmentTitle')}
  // get assignmentDeadline(){return this.LessonForm.get('assignmentDeadline')}
  // DeleteLesson(id: number) {
  //   this.lessons = this.lessons.filter(lesson => lesson.id !== id);
  //   // If the deleted unit is being edited, reset the form
  //   if (this.editId === id) {
  //     this.LessonForm.reset();
  //     this.editId = null;
  //   }
  // }
  // UpdateLesson(id: number) {
  //   const lessonToUpdate = this.lessons.find(lesson => lesson.id === id);
  //   if (lessonToUpdate) {
  //     this.LessonForm.patchValue(lessonToUpdate);
  //     this.editId = id;  // Set edit mode
  //   }
  // }
  // onSubmitLesson() {
  //   if (this.LessonForm.valid) {
  //     const formData = this.LessonForm.value;

  //     if (this.editId !== null) {
  //       // Update existing lesson
  //       const index = this.lessons.findIndex(i => i.id === this.editId);
  //       if (index > -1) {
  //         this.units[index] = { id: this.editId, ...formData };
  //       }
  //       this.editId = null; // Exit edit mode
  //     } else {
  //       // Add new lesson
  //       const newId = this.lessons.length > 0 ? Math.max(...this.lessons.map(i => i.id)) + 1 : 1;
  //       this.units.push({ id: newId, ...formData });
  //     }

  //     this.LessonForm.reset();
  //   } else {
  //     console.log('Form is invalid');
  //   }
  // }


  // /*      quiz      */


  // get questions(): FormArray {
  //   return this.QuizForm.get('questions') as FormArray;
  // }

  // addQuestion(): void {
  //   const questionGroup = new FormGroup({
  //     question: new FormControl('', [Validators.required, Validators.minLength(3)]),
  //     correctAnswer: new FormControl('', Validators.required),
  //     mark: new FormControl('', Validators.required),
  //     options: new FormArray([
  //       new FormControl('', Validators.required) // Start with one option
  //     ])
  //   });
  //   this.questions.push(questionGroup);
  // }

  // removeQuestion(index: number): void {
  //   this.questions.removeAt(index);
  // }

  // getOptions(index: number): FormArray {
  //   return (this.questions.at(index).get('options') as FormArray);
  // }

  // addOption(questionIndex: number): void {
  //   this.getOptions(questionIndex).push(new FormControl('', Validators.required));
  // }

  // removeOption(questionIndex: number, optionIndex: number): void {
  //   this.getOptions(questionIndex).removeAt(optionIndex);
  // }

  // onSubmitQuiz(): void {
  //   if (this.QuizForm.valid) {
  //     this.quizes.push(this.QuizForm.value)
  //     console.log(this.QuizForm.value);
  //   } else {
  //     console.log("Form is invalid.");
  //   }
  // }

  // trackByIndex(index: number): number {
  //   return index;
  // }
  // DeleteQuiz(id: number) {
  //   this.quizes = this.quizes.filter(quiz => quiz.id !== id);
  //   // If the deleted unit is being edited, reset the form
  //   if (this.editQuizId === id) {
  //     this.QuizForm.reset();
  //     this.editQuizId = null;
  //   }
  // }
  // UpdateQuiz(id: number) {
  //   const quizToUpdate = this.quizes.find(quiz => quiz.id === id);
  //   if (quizToUpdate) {
  //     this.QuizForm.patchValue(quizToUpdate);
  //     this.editQuizId = id;  // Set edit mode
  //   }
  // }
}
