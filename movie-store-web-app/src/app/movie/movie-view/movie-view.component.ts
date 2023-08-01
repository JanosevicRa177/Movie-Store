import { ToastrService } from 'ngx-toastr';
import { GetMoviesResponse, MovieClient } from './../../api/api-reference';
import { Component, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import {  Router } from '@angular/router';

@Component({
  selector: 'app-movie-view',
  templateUrl: './movie-view.component.html',
  styleUrls: ['./movie-view.component.css'],
})
export class MovieViewComponent {
  movies: GetMoviesResponse[] = [];
  constructor(
    private readonly toastr: ToastrService,
    private readonly movieClient: MovieClient,
    private readonly router: Router
  ) {
    movieClient.getAll().subscribe((movies) => (this.movies = movies));
  }

  readonly delete = (id: string) => {
    this.movieClient.delete(id).subscribe({
      next: () => {
        this.movies = this.movies.filter((movie) => movie.id !== id);
        this.toastr.success('Successfuly deleted movie');
      },
      error: (e: any) => this.toastr.error('Failed to delete movie'),
    });
  };
  readonly openNew = () => {
    this.router.navigate(['/movie/create']);
  };
  readonly openEdit = (id: string) => {
    this.router.navigate([`/movie/edit/${id}`]);
  };
}
