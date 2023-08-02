import { Injectable } from '@angular/core';
import { Role } from '../api/api-reference';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class RoleService {
    private userRole = new BehaviorSubject<Role>(Role.Regular);
    userRole$ = this.userRole.asObservable();
    constructor() { }
    updateRole(role: Role) {
        this.userRole.next(role);
    }
}
