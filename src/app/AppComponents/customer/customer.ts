import { Component, inject, ViewChild, OnInit, NgZone } from '@angular/core';
import { CustomerDialogBox } from '../customer-dialog-box/customer-dialog-box';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { DialogBox } from '../dialog-box/dialog-box';


@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [CustomerDialogBox, CommonModule, DialogBox],
  templateUrl: './customer.html',
  styleUrl: './customer.css',
})

export class Customer implements OnInit {

  customerIdToDelete: number = 0;

  httpClient = inject(HttpClient);
  ngZone = inject(NgZone);

  customerData = {
    customerId: 0,
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    registrationDate: ""
  };

  customerDetails: any[] = [];

  @ViewChild('customerDialogBox') customerDialogBox!: CustomerDialogBox;
  @ViewChild('confirmDialog') confirmDialog!: DialogBox;

  openCustomerDialog() {
    this.customerDialogBox.open(null);
    this.getCustomerDetails();
  }

  addCustomer(newCustomer: any) {
    console.log("Customer recebido:", newCustomer);
  }
  ngOnInit() {
    this.ngZone.run(() => this.getCustomerDetails());
  }

  openConfirmDialog(customerId: number) {
    this.customerIdToDelete = customerId;
    this.confirmDialog.open("Confirmar exclusão", "Deletar este item?");
  }

  handleDelete(confirmed: boolean) {
    if (confirmed) {
      this.deleteCustomer();
    }
  }

  deleteCustomer() {
    let apiUrl = `https://localhost:7259/api/customer/${this.customerIdToDelete}`;

    this.httpClient.delete(apiUrl).subscribe(data => {
      this.getCustomerDetails();
    });
  }

  openCustomerEditDialogBox(customer: any) {
    this.customerData.customerId = customer.customerId;
    this.customerData.firstName = customer.firstName;
    this.customerData.lastName = customer.lastName;
    this.customerData.email = customer.email;
    this.customerData.phone = customer.phone;
    this.customerData.registrationDate = customer.registrationDate;

    this.customerDialogBox.open(this.customerData);
  }

  getCustomerDetails() {
    const apiUrl = "https://localhost:7259/api/customer";

    this.httpClient.get(apiUrl).subscribe(result => {
      console.log("Loaded customers:", result);
      this.customerDetails = [...(result as any[])];
    })
  }
}
