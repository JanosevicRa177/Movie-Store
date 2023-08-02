import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MovieViewComponent } from './movie-view/movie-view.component';
import { MovieNewEditViewComponent } from './movie-new-edit-view/movie-new-edit-view.component';
import { authGuard } from '../util/auth.guard';
import { Role } from '../api/api-reference';

const routes: Routes = [
    { path: 'all', component: MovieViewComponent },
    {
        path: 'edit/:id', component: MovieNewEditViewComponent,
        canActivate: [authGuard],
        data: {
            role: Role.Administrator
        },
    },
    {
        path: 'create', component: MovieNewEditViewComponent,
        canActivate: [authGuard],
        data: {
            role: Role.Administrator
        },
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes), CommonModule],
    exports: [RouterModule],
})
export class MovieRoutingModule { }
