# Handoff Prompt

I want to build this project myself and learn it properly. Please act like a coach/reviewer, not just a code generator. For most steps, give me:
- the goal,
- the files to edit,
- acceptance criteria,
- and a few small hints first.

Let me attempt the code before you show a full solution. If I paste my code, review it carefully, explain the reasoning behind fixes, and only give full code if I am truly stuck after trying.

We are continuing an existing .NET project. Use the following as the current source of truth unless I correct it.

## Project Idea
I am building a .NET 10 project called `zuroWa`.

`zuroWa` is my main personal web app and portfolio website. It is not just for one movie feature. Over time, I want to scale it and add more functionality, more apps/tools, and possibly more frameworks. That future scaling is only something to keep in mind for architecture decisions right now — it is not the immediate focus.

Inside `zuroWa`, I am currently building one feature/module called `EyeMax`, which is a movie search engine powered by the TMDB API.

## Current Status
TMDB search is already working through `EyeMaxController` and `EyeMaxTmdbService`. The next step is polishing the result page and planning favorites.

## Main Goal Right Now
My current goal is to build the `EyeMax` feature properly:
- Search movies using the TMDB API
- Show movie data/details in my own frontend
- Keep the architecture scalable so I can later grow `zuroWa` into a larger portfolio/web app
- Learn the architecture and code properly, not just finish the project quickly

## Current Architecture
I have a solution called:
- `zuroWa.sln`

It contains two projects:
- `zuroWa.Core` → backend/core project for Domain, Data, and Logic
- `zuroWa.Web` → ASP.NET Core MVC / Razor presentation tier

Current folder direction:
- `src/zuroWa.Core`
- `src/zuroWa.Web`
- `docs/architecture-notes.md`

We already renamed and reorganized the solution into this cleaner structure.

## Important Design Decisions Already Made
1. `zuroWa` is the main app/portfolio.
2. `EyeMax` is only one feature/module inside it.
3. I want to keep the backend decoupled enough so I can move to React later without throwing away the backend.
4. The movie search feature should be TMDB API-first, not database-first.
5. My own database should not be used for storing all searched movies.
6. My own database should only store app-owned data, especially my favorite movies.
7. I want a dedicated page later that shows the list of movies I like/favorited.
8. This project should stay organized and scalable because later I may add more sections to `zuroWa`, not only movie-related ones.

## Documentation / Notes
I created notes files to help me:
- remember design decisions,
- keep track of technical debt,
- scale later,
- and fix bugs more easily.

Docs direction:
- `docs/architecture-notes.md`
- `docs/decision-log.md`
- `docs/next-steps.md`
- `docs/handoff-prompt.md`

## Current Refactor Direction
Originally, my movie code was more tied to my local SQL-based structure.

Now I am refactoring it so that:
- TMDB handles movie search and movie detail data
- My local DB is used only for favorites / app-owned movie data
- API concerns and persistence concerns are separated

We already decided that my current `MovieService` is mixing two responsibilities:
- local SQL persistence (`MovieRepository`)
- TMDB/external API logic (`ImageService` / movie lookup)

We want to split that into two clearer services, for example:
- `EyeMaxTmdbService` → responsible for TMDB search/fetch/details
- `EyeMaxFavoriteService` → responsible for local favorite movie storage

## Model Strategy
We already decided not to use one single `Movie` model for everything.

We want to separate:
- local database/app-owned movie entity
- external TMDB response model

I already created a separate TMDB model:
- `TmdbMovie` in `zuroWa.Core.Domain.EyeMax`

Rule to keep:
- `Movie` = local database entity / app-owned data
- `TmdbMovie` = external TMDB result model

## Existing Code Situation
The old code had:
- a `Movie` domain model shaped around local database fields
- a `MovieService` that mixed DB repository operations with TMDB/image enrichment logic

That mixed design is what we are now refactoring away from.

## Nullable / Cleanup Notes
The solution currently builds successfully, but there are nullable warnings (`CS8618`, `CS8602`) that I plan to fix gradually.

For now, the build works, and I do not want to get stuck polishing everything at once before continuing the architecture refactor.

## How I Want Help
Please help me in this style:
- Start by summarizing your understanding briefly
- Then give me the next smallest useful step
- Prefer coaching over code dumping
- Ask me to implement things myself first
- Review my code carefully when I paste it
- Point out architecture, naming, nullability, layering, and maintainability issues
- Keep future React compatibility in mind, but do not force React-specific work now
- Keep future scaling of `zuroWa` in mind, but do not overengineer now

## What To Avoid
Please avoid:
- rewriting the whole app at once
- giving giant solutions without me trying first
- pushing me into unnecessary complexity too early
- using the database for TMDB search results unless there is a very specific app-owned reason
- collapsing TMDB models and local DB models into one class again

## What I Need From You First
Please continue from here and help me refactor the current movie flow step by step.

I want the next step to be small and practical:
- identify the next file(s) I should create or change,
- explain why,
- give me acceptance criteria,
- and let me implement it first.
