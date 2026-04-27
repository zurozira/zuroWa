# Architecture Notes

## Project Overview
- `zuroWa` is the main personal web app and portfolio.
- `EyeMax` is a movie-search feature/module inside `zuroWa`.
- The architecture should stay scalable so more features, apps, and frameworks can be added later.

## Solution Structure
- `zuroWa.sln`
- `src/zuroWa.Core` = backend/core logic, domain models, repositories, services
- `src/zuroWa.Web` = ASP.NET Core MVC / Razor presentation tier
- `docs/` = project notes, handoff context, decisions, and next steps

## Current Architecture Rules
- Keep the backend decoupled enough to support Razor now and React later.
- TMDB movie search is API-first.
- Do not store all TMDB search results in the local database.
- Use the local database only for app-owned data such as favorites.
- Avoid mixing external API response models with local persistence entities.

## EyeMax Modeling Rules
- `Movie` = local database/app-owned entity.
- `TmdbMovie` = external TMDB API result model.
- Do not collapse TMDB and local DB models into one class unless there is a deliberate app-owned save flow.

## Service Boundaries
- `EyeMaxTmdbService` should handle TMDB search/details.
- `EyeMaxFavoriteService` should handle local favorite movie persistence.
- Repositories should stay focused on persistence.
- External API calls should be isolated in dedicated services.

## Current Technical Debt
- Refactor old `MovieService` so it no longer mixes DB and TMDB responsibilities.
- Remove DB dependency from movie search flow.
- Keep DB only for favorites or other app-owned data.
- Fix nullable warnings (`CS8618`, `CS8602`) gradually.
- Improve namespaces so they match the renamed solution/project structure.

## Commenting / Documentation Rule
- Add comments for intent, not obvious syntax.
- Keep notes for architectural decisions and future reminders.
- Prefer small, useful comments over heavy documentation blocks.
