import { Component } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { Route } from '../../../model/utils';

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
        },
        { name: 'Customers', route: '/customer/all' },
        { name: 'Movies', route: '/movie/all' },
    ];
    loginDisplay = false;
    name = "";

    constructor(private readonly authService: MsalService) {
        this.setLoginDisplay();
    }

    login() {
        this.authService.loginPopup().subscribe(_ => {
            this.setLoginDisplay()
        });
    }

    logout() {
        this.authService.logoutPopup().subscribe(_ => {
            this.setLoginDisplay()
        });
    }

    setLoginDisplay() {
        this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
        if (this.loginDisplay) {
            const account = this.authService.instance.getAllAccounts()[0];
            this.authService.instance.setActiveAccount(account);
            this.name = account.name ?? "";
        }
        else this.name = "";
    }
}
