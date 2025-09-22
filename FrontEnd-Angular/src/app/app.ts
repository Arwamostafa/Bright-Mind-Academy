import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './components/navbar/navbar';
import { Home } from './components/home/home';
import { Footer } from './components/footer/footer';
import { Details } from './components/details/details';
import { NotFound } from './components/not-found/not-found';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { Profile } from './components/profile/profile';
import { Info } from './components/info/info';
import { Cart } from './components/cart/cart';
import { Payment } from './components/payment/payment';
import { CommonModule } from '@angular/common';
import { Admin } from './components/admin/admin';
import { Instructor } from './components/instructor/instructor';
import { AddingInstructor } from './components/adding-instructor/adding-instructor';
import { AddingClassTrack } from './components/adding-class-track/adding-class-track';
import { AddingAdmin } from './components/adding-admin/adding-admin';
import { AddingSubject } from './components/adding-subject/adding-subject';
import { Assigment } from './components/assigment/assigment';
import { CourseDashboard } from './components/course-dashboard/course-dashboard';
import { CourseDetails } from './components/course-details/course-details';
import { Quiz } from './components/quiz/quiz';
import { Sidebar } from './components/sidebar/sidebar';
import { Chat } from './components/chat/chat';
import { AddingUnitAndLesson } from './components/adding-unit-and-lesson/adding-unit-and-lesson';
import { LiveMeeting } from './components/live-meeting/live-meeting';
import { Chatbotspesefic } from './components/chatbotspesefic/chatbotspesefic';

@Component({
  selector: 'app-root',
  imports: [CommonModule,RouterOutlet, Chatbotspesefic,LiveMeeting , Navbar, Home, Footer, Details, NotFound, Login, Register, Payment, Profile, Info, Cart, Admin, AddingInstructor, AddingClassTrack, AddingAdmin, AddingSubject, Instructor, Assigment, CourseDashboard, CourseDetails, Quiz, Sidebar, Chat, AddingUnitAndLesson],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'E_Learning';

  // page: 'units' | 'lessons' = 'units';

  // setPage(newPage: 'units' | 'lessons') {
  //   this.page = newPage;
  // }
}

