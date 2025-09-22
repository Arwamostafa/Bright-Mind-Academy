import { Component, Input, OnInit, Output } from '@angular/core';
import { Sidebar } from "../sidebar/sidebar";
import { CourseDetails } from '../course-details/course-details';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-course-dashboard',
  imports: [Sidebar, CourseDetails],
  templateUrl: './course-dashboard.html',
  styleUrl: './course-dashboard.css'
})
export class CourseDashboard implements OnInit {
subjectId!: number;
  selectedLessonId!: number;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.subjectId = Number(this.route.snapshot.paramMap.get('id'));
  }

  onLessonSelected(lessonId: number) {
    this.selectedLessonId = lessonId;
  }

}
