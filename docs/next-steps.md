# Next Steps

## Current Goal
Refactor the EyeMax movie feature from local-database-first into TMDB-API-first.

## Current Focus
Split the old `MovieService` responsibilities into clearer services.

## Next Small Step
Create or refine service boundaries for:
- `EyeMaxTmdbService`
- `EyeMaxFavoriteService`

## After That
- Update controllers to use the new services.
- Build TMDB search flow in the web app.
- Keep favorites as a separate local DB flow.

## Known Reminders
- Do not overengineer for future React yet, but keep backend decoupled.
- Keep comments/documentation useful and light.
- Fix nullable warnings gradually, not all at once.
