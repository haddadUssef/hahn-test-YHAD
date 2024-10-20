import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket } from '../Models/Ticket';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  private apiUrl = `${environment.apiUrl}/api/Tickets`;

  constructor(private http: HttpClient) {}

  getTickets(pageNumber: number, pageSize: number): Observable<Ticket[]> {
    const params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    return this.http.get<Ticket[]>(this.apiUrl, { params });
  }

  getTicketById(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/${id}`);
  }

  createTicket(ticket: Ticket): Observable<Ticket> {
    return this.http.post<Ticket>(this.apiUrl, ticket);
  }

  updateTicket(id: number, ticket: Ticket): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, ticket);
  }

  deleteTicket(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
