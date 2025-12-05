import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, ElementRef, EventEmitter, inject, Input, Output, signal, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-customer-dialog-box',
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-dialog-box.html',
  styleUrl: './customer-dialog-box.css',
})
export class CustomerDialogBox {
  httpClient = inject(HttpClient);

  @Output() onSave = new EventEmitter<any>();
  @Output() onCancel = new EventEmitter<void>();

  @Input() private customer: any;

  @ViewChild('modal') modalElement!: ElementRef;

  disableCustomerIdInput: boolean = false;

  visible = signal(false);

  customerDetails = signal({
    customerId: 0,
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    registrationDate: ""
  });

  open(customer: any) {
    if (customer != null) {
      this.disableCustomerIdInput = true;

      this.customerDetails.set({
        ...this.customerDetails(),
        customerId: customer.customerId,
        firstName: customer.firstName,
        lastName: customer.lastName,
        email: customer.email,
        phone: customer.phone,
        registrationDate: customer.registrationDate
      });
    } else {
      this.disableCustomerIdInput = false;
      this.resetForm();
    }
    this.visible.set(true);
  }

  close() {
    this.visible.set(false);
  }

  onSubmit() {
    const apiUrl = "https://localhost:7259/api/customer";

    const payload = {
      ...this.customerDetails(),
      registrationDate: new Date(this.customerDetails().registrationDate).toISOString()
    };

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    if (this.disableCustomerIdInput) {
      this.httpClient.put(apiUrl, payload, httpOptions).subscribe({
        next: v => console.log(v),
        error: e => {
          if (e.status === 409) {
            alert("Erro: ID já existe!");
          } else {
            alert("Erro ao enviar!");
          }
          console.error(e)
        },
        complete: () => {
          alert("Customer saved successfully");
        }
      });
    } else {
      this.httpClient.post(apiUrl, payload, httpOptions).subscribe({
        next: v => console.log(v),
        error: e =>{
          if (e.status === 409) {
            alert("Erro: ID já existe!");
          } else {
            alert("Erro ao enviar!");
          }
          console.error(e)
        },
        complete: () => {
          alert("Customer saved successfully");
        }
      });
    }

    this.onSave.emit(payload);
    this.close();
  }


  resetForm() {
    this.customerDetails.set({
      customerId: 0,
      firstName: "",
      lastName: "",
      email: "",
      phone: "",
      registrationDate: ""
    });
  }
}
