import { GetMoviesResponse, MovieClient } from './../../api/api-reference';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-movie-view',
  templateUrl: './movie-view.component.html',
  styleUrls: ['./movie-view.component.css'],
})
export class MovieViewComponent {
  movies: GetMoviesResponse[] = [];
  constructor(movieClient: MovieClient) {
    movieClient.getAll().subscribe((movies) => (this.movies = movies));
  }
}
