# zuroWa

**zuro's Web Apps** is my personal portfolio of web applications built with ASP.NET Core MVC.

**Live:** [https://zurowa.azurewebsites.net](https://zurowa.azurewebsites.net) (It may take a while for the site to load first time since the server maybe idle)

---

## What is zuroWa?

zuroWa is my portfolio platform where each section showcases a different web app, all built and maintained by myself. New apps get added over time, so check back!

---

## Apps

### Ponsai - Simple habit tracker with a cute UI
Currently building ... Will add more info later

- Route: /Ponsai
- Status: Building

### EyeMax — Movie Search & Favorites
Search any movie title and get live results powered by the TMDB API including posters, release dates, and overviews. Save movies to your personal favorites list.

- **Route:** `/EyeMax` (search), `/Favorites` (favorites)
- **API:** [The Movie Database (TMDB)](https://www.themoviedb.org/)
- **Status:** ✅ Live

---

### More Coming Soon
New apps are in the works.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| Language | C# |
| Frontend | Razor Views, Bootstrap, custom CSS |
| Database | SQLite (EF Core) |
| Hosting | Azure App Service (Linux, Free tier) |

---

## Project Structure

```
zuroWa/
├── src/
│   ├── zuroWa.Core/              # Domain, Data, and Logic
│   │   ├── Data/                 # AppDbContext, EF Core
│   │   ├── Domain/EyeMax/        # Movie, TmdbMovie entities
│   │   └── Logic/EyeMax/         # EyeMaxTmdbService, EyeMaxFavoriteService
│   └── zuroWa.Web/               # ASP.NET Core MVC presentation tier
│       ├── Controllers/          # EyeMaxController, FavoritesController
│       ├── Views/
│       │   ├── Home/             # Portfolio homepage
│       │   ├── EyeMax/           # Movie search views
│       │   └── Favorites/        # Favorites list view
│       └── appsettings.json
└── docs/                         # Local-only project notes (gitignored)
```

---

## Running Locally

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A free [TMDB API key](https://www.themoviedb.org/settings/api)

### Setup

1. Clone the repo:
   ```bash
   git clone https://github.com/zurozira/zuroWa.git
   cd zuroWa
   ```

2. Add your TMDB API key to `src/zuroWa.Web/appsettings.Development.json`:
   ```json
   {
     "EyeMax": {
       "TmdbApiKey": "your_api_key_here"
     }
   }
   ```

3. Apply database migrations:
   ```bash
   dotnet ef database update \
     --project src/zuroWa.Core \
     --startup-project src/zuroWa.Web
   ```

4. Run:
   ```bash
   dotnet run --project src/zuroWa.Web
   ```

5. Open `https://localhost:5001`

---

## Notes

