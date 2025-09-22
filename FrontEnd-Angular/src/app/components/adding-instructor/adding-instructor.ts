import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IInstructor } from '../../models/iinstructor';
import { InstructorService } from '../../services/instructor-service';
import JSZip from 'jszip';
export function passwordValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;

  if (!value) {
    return null; // Let required validator handle empty values
  }

  const hasNumber = /[0-9]/.test(value);
  const hasNonAlphanumeric = /[^a-zA-Z0-9]/.test(value);
  const isValidLength = value.length >= 6;

  const errors: ValidationErrors = {};

  if (!isValidLength) {
    errors['minlength'] = true;
  }
  if (!hasNumber) {
    errors['requiresDigit'] = true;
  }
  if (!hasNonAlphanumeric) {
    errors['requiresSpecialChar'] = true;
  }

  return Object.keys(errors).length > 0 ? errors : null;
}
@Component({
  selector: 'app-adding-instructor',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './adding-instructor.html',
  styleUrl: './adding-instructor.css'
})

export class AddingInstructor implements OnInit {
  InstructorForm: FormGroup;
  instructors: IInstructor[] = [];
  editId: number | null = null;
invalidimageType = false;
image: File | null = null;
oldImageUrl: string  = '';

  constructor(private _instructorService : InstructorService) {
    this.InstructorForm = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      lastName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      address: new FormControl('', [Validators.required, Validators.minLength(10)]),
      phoneNumber: new FormControl('', [Validators.required, Validators.pattern('^[0-9]{10}$')]),
      gender: new FormControl('', [Validators.required]),
      image:new FormControl<File | null>(null),
    });
  }

  get firstName() { return this.InstructorForm.get('firstName'); }
  get lastName() { return this.InstructorForm.get('lastName'); }
  get email() { return this.InstructorForm.get('email'); }
  get password() { return this.InstructorForm.get('password'); }
  get address() { return this.InstructorForm.get('address'); }
  get gender() { return this.InstructorForm.get('gender'); }
  get imageget() { return this.InstructorForm.get('image'); }
  get phoneNumber() { return this.InstructorForm.get('phoneNumber'); }

  ngOnInit(): void {
    if (this.editId) {
    this.InstructorForm.get('image')?.clearValidators();
  } else {
    // الحالة Add → required
    this.InstructorForm.get('image')?.setValidators([Validators.required]);
  }

  this.InstructorForm.get('image')?.updateValueAndValidity();

    this._instructorService.getAllInstructors().subscribe({
      next: (data) => {
        this.instructors = data;

      },
      error: (err) => {
        console.log(err.err.errors);
      }

    })
  }

  Update(id: number) {

    console.log(id);
    const instructorToUpdate = this.instructors.find(instructor => instructor.id === id);
    if (instructorToUpdate) {
      this.InstructorForm.get('password')?.clearValidators();
      this.InstructorForm.get('password')?.updateValueAndValidity();
      this.InstructorForm.get('password')?.disable();
      // this.oldImageUrl = instructorToUpdate.image || null;
      this.InstructorForm.patchValue({ ...instructorToUpdate, image:  null });
    this.oldImageUrl = instructorToUpdate.ImageURL?? '';


      this.editId = id;
    }
  }

  Delete(id: number) {
    // this.instructors = this.instructors.filter(instructor => instructor.id !== id);
    // If the deleted instructor is being edited, reset the form
    console.log(id);

    this._instructorService.deleteInstructor(id).subscribe({
          next : (data) => {

            console.log(data);
          },
          error: (err) => {
          console.error("Failed to delete Instructor:", err.error.errors)
        }
    });
    this.ngOnInit();
    if (this.editId === id) {
      this.InstructorForm.reset();
      this.editId = null;
    }
  }

  onSubmit() {
    if (this.InstructorForm.valid) {
    const instructor: IInstructor = {
      id: this.editId ?? 0,
      firstName: this.InstructorForm.value.firstName,
      lastName: this.InstructorForm.value.lastName,
      email: this.InstructorForm.value.email,
      phoneNumber: this.InstructorForm.value.phoneNumber,
      address: this.InstructorForm.value.address,
      gender: this.InstructorForm.value.gender,
      password: this.editId === null ? this.InstructorForm.value.password : '',
      Image: this.image || undefined,
    };

    if (this.editId !== null) {
      console.log('Updating instructor:', this.editId);

      this._instructorService.updateInstructor(this.editId, instructor).subscribe({
        next: (data) => {
          console.log("Updated:", data);
          this.InstructorForm.reset();
          this.ngOnInit(); // refresh data
        },
        error: (err) => {
          console.error("Failed to update instructor:", err);
          this.handleFormErrors(err);
        }
      });
    } else {
      this._instructorService.addInstructor(instructor).subscribe({
        next: (data) => {
          console.log("Added:", data);
          this.InstructorForm.reset();
          this.ngOnInit();
        },
        error: (err) => {
          console.error("Failed to add instructor:", err);
          this.handleFormErrors(err);
        }
      });
    }
  } else {
    console.log('Form is invalid');
    Object.keys(this.InstructorForm.controls).forEach(key => {
      const control = this.InstructorForm.get(key);
      if (control?.invalid) {
        control.markAsTouched();
      }
    });
  }
  }

private handleFormErrors(err: any): void {
    if (err.status === 400 && err.error.errors) {
      if (Array.isArray(err.error.errors)) {
        err.error.errors.forEach((error: any) => {
          // console.log(Field: `${error.Field}`, Error: `${error.Error});
                      this.handleFormErrors(err);

        });
      } else {
        console.log('Validation errors:', err.error.errors);
      }
    }
  }
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


  validateVideoFile(event: Event): void {
  const input = event.target as HTMLInputElement;

  if (input.files && input.files.length > 0) {
    const file = input.files[0];
    const isimage = file.type.startsWith('image/');

    this.invalidimageType = !isimage;
  }
}


async onImageFileChange(event: Event): Promise<void> {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];

    if (file) {
      console.log('Image file selected:', file.name, file.size, file.type);

      const isImage = file.type.startsWith('image/');
      this.invalidimageType = !isImage;

      if (!this.invalidimageType) {
        try {
          const zipFile = await this.compressFileToZip(file, `image_${Date.now()}.zip`);
          this.image = new File([zipFile], `image_${Date.now()}.zip`, { type: 'application/zip' });
          console.log('Image file compressed successfully:', this.image.size);
        } catch (error) {
          console.error('Error compressing image file:', error);
          this.invalidimageType = true;
          this.image = null;
        }
      } else {
        console.error('Invalid image file type:', file.type);
        this.image = null;
        // Clear the input
        input.value = '';
      }
    } else {
      this.image = null;
      this.invalidimageType = false;
    }
  }
onImageError(event: any): void {
  console.error('Image failed to load:', event.target.src);
  event.target.style.display = 'none';

}
}
