import { EditModalReturn } from './../../../model/utils';
import { CustomerClient, CustomerDto } from './../../api/api-reference';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { GetCustomersResponse } from 'src/app/api/api-reference';

@Component({
  selector: 'app-customer-edit-dialog',
  templateUrl: './customer-edit-dialog.component.html',
  styleUrls: ['./customer-edit-dialog.component.css'],
})
export class CustomerEditDialogComponent {
  customerForm = new FormGroup({
    email: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.email]}),
  });
  constructor(
    private readonly dialogRef: MatDialogRef<CustomerEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private readonly customer: GetCustomersResponse,
    private readonly customerClient: CustomerClient,
    private readonly toastr: ToastrService
  ) {
    this.customerForm.patchValue({ email: this.customer.email ?? '' });
  }

  readonly updateCustomer = () => {
    this.customerClient
      .update(this.customer.id, new CustomerDto({ email: this.customerForm.controls.email.value }))
      .subscribe({
        next: () => {
          this.customer.email = this.customerForm.controls.email.value;
          this.toastr.success('Successfuly updated customer!');
          this.dialogRef.close();
        },
        error: () => this.toastr.error('Failed to update customer.'),
      });
  };
}
