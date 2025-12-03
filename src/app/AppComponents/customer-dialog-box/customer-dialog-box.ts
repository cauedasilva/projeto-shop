import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, Output, signal, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-customer-dialog-box',
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-dialog-box.html',
  styleUrl: './customer-dialog-box.css',
})
export class CustomerDialogBox {
  @Output() onSave = new EventEmitter<any>();
  @Output() onCancel = new EventEmitter<void>();

  @ViewChild('modal') modalElement!: ElementRef;

  visible = signal(false);

  customer = signal({
    customerId: 0,
    customerName: "",
    email: "",
    phone: "",
    registrationDate: ""
  });

  open() {
    this.resetForm();
    this.visible.set(true);
  }

  close() {
    this.visible.set(false);
  }

  onSubmit() {
    this.onSave.emit(this.customer());
    this.close();
  }

  resetForm() {
    this.customer.set({
      customerId: 0,
      customerName: "",
      email: "",
      phone: "",
      registrationDate: ""
    });
  }
}
