import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MovieViewComponent } from './movie-view/movie-view.component';

const routes: Routes = [{ path: 'all', component: MovieViewComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes), CommonModule],
  exports: [RouterModule],
})
export class MovieRoutingModule {}
