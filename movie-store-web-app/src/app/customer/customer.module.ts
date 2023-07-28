import { MatTableModule } from '@angular/material/table';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerViewComponent } from './customer-view/customer-view.component';

@NgModule({
  declarations: [CustomerViewComponent],
  imports: [CommonModule, MatTableModule],
})
export class CustomerModule {}
