import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { Details } from './components/details/details';
import { NotFound } from './components/not-found/not-found';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { Profile } from './components/profile/profile';
import { Info } from './components/info/info';
import { Cart } from './components/cart/cart';
import { Payment } from './components/payment/payment';
import { Admin } from './components/admin/admin';
import { AddingInstructor } from './components/adding-instructor/adding-instructor';
import { AddingClassTrack } from './components/adding-class-track/adding-class-track';
import { AddingAdmin } from './components/adding-admin/adding-admin';
import { AddingSubject } from './components/adding-subject/adding-subject';
import { Instructor } from './components/instructor/instructor';
import { CourseDetails } from './components/course-details/course-details';
import { CourseDashboard } from './components/course-dashboard/course-dashboard';
import { Chat } from './components/chat/chat';
import { AddingUnitAndLesson } from './components/adding-unit-and-lesson/adding-unit-and-lesson';
import { LiveMeeting } from './components/live-meeting/live-meeting';
import { adminAuthGuardGuard } from './guards/admin-auth-guard-guard';
import { studentAuthGuardGuard } from './guards/student-auth-guard-guard';
import { instructorAuthGuardGuard } from './guards/instructor-auth-guard-guard';
import { instructorOrStudentAuthGuardGuard } from './guards/instructor-or-student-auth-guard-guard';

export const routes: Routes = [
  {path:'', redirectTo: 'home', pathMatch:'full', title: 'Home'},
  {path:'home', component: Home, title: 'Home'},
  {path:'payment', component: Payment, title: 'Payment',canActivate:[studentAuthGuardGuard] },
  {path:'meeting', component: LiveMeeting, title: 'Meeting', canActivate:[instructorOrStudentAuthGuardGuard]},
  {path:'chat', component: Chat, title: 'Chat',canActivate:[instructorOrStudentAuthGuardGuard]},
  {path:'instructor', component: Instructor, title: 'Instructor', canActivate:[instructorAuthGuardGuard]},
  {path:'profile', component: Profile, title: 'Profile',
    children: [
      {path:'', redirectTo: 'info', pathMatch:'full', title: 'Info'},
      {path:'info', component: Info, title: 'Info'},
      {path:'cart', component: Cart, title: 'Subjects'},
      {path:'**', component: NotFound, title: 'Not Found'}
    ]
  },
  {path:'login', component: Login, title: 'Login'},

  {path:'register', component: Register, title: 'Register'},
  {path:'details/:id', component: Details, title: 'Details'},
  {path:'admin', component: Admin, title: 'Admin',canActivate: [adminAuthGuardGuard],
    children: [
      {path:'', redirectTo: 'addingAdmin', pathMatch:'full', title: 'Add Admin'},
      {path:'addingAdmin', component: AddingAdmin, title: 'Add Admin',canActivate:[adminAuthGuardGuard]},
      {path:'addingInstructor', component: AddingInstructor, title: 'Add Instructor',canActivate:[adminAuthGuardGuard]},
      {path:'addingClassTrack', component: AddingClassTrack, title: 'Add Class',canActivate:[adminAuthGuardGuard]},
      {path:'addingSubject', component: AddingSubject, title: 'Add Subject',canActivate:[adminAuthGuardGuard]},
      {path:'**', component: NotFound, title: 'Not Found'}
    ]
  },
  {path: 'courseDashboard/:id', component: CourseDashboard, title: 'Course',canActivate:[instructorOrStudentAuthGuardGuard] },
  {path:'**', component: NotFound, title: 'Not Found'},
];
