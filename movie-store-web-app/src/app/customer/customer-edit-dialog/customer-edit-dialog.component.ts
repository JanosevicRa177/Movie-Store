import { CustomerClient, CustomerDto } from './../../api/api-reference';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { GetCustomersResponse } from 'src/app/api/api-reference';
import { convertStringToCamelCase } from 'src/app/util/string.converter';
import { toastError } from 'src/app/util/toastr';

@Component({
    selector: 'app-customer-edit-dialog',
    templateUrl: './customer-edit-dialog.component.html',
    styleUrls: ['./customer-edit-dialog.component.css'],
})
export class CustomerEditDialogComponent {
    customerForm = new FormGroup({
        email: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.email] }),
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
                error: (error: any) => {
                    this.setErrors(error);
                    toastError(this.toastr, error)
                },
            });
    };
    private readonly setErrors = (error: any) => {
        var customerForm = this.customerForm;
        Object.keys(error.errors).forEach(function (key) {
            var errorString = ""
            var errors: string[] = [];
            error.errors[key].forEach((error: string | undefined) => {
                errors.push(error ?? "");
            });
            errorString = errors.join(" | ")
            customerForm.get(convertStringToCamelCase(key))?.setErrors({ custom: errorString });
        });
    }
}
