# Next Steps

## Current Goal

Wire TMDB search flow into the web layer.

## Current Focus

Create one controller action that uses `EyeMaxTmdbService.SearchMoviesAsync()` and displays `TmdbMovie` results.

## Next Small Step

- Create or update a controller action for movie search.
- Call `EyeMaxTmdbService.SearchMoviesAsync(title)`.
- Return `TmdbMovie` results to a simple view.
- Do not involve `MovieRepository` in search.

## After That

- Add search form to a Razor page or view.
- Implement `EyeMaxFavoriteService` for local favorites.
- Update old controllers to use new service boundaries.

## Known Reminders

- Do not overengineer for future React yet, but keep backend decoupled.
- Keep comments/documentation useful and light.
- Fix nullable warnings gradually, not all at once.
