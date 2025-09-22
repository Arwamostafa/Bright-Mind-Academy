import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-payment',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './payment.html',
  styleUrl: './payment.css'
})
export class Payment {
  paymentForm: FormGroup;
  countries = ['Egypt', 'Saudi Arabia', 'United Arab Emirates', 'Kuwait', 'Bahrain', 'Oman', 'Qatar', 'Jordan', 'Lebanon', 'Iraq', 'Syria'];

  constructor(private fb: FormBuilder, private router: Router) {
    this.paymentForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      cardNumber: ['', [Validators.required]],
      expiration: ['', [Validators.required]],
      cvc: ['', [Validators.required]],
      country: ['', Validators.required],
      // searchQuery: ['']
    });
  }

  formatCardNumber(event: any) {
    let value = event.target.value.replace(/\s+/g, '').replace(/[^0-9]/gi, '');
    if (value.length > 16) {
      value = value.substring(0, 16);
    }
    const formatted = value.replace(/(\d{4})/g, '$1 ').trim();
    this.paymentForm.get('cardNumber')?.setValue(formatted, { emitEvent: false });
    event.target.value = formatted;
  }

  formatExpiration(event: any) {
    let value = event.target.value.replace(/\D/g, '');
    if (value.length > 2) {
      value = value.substring(0, 2) + '/' + value.substring(2, 4);
    }
    this.paymentForm.get('expiration')?.setValue(value, { emitEvent: false });
    event.target.value = value;
  }

  onSubmit() {
    if (this.paymentForm.valid) {
      this.router.navigateByUrl("/home");
      console.log('Payment submitted:', this.paymentForm.value);

    }
  }

}
