import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IAdmin } from '../../models/iadmin';
import { AdminService } from '../../services/admin-service';


@Component({
  selector: 'app-adding-admin',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule ],
  templateUrl: './adding-admin.html',
  styleUrl: './adding-admin.css'
})
export class AddingAdmin implements OnInit {
  admins: IAdmin[] = [];
  AdminForm: FormGroup;
  editId: number | null = null;

  constructor(private _adminService: AdminService) {
    this.AdminForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      nationalId: new FormControl('', [Validators.required, Validators.pattern("^[2-3][0-9]{13}$")]),
      phoneNumber: new FormControl('', [Validators.required, Validators.pattern('^[0-9]{10}$')]),
      email: new FormControl('', [Validators.required, Validators.pattern("^[a-zA-Z][a-zA-Z0-9_]*@gmail\\.com$")]),
      password: new FormControl('', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9])[\S]{8,}$/) ])
    });
  }

  get firstName() {
    return this.AdminForm.get('firstName');
  }

  get lastName() {
    return this.AdminForm.get('lastName');
  }

   get phoneNumber() {
    return this.AdminForm.get('phoneNumber');
  }

  get nationalId() {
    return this.AdminForm.get('nationalId');
  }

  get email() {
    return this.AdminForm.get('email');
  }

  get password() {
    return this.AdminForm.get('password');
  }

  ngOnInit(): void {
      this.loading();
    }

    loading(): void{
      this._adminService.getAllAdmins().subscribe({
        next: (data) => {
          this.admins = data;
        },
        error: (err) => {
          console.log(err);
        }

      })
    }

    Update(id: number) {

      console.log(id);
      const adminToUpdate = this.admins.find(admin => admin.id === id);
      if (adminToUpdate) {
        this.AdminForm.get('password')?.clearValidators();
        this.AdminForm.get('password')?.updateValueAndValidity();
        this.AdminForm.get('password')?.disable();
        this.AdminForm.patchValue(adminToUpdate);
        this.editId = id;  // Set edit mode
      }
    }

    Delete(id: number) {
      // this.instructors = this.instructors.filter(instructor => instructor.id !== id);
      // If the deleted instructor is being edited, reset the form
      console.log(id);

      this._adminService.deleteAdmin(id).subscribe({
            next : (data) => {

              console.log(data);
            },
            error: (err) => {
            console.error("Failed to delete Admin:", err.error.errors)
          }
      });
      this.loading();
      if (this.editId === id) {
        this.AdminForm.reset();
        this.editId = null;
      }
    }

    onSubmit() {
      if (this.AdminForm.valid) {
        const formData = this.AdminForm.value;

        if (this.editId !== null) {
          // Update existing admin
          console.log(this.editId);

           const { password, ...adminData } = formData;
          const index = this.admins.findIndex(a => a.id === this.editId);
          if (index > -1) {
            this._adminService.updateAdmin(this.editId, adminData as IAdmin).subscribe({
              next: (data)=>{
                console.log(data);

              },
              error: (err) => {
                console.error("Failed to update Admin:", err.error.errors)
              }
            })
           this.loading();
          }
          this.editId = null; // Exit edit mode
        } else {
          // Add new admin
          console.log(formData);

          this._adminService.addAdmin(formData).subscribe({
            next : (data) => {

              console.log(data);
            },
            error: (err) => {
            console.log(err.error.errors)
          }
          })
        }
        this.loading();

        this.AdminForm.reset();
      } else {
        console.log('Form is invalid');
      }
    }


}
