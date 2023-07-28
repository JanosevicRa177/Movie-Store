import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerViewComponent } from './customer-view/customer-view.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{ path: 'all', component: CustomerViewComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes), CommonModule],
  exports: [RouterModule],
})
export class CustomerRoutingModule {}
