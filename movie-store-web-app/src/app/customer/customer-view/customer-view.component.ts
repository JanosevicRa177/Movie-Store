import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
    CustomerClient,
    GetCustomersResponse,
    Status,
} from 'src/app/api/api-reference';
import { CustomerEditDialogComponent } from '../customer-edit-dialog/customer-edit-dialog.component';
import { MatDialog } from '@angular/material/dialog';
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
        private readonly dialog: MatDialog,
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

    readonly upgrade = (customer: GetCustomersResponse) => {
        this.customerClient.upgradeCustomer(customer.id).subscribe({
            next: () => {
                this.toastr.success('Successfuly upgraded customer')
                customer.status = Status.Advanced
            },
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
