# Next Steps

## Current Goal
Polish the EyeMax TMDB search experience.

## Current Focus
Improve the search results page and decide the next feature boundary.

## Next Small Step
- Clean up `SearchResult.cshtml`.
- Show a friendly empty state when no results are returned.
- Decide whether the next step is movie details or favorites.
- Keep the local DB separate from TMDB search results.

## After That
- Implement `EyeMaxFavoriteService` for local favorites.
- Add a favorites page or flow.
- Update old controllers only if needed for the new architecture.

## Known Reminders
- Do not overengineer for future React yet, but keep backend decoupled.
- Keep comments/documentation useful and light.
- Fix nullable warnings gradually, not all at once.
