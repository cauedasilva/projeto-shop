import { Component, inject } from '@angular/core';
import { ElementRef, EventEmitter, Output, ViewChild, AfterViewInit } from '@angular/core';
declare const bootstrap: any;

@Component({
  selector: 'app-dialog-box',
  imports: [],
  templateUrl: './dialog-box.html',
  styleUrl: './dialog-box.css',
})

export class DialogBox implements AfterViewInit {

  @ViewChild('modal') modalElement!: ElementRef;

  @Output() onClose = new EventEmitter<boolean>();

  title = 'Confirm';
  message = 'Are you sure?';

  private modalInstance: any;

  ngAfterViewInit() {
    this.modalInstance = new bootstrap.Modal(this.modalElement.nativeElement);
  }

  open(title: string, message: string) {
    this.title = title;
    this.message = message;
    this.modalInstance.show();
  }

  confirm() {
    this.modalInstance.hide();
    this.onClose.emit(true);
  }

  cancel() {
    this.modalInstance.hide();
    this.onClose.emit(false);
  }
}
