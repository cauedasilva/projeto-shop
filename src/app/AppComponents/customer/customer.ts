import { Component, inject, ViewChild } from '@angular/core';
import { CustomerDialogBox } from '../customer-dialog-box/customer-dialog-box';


@Component({
  selector: 'app-customer',
  imports: [CustomerDialogBox],
  templateUrl: './customer.html',
  styleUrl: './customer.css',
})

export class Customer {

  @ViewChild('customerDialogBox') customerDialogBox!: CustomerDialogBox;

  openCustomerDialog() {
    this.customerDialogBox.open();
  }

  addCustomer(newCustomer: any) {
    console.log("Customer recebido:", newCustomer);
  }
}
