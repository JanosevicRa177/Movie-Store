import { Component, OnInit } from '@angular/core';

export interface Route {
  name: string;
  route: string;
}

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  routes: Route[] = [
    {
      name: 'Home',
      route: '/',
    },
    { name: 'Customers', route: '/customer/all' },
    { name: 'Movies', route: '/movie/all' },
  ];
  constructor() {}

  ngOnInit(): void {}
}
