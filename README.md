# zuroWa

**zuro's Web Apps** is my personal portfolio of web applications built with ASP.NET Core MVC.

**Live:** [https://zurowa.azurewebsites.net](https://zurowa.azurewebsites.net) *(Free tier — may take a moment to wake up)*

---

## What is zuroWa?

zuroWa is my portfolio platform where each section showcases a different web app, all built and maintained by me. New apps get added over time.

---

## Apps

### ZicZacZu: Async PvP Tic-Tac-Toe
A turn-based Tic-Tac-Toe game to play with a friend, anytime. Create a game, share a code, and take turns on your own schedule  (no real-time required).

- **Route:** `/ZicZacZu`
- **Status:** ✅ Live 
- **Tech:** SQLite, EF Core, jQuery
- **Rules:** 5×5 grid, 4-in-a-row wins

### Ponsai: Habit Tracker
Track daily habits with a visual bonsai tree that grows with your streak. Log a habit each day to watch your tree grow from a seed to a full bonsai. Miss a day and the streak resets.

- **Route:** `/Ponsai`
- **Status:** ✅ Live
- **Tech:** SQLite, EF Core

### EyeMax: Movie Search & Favorites
Search any movie title and get live results powered by the TMDB API including posters, release dates, and overviews. Save movies to your personal favorites list.

- **Route:** `/EyeMax` (search), `/Favorites` (favorites)
- **API:** [The Movie Database (TMDB)](https://www.themoviedb.org/)
- **Status:** ✅ Live

---

### More Coming Soon
- Member portal with authentication and roles
- Per-user Favorites and Ponsai data
- Guestbook — leave a message

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| Language | C# |
| Frontend | Razor Views, Bootstrap, custom CSS |
| Database | SQLite (EF Core) |
| Hosting | Azure App Service (Linux, Free F1 tier) |

---

## Project Structure

```
zuroWa/
├── src/
   ├── zuroWa.Core/              # Domain, Data, and Logic
   │   ├── Data/                 # AppDbContext, EF Core migrations
   │   ├── Domain/
   │   │   ├── EyeMax/           # Movie, TmdbMovie entities
   │   │   ├── Ponsai/           # Habit, HabitLog entities
   │   │   └── ZicZacZu/         # Game entity
   │   └── Logic/
   │       ├── EyeMax/           # EyeMaxTmdbService, EyeMaxFavoriteService
   │       ├── Ponsai/           # PonsaiService, HabitsWithStreak
   │       └── ZicZacZu/         # ZicZacZuService
   └── zuroWa.Web/               # ASP.NET Core MVC presentation tier
       ├── Controllers/          # EyeMaxController, FavoritesController,
       │                         # HomeController, PonsaiController,
       │                         # ZicZacZuController
       ├── Views/
          ├── Home/             # Portfolio homepage
          ├── EyeMax/           # Movie search views
          ├── Favorites/        # Favorites list view
          ├── Ponsai/           # Habit tracker views
          └── ZicZacZu/         # Game lobby + session views
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
