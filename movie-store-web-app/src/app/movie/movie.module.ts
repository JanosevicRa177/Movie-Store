import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieViewComponent } from './movie-view/movie-view.component';
import { AppRoutingModule } from '../app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MovieNewEditViewComponent } from './movie-new-edit-view/movie-new-edit-view.component';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
    declarations: [MovieViewComponent, MovieNewEditViewComponent],
    imports: [
        CommonModule,
        MatButtonModule,
        AppRoutingModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
        MatFormFieldModule,
        MatPaginatorModule,
        MatSelectModule,
        FormsModule
    ],
})
export class MovieModule { }
