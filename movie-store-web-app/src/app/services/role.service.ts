import { CustomerClient, GetCurrentCustomerRoleResponse } from './../api/api-reference';
import { MsalService } from '@azure/msal-angular';
import { Injectable } from '@angular/core';
import { Role } from '../api/api-reference';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class RoleService {
    private userRole = new BehaviorSubject<Role>(Role.Regular);
    userRole$ = this.userRole.asObservable();
    constructor(private readonly authService: MsalService, private readonly customerClient: CustomerClient) {
    }
    readonly updateRole = (role: Role) => {
        this.userRole.next(role);
    }

    readonly getCustomerRole = async () => {
        const account = this.authService.instance.getActiveAccount();
        if (account == null) return
        this.customerClient.getCurrentCustomerRole().subscribe((res: GetCurrentCustomerRoleResponse) => {
            this.updateRole(res.role)
        });
    }

}
