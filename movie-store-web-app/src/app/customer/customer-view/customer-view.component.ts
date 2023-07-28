import { Component, OnInit } from '@angular/core';
import {
  CustomerClient,
  GetCustomersResponse,
} from 'src/app/api/api-reference';

@Component({
  selector: 'app-customer-view',
  templateUrl: './customer-view.component.html',
  styleUrls: ['./customer-view.component.css'],
})
export class CustomerViewComponent {
  displayedColumns: string[] = ['id', 'email'];
  customers: GetCustomersResponse[] = [];
  constructor(customerClient: CustomerClient) {
    customerClient
      .getAll()
      .subscribe((customers) => (this.customers = customers));
  }
}
