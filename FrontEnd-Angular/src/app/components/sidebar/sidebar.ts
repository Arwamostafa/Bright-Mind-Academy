import { routes } from './../../app.routes';
import { SubjectService } from './../../services/subject-service';
import { LessonService } from './../../services/lesson-service';
import { UnitService } from './../../services/unit-service';
import { Component, OnInit, Output, EventEmitter, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { IISubject } from '../../models/isubject';
import { firstValueFrom } from 'rxjs';
import { Ilesson } from '../../models/ilesson';
import { ISection, IUnit } from '../../models/iunit';

@Component({
  selector: 'app-sidebar',
  imports: [CommonModule, RouterLink],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar implements OnInit , OnChanges {
 courseTitle = '';
  currentLesson: number | null = null;
  isSidebarOpen = false;
@Input() subjectId!: number;
  @Output() lessonSelected = new EventEmitter<number>();

  sections: ISection[] = [];
  currentLessonId!: number;
lessons:Ilesson[]=[]

  subject: IISubject | undefined;
units:IUnit[]=[]
unit:IUnit|null=null
  constructor(
    private subjectService: SubjectService,
    private unitService: UnitService,
    private lessonService: LessonService
  ) {}

  defultlesson:number|null=0;

  ngOnInit(): void {
    this.unitService.getUnitsBySubjectId(this.subjectId).subscribe({
    next: (data) => {
      this.units = data;
    },
    error: (err) => {
      console.error('Error fetching units:', err);
    }
  });
  this.lessonService.getAll().subscribe({
    next:(lessons) =>{
   this.defultlesson=lessons[0].id
    }
  })
    console.log(this.subjectId)
    if (this.subjectId) {
      this.loadData();
    }

  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['subjectId'] && this.subjectId) {
      this.loadData();
    }
  }

async loadData() {
  try {
    this.subject = await firstValueFrom(this.subjectService.getSubjectById(this.subjectId));
    this.units = await firstValueFrom(this.unitService.getUnitsBySubjectId(this.subjectId));

    let defaultLessonId: number | null = null;

    this.sections = await Promise.all(
      this.units.map(async (unit: IUnit) => {
        const lessons = await firstValueFrom(this.lessonService.getByUnitId(unit.id));

        if (!defaultLessonId && lessons.length > 0) {
          defaultLessonId = lessons[0].id;
        }

        return {
          unitName: unit.title,
          lessons: lessons.map((lesson, index) => ({
            id: lesson.id,
            number: index + 1,
            title: lesson.title
          }))
        } as ISection;
      })
    );

    // بعد ما نخلص، لو فيه defaultLessonId نعمل emit
    if (defaultLessonId) {
      this.currentLesson = defaultLessonId;
      this.lessonSelected.emit(defaultLessonId);
    }

  } catch (error) {
    console.error('Error loading sidebar data', error);
  }
}

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  selectLesson(lessonId: number): void {
    this.currentLesson = lessonId;
    this.lessonSelected.emit(lessonId);
  }
}
