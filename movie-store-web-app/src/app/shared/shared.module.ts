import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
  declarations: [HeaderComponent, FooterComponent],
  imports: [CommonModule, MatToolbarModule, MatButtonModule, AppRoutingModule],
  exports: [HeaderComponent, FooterComponent],
})
export class SharedModule {}
