import { CreateMovieDto, MovieClient, MovieDto } from '../../api/api-reference';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LicensingType } from 'src/app/api/api-reference';
import { convertStringToCamelCase } from 'src/app/util/string.converter';
import { toastError } from 'src/app/util/toastr';

@Component({
    selector: 'app-movie-new-edit-view',
    templateUrl: './movie-new-edit-view.component.html',
    styleUrls: ['./movie-new-edit-view.component.css'],
})
export class MovieNewEditViewComponent {
    movieForm = new FormGroup({
        name: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        licensingType: new FormControl<LicensingType>(LicensingType.Lifelong, { nonNullable: true }),
    });
    movieId = '';

    constructor(
        private readonly movieClient: MovieClient,
        private readonly toastr: ToastrService,
        private readonly route: ActivatedRoute,
        private readonly router: Router) {
        this.route.params.subscribe(params => {
            this.movieId = params['id'];
            if (this.movieId) {
                this.movieClient.getById(this.movieId).subscribe((movie) => {
                    this.movieForm.patchValue({ name: movie.name, licensingType: movie.licensingType });
                })
            }
        });
    }

    handleSubmit() {
        this.movieId ? this.updateMovie() : this.createMovie();
    }

    readonly createMovie = () => {
        this.movieClient.add(this.getMovieDto()).subscribe({
            next: () => {
                this.toastr.success('Successfuly created movie!');
                this.router.navigate(['/movie/all']);
            },
            error: (error: any) => {
                this.setErrors(error);
                toastError(this.toastr, error)
            },
        });
    };

    readonly updateMovie = () => {
        this.movieClient.update(this.movieId, this.getMovieDto()).subscribe({
            next: () => {
                this.toastr.success('Successfuly updated movie!');
                this.router.navigate(['/movie/all']);
            },
            error: (error: any) => {
                this.setErrors(error);
                toastError(this.toastr, error)
            },
        });
    };

    private readonly getMovieDto = (): CreateMovieDto => {
        return new CreateMovieDto({
            name: this.movieForm.value.name ?? "",
            licensingType: Number(this.movieForm.value.licensingType),
        });
    }

    private readonly setErrors = (error: any) => {
        var movieForm = this.movieForm;
        Object.keys(error.errors).forEach(function (key) {
            var errorString = ""
            var errors: string[] = [];
            error.errors[key].forEach((error: string | undefined) => {
                errors.push(error ?? "")
            });
            errorString = errors.join(" | ")
            movieForm.get(convertStringToCamelCase(key))?.setErrors({ custom: errorString });
        });
    }
}
