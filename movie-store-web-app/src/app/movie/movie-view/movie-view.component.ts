import { RoleService } from './../../services/role.service';
import { ToastrService } from 'ngx-toastr';
import { GetMoviesResponse, MovieClient, Role, CustomerClient } from './../../api/api-reference';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-movie-view',
    templateUrl: './movie-view.component.html',
    styleUrls: ['./movie-view.component.css'],
})
export class MovieViewComponent {
    movies: GetMoviesResponse[] = [];
    role: Role = Role.Regular;
    constructor(
        private readonly toastr: ToastrService,
        private readonly movieClient: MovieClient,
        private readonly router: Router,
        private readonly roleService: RoleService,
        private readonly customerClient: CustomerClient
    ) {
        this.roleService.userRole$.subscribe(innerRole => {
            this.role = innerRole
        })
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
    readonly purchaseMovie = (movieId: string) => {
        this.customerClient.purchaseMovie(movieId).subscribe({
            next: () => this.toastr.success('Movie bought!'),
            error: () => this.toastr.error('Failed to buy a movie.'),
        });
    };
    readonly hasRole = (role: Role): boolean => {
        return this.role == role
    };
}
