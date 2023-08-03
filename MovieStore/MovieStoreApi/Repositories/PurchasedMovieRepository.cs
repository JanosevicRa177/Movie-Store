﻿using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Model;
using MovieStore.Infrastructure;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Repositories;

public class PurchasedMovieRepository: GenericRepository<PurchasedMovie>
{
    public PurchasedMovieRepository(MovieStoreContext ctx):base(ctx)
    {
        ctx.PurchasedMovies
            .Include(movie => movie.Customer)
            .Include(movie => movie.Movie);
    }
}