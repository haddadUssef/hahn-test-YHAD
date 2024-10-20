import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Ticket } from './Models/Ticket';
import { TicketService } from './services/ticket-service.service';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { FormsModule } from '@angular/forms';
import { TicketModalComponent } from './components/ticket-modal/ticket-modal.component';
import {
  NzNotificationModule,
  NzNotificationService,
} from 'ng-zorro-antd/notification';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { LoaderComponent } from './components/loader/loader.component';

@Component({
  selector: 'app-root',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    NzTableModule,
    NzTagModule,
    NzButtonModule,
    NzPaginationModule,
    NzPopconfirmModule,
    NzModalModule,
    FormsModule,
    TicketModalComponent,
    NzNotificationModule,
    NzIconModule,
    LoaderComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'ticket-management-ui';
  isVisible = false;
  tickets: Ticket[] = [];
  modalTitle: string = 'TITLE NOT SPECIFIED !';
  displayData: Ticket[] = [];
  selectedTicket: Ticket = {
    ticketId: 0,
    description: '',
    status: 0,
    dateCreated: new Date(),
  };
  statusFilterOptions = [
    { text: 'Open', value: 0 },
    { text: 'Closed', value: 1 },
  ];
  currentPage = 1;
  pageSize = 5;

  constructor(
    private ticketService: TicketService,
    private notificationService: NzNotificationService
  ) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  editTicket(ticket: Ticket) {
    this.selectedTicket = { ...ticket };
    this.showModal(this.selectedTicket);
  }

  loadTickets(): void {
    this.ticketService.getTickets(this.currentPage, this.pageSize).subscribe(
      (data) => {
        this.tickets = data;
        this.displayData = [...this.tickets];
      },
      (error) => {
        this.notificationService.error('Error', error.message);
      }
    );
  }

  deleteTicket(ticketId: number) {
    return new Promise((resolve) => {
      this.ticketService.deleteTicket(ticketId).subscribe(() => {
        this.loadTickets();
        resolve(true);
      });
    });
  }

  showModal(ticket: Ticket): void {
    this.selectedTicket = { ...ticket };
    this.isVisible = true;
    this.modalTitle = ticket.ticketId === 0 ? 'Add New Ticket' : 'Edit Ticket';
  }

  handleSave(ticket: Ticket): void {
    if (ticket.ticketId === 0) {
      this.ticketService.createTicket(ticket).subscribe(
        (newTicket) => {
          this.notificationService.success(
            'Success',
            'Ticket saved successfully'
          );
          this.loadTickets();
        },
        (error) => {
          this.notificationService.error('Error', error.message);
        }
      );
    } else {
      this.ticketService.updateTicket(ticket.ticketId, ticket).subscribe(
        () => {
          this.notificationService.success(
            'Success',
            'Ticket updated successfully'
          );
          this.loadTickets();
        },
        (error) => {
          this.notificationService.error('Error', error.message);
        }
      );
    }
    this.isVisible = false;
    this.selectedTicket = {
      ticketId: 0,
      description: '',
      status: 0,
      dateCreated: new Date(),
    };
  }

  handleCancel(): void {
    this.isVisible = false;
    this.selectedTicket = {
      ticketId: 0,
      description: '',
      status: 0,
      dateCreated: new Date(),
    };
    this.loadTickets();
  }

  filterStatus(filter: number[]): void {
    if (filter.length === 0) {
      // Reset to show all tickets if no filter is applied
      this.displayData = [...this.tickets];
    } else {
      // Filter the tickets based on the selected status
      this.displayData = this.tickets.filter((ticket) =>
        filter.includes(ticket.status)
      );
    }
  }

  sortDate = (a: Ticket, b: Ticket) =>
    new Date(a.dateCreated).getTime() - new Date(b.dateCreated).getTime();

  goToPage(page: number): void {
    this.currentPage = page;
    this.loadTickets();
  }
}
