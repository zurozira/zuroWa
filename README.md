# zuroWa

**zuro's Web Apps** is my personal portfolio of web applications built with ASP.NET Core MVC.

**Live:** [https://zurowa.azurewebsites.net](https://zurowa.azurewebsites.net) *(B1 Tier and I will get a proper domain soon :)*

***

## What is zuroWa?

zuroWa is my portfolio platform where each section showcases a different web app, all built and maintained by me. New apps get added over time.

***

## Apps

### ZicZacZu: PvP Tic-Tac-Toe
A turn-based Tic-Tac-Toe game to play with a friend, anytime. Create a game, share a code, and take turns on your own schedule (no real-time required so you can come back anytime and pick up where you left off).

- **Route:** `/ZicZacZu`
- **Status:** ✅ Live
- **Tech:** SQLite, EF Core, jQuery
- **Rules:** 5×5 grid, 4-in-a-row wins (No login required for now)

### Ponsai: Habit Tracker
Track daily habits with a visual bonsai tree that grows with your streak. Log a habit each day to watch your tree grow from a seed to a full bonsai. Miss a day and the streak resets.

- **Route:** `/Ponsai`
- **Status:** ✅ Live
- **Tech:** SQLite, EF Core
- **Auth:** Per-user habits — each account has their own habit list (Log in required)

### EyeMax: Movie Search & Favorites
Search any movie title and get live results powered by the TMDB API including posters, release dates, and overviews. Save movies to a shared community favorites list.

- **Route:** `/EyeMax` (search), `/Favorites` (favorites)
- **API:** [The Movie Database (TMDB)](https://www.themoviedb.org/)
- **Status:** ✅ Live
- **Auth:** Login required to save/remove favorites; anyone can browse the list

***

## Member Portal

Register or log in at `/Account/Login` to unlock per-user features.

- **Members** can save EyeMax favorites and track their own Ponsai habits
- **Admin** (me) has full access including future moderation features

***

### More Coming Soon
- Guestbook: leave a message (shared board, login required to post)

***

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| Language | C# |
| Frontend | Razor Views, Bootstrap, custom CSS |
| Database | SQLite (EF Core) |
| Hosting | Azure App Service (Linux, B1 tier) |

***

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
       ├── Controllers/          # AccountController, EyeMaxController,
       │                         # FavoritesController, HomeController,
       │                         # PonsaiController, ZicZacZuController
       ├── Views/
          ├── Home/             # Portfolio homepage
          ├── Account/          # Login, Register views
          ├── EyeMax/           # Movie search views
          ├── Favorites/        # Shared favorites list view
          ├── Ponsai/           # Habit tracker views
          └── ZicZacZu/         # Game lobby + session views
```

***

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
- **Status:** ✅ Live

---
