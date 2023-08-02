import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './util/auth.guard';
import { Role } from './api/api-reference';

const routes: Routes = [
    {
        path: 'movie',
        loadChildren: () =>
            import('./movie/movie-routing.module').then((x) => x.MovieRoutingModule),
    },
    {
        path: 'customer',
        loadChildren: () =>
            import('./customer/customer-routing.module').then(
                (x) => x.CustomerRoutingModule
            ),
        canActivate: [authGuard],
        data: {
            role: Role.Administrator
        },
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }
