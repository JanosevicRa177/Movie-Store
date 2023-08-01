import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieViewComponent } from './movie-view/movie-view.component';
import { AppRoutingModule } from '../app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MovieNewEditViewComponent } from './movie-new-edit-view/movie-new-edit-view.component';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@NgModule({
    declarations: [MovieViewComponent, MovieNewEditViewComponent],
    imports: [
        CommonModule,
        MatButtonModule,
        AppRoutingModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
    ],
})
export class MovieModule { }
