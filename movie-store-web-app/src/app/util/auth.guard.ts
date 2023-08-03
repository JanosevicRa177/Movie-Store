import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { Role } from '../api/api-reference';
import { RoleService } from '../services/role.service';

export const authGuard: CanActivateFn = async (route: ActivatedRouteSnapshot) => {
    const authService = inject(MsalService);
    const roleService = inject(RoleService);
    let userRole;
    roleService.userRole$.subscribe(innerRole => {
        userRole = innerRole
    })
    const role = route.data["role"] as Role;
    if (role == null)
        return authService.instance.getAllAccounts().length > 0;
    return userRole == role && authService.instance.getAllAccounts().length > 0;
};
