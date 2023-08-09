
import { CustomerClient, Role } from './../../api/api-reference';
import { Component } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { Route } from '../../../model/utils';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/services/role.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
    routes: Route[] = [
        {
            name: 'Home',
            route: '/',
            protected: false
        },
        { name: 'Customers', route: '/customer/all', protected: true, role: Role.Administrator },
        { name: 'Movies', route: '/movie/all', protected: true },
    ];
    loginDisplay = false;
    role: Role = Role.Regular;
    name = "";

    constructor(private readonly authService: MsalService,
        private readonly toastr: ToastrService,
        private readonly router: Router,
        private readonly customerClient: CustomerClient,
        private readonly roleService: RoleService) {
        this.roleService.userRole$.subscribe(innerRole => {
            this.role = innerRole
        })
        this.setupCurrentCustomer()
    }

    readonly login = () => {
        this.authService.loginPopup().subscribe({
            next: () => {
                this.toastr.success("Successfully logged in!")
                this.setupCurrentCustomer()
            },
            error: () => {
                this.toastr.error("Login failed!")
            },
        });
    }

    readonly logout = () => {
        this.authService.logoutPopup().subscribe(_ => {
            this.router.navigate(['/']);
            this.setLoginDisplay()
        });
    }

    readonly setupCurrentCustomer = () => {
        this.roleService.getCustomerRole().then(() => {
            this.setLoginDisplay();
        });
    }

    readonly setLoginDisplay = () => {
        this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
        if (this.loginDisplay) {
            const account = this.authService.instance.getAllAccounts()[0];
            this.authService.instance.setActiveAccount(account);
            this.name = account.name ?? "";
        }
        else this.name = "";
    }

    readonly shouldShow = (route: Route): boolean => {
        const roleValid = route.role == null || route.role == this.role;
        return roleValid && (!route.protected || this.loginDisplay)
    }

}
