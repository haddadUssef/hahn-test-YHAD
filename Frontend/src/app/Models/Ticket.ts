export interface Ticket {
  ticketId: number;
  description: string;
  status: number; // Assuming status is either 'Open' or 'Closed'
  dateCreated: Date;
}
