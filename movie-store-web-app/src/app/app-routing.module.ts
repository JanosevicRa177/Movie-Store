import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

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
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
