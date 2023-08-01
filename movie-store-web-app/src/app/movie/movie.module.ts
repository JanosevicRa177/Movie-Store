import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieViewComponent } from './movie-view/movie-view.component';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { AppRoutingModule } from '../app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MovieNewEditViewComponent } from './movie-new-edit-view/movie-new-edit-view.component';

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
export class MovieModule {}
