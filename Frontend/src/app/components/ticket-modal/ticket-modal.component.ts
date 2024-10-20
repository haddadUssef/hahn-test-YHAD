import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Ticket } from '../../Models/Ticket';
import {
  FormBuilder,
  FormsModule,
  Validators,
  FormGroup,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzNotificationService } from 'ng-zorro-antd/notification';
@Component({
  selector: 'app-ticket-modal',
  templateUrl: './ticket-modal.component.html',
  styleUrls: ['./ticket-modal.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzModalModule,
    NzButtonModule,
    NzSelectModule,
  ],
})
export class TicketModalComponent implements OnInit {
  validateForm!: FormGroup;
  @Input() ticket: Ticket = {
    ticketId: 0,
    description: '',
    status: 0,
    dateCreated: new Date(),
  };
  @Output() save = new EventEmitter<Ticket>();
  @Output() cancel = new EventEmitter<void>();
  @Input() isModalVisible = false;
  @Input() modalTitle: string = 'TITLE NOT SPECIFIED !';

  constructor(
    private fb: FormBuilder,
    private notification: NzNotificationService
  ) {}

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      description: ['', [Validators.required]],
      status: ['', [Validators.required]],
    });
  }
  handleOk(): void {
    if (this.validateForm.valid) {
      this.notification.success('Success', 'Form is valid and submitted');
      this.save.emit(this.validateForm.value);
    } else {
      this.notification.error('Error', 'Please fill in all required fields');
      Object.values(this.validateForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity();
        }
      });
    }
  }

  handleCancel(): void {
    this.cancel.emit();
  }
}
