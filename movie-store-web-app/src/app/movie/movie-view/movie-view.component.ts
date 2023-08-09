import { RoleService } from './../../services/role.service';
import { ToastrService } from 'ngx-toastr';
import { MovieClient, Role, CustomerClient, LicensingType, MovieDto } from './../../api/api-reference';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';

@Component({
    selector: 'app-movie-view',
    templateUrl: './movie-view.component.html',
    styleUrls: ['./movie-view.component.css'],
})
export class MovieViewComponent {
    movies: MovieDto[] = [];
    role: Role = Role.Regular;
    length = 50;
    pageSize = 6;
    pageIndex = 0;
    selectedLicensingType: LicensingType | null = null;
    name: string = "";

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
        this.handlePage(0, false)
    }

    readonly handlePage = (pageIndex: number, isSearch: boolean) => {
        if (isSearch)
            pageIndex = 0;
        window.scroll({
            top: 0,
            left: 0,
            behavior: 'smooth'
        });
        this.movieClient.getAll(this.name, this.selectedLicensingType, 6, pageIndex).subscribe((moviesPage) => {
            this.movies = moviesPage.movies;
            this.length = moviesPage.size
        });
        this.pageIndex = pageIndex;
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

    readonly isLicenceLifelong = (license: LicensingType): boolean => {
        return license == LicensingType.Lifelong;
    }
}
