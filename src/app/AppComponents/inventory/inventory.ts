import { Component, inject, ViewChild, OnInit, NgZone } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { DialogBox } from '../../AppComponents/dialog-box/dialog-box';

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [FormsModule, CommonModule, DialogBox],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css',
})
export class Inventory implements OnInit {

  httpClient = inject(HttpClient);
  ngZone = inject(NgZone);

  @ViewChild('confirmDialog') confirmDialog!: DialogBox;

  productIdToDelete: number = 0;
  disableProductIdInput: boolean = false;

  inventoryData = {
    productId: 0,
    productName: "",
    availableQuantity: 0,
    reorderPoint: 0
  };

  inventoryDetails: any[] = [];

  ngOnInit() {
    this.ngZone.run(()=>this.getInventoryDetails());
  }

  getInventoryDetails(): void {
    const apiUrl = "https://localhost:7259/api/inventory";

    this.httpClient.get<any[]>(apiUrl).subscribe(data => {
      this.inventoryDetails = data;
      console.log(this.inventoryDetails);
    });

    this.inventoryData = {
      productId: 0,
      productName: "",
      availableQuantity: 0,
      reorderPoint: 0
    };

    this.disableProductIdInput = false;
  }

  onSubmit(): void {
    const apiUrl = "https://localhost:7259/api/inventory";
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'my-auth-token',
        'Content-Type': 'application/json'
      })
    };

    if (this.disableProductIdInput) {
      this.httpClient.put(apiUrl, this.inventoryData, httpOptions).subscribe({
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
          alert("Submitted: " + JSON.stringify(this.inventoryData));
          this.getInventoryDetails();
        }
      });
    } else {
      this.httpClient.post(apiUrl, this.inventoryData, httpOptions).subscribe({
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
          alert("Submitted: " + JSON.stringify(this.inventoryData));
          this.getInventoryDetails();
        }
      });
    }
  }

  openConfirmDialog(productId: number) {
    this.productIdToDelete = productId;
    this.confirmDialog.open("Confirmar exclusão", "Deletar este item?");
  }

  handleDelete(confirmed: boolean) {
    if (confirmed) {
      this.deleteInventory();
    }
  }

  deleteInventory() {
    let apiUrl = `https://localhost:7259/api/inventory/${this.productIdToDelete}`;

    this.httpClient.delete(apiUrl).subscribe(data => {
      this.getInventoryDetails();
    });
  }

  populateFormForEdit(inventory: any) {
    this.inventoryData.productId = inventory.productId;
    this.inventoryData.productName = inventory.productName;
    this.inventoryData.availableQuantity = inventory.availableQuantity;
    this.inventoryData.reorderPoint = inventory.reorderPoint;

    this.disableProductIdInput = true;
  }
}
