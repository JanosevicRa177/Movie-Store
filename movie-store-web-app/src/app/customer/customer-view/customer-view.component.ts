import { EditModalReturn } from './../../../model/utils';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA, MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ToastrService } from 'ngx-toastr';
import {
  CustomerClient,
  GetCustomersResponse,
  Status,
} from 'src/app/api/api-reference';
import { CustomerEditDialogComponent } from '../customer-edit-dialog/customer-edit-dialog.component';

@Component({
  selector: 'app-customer-view',
  templateUrl: './customer-view.component.html',
  styleUrls: ['./customer-view.component.css'],
})
export class CustomerViewComponent {
  displayedColumns: string[] = ['email', 'actions'];
  customers: GetCustomersResponse[] = [];
  constructor(
    private readonly toastr: ToastrService,
    private readonly customerClient: CustomerClient,
    private readonly dialog: MatDialog
  ) {
    this.getCustomers();
  }

  readonly delete = (id: string) => {
    this.customerClient.delete(id).subscribe({
      next: () => {
        this.customers = this.customers.filter(
          (customer) => customer.id !== id
        );
        this.toastr.success('Successfuly deleted customer');
      },
      error: () => this.toastr.error('Failed to delete customer'),
    });
  };

  readonly upgrade = (id: string) => {
    this.customerClient.upgradeCustomer(id).subscribe({
      next: () => this.toastr.success('Successfuly upgraded customer'),
      error: () => this.toastr.error('Failed to upgrade customer'),
    });
  };

  readonly openEdit = (customer: GetCustomersResponse) => {
    this.dialog.open(CustomerEditDialogComponent, {
      data: customer,
    });
  };

  readonly isAdvanced = (customer: GetCustomersResponse): boolean => {
    return customer.status === Status.Advanced;
  };

  private getCustomers() {
    this.customerClient
      .getAll()
      .subscribe((customers) => (this.customers = customers));
  }
}
