import { Component } from '@angular/core';
import { Route } from 'src/model/utils';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  routes: Route[] = [
    {
      name: 'Home',
      route: '/',
    },
    { name: 'Customers', route: '/customer/all' },
    { name: 'Movies', route: '/movie/all' },
  ];
}
