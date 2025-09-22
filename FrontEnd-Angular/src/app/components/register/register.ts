import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { StudentService } from '../../services/student-service';
@Component({
  selector: 'app-register',
  imports:[ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  signupForm: FormGroup;
  // showPassword = false;
  genders = ['Male', 'Female'];
  ageGroups = ['Under 18', '18-25', '26-35', '36-45', '46-55', '56+'];

  constructor(private fb: FormBuilder, private router: Router,private _studentService: StudentService) {
    this.signupForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.pattern("^[A-Za-z][A-Za-z0-9_]{2,}$")]],
      lastName: ['', [Validators.required, Validators.pattern("^[A-Za-z][A-Za-z0-9_]{2,}$")]],
      email: ['', [Validators.required, Validators.pattern("^[a-zA-Z][a-zA-Z0-9_]*@gmail\\.com$")]],
      phoneNumber: ['', [Validators.required, Validators.pattern("^01[0-2,5]{1}[0-9]{8}$")]],
      gender: ['', Validators.required],
      age: ['', [Validators.required, Validators.minLength(6), Validators.minLength(30)]],
      address: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9])[\S]{8,}$/) ]]    });
  }

  // togglePasswordVisibility() {
  //   this.showPassword = !this.showPassword;
  // }

  creatingAccount() {
    if (this.signupForm.valid) {

      console.log(this.signupForm.value);

      this._studentService.addStudent(this.signupForm.value).subscribe({
        next: (data) => {
          console.log(data);

        },
        error: (err) => {
          console.error(err.error.errors);;
        }

      })
      this.router.navigateByUrl("/login");

      // console.log('Form submitted:', this.signupForm.value);
    }
  }
}

