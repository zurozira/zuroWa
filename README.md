# zuroWa

zuro's Web Apps is my personal dumping ground for ASP.NET Core projects :D
**Live:** [zurowa.cong-vu.com](https://zurowa.cong-vu.com)

## What's this about

Instead of scattering a bunch of separate little repos and demo links everywhere, I built zuroWa as one platform that hosts all my smaller web apps under one roof. Each app below runs live at its own route. I add new ones whenever I finish something worth showing off.

## The apps

### ZicZacZu ~

`PvP Tic-Tac-Toe, but async`

Turn-based Tic-Tac-Toe you can play with a friend on your own schedule. No need to both be online at the same time: create a game, share the code, take your turn whenever, come back later and it's still waiting for you.

- **Tech:** SQLite, EF Core
- **Rules:** 5×5 grid, get 4 in a row to win. No login needed (yet)

### Ponsai ~

`habit tracker with a bonsai gimmick`

Log a habit each day and watch a little bonsai tree grow with your streak. Miss a day, streak resets, tree stops growing. Simple mechanic, but it's weirdly motivating watching a tree instead of just a number.

- **Tech:** SQLite, EF Core
- **Auth:** login required — everyone's habits and streaks are private to their own account

### EyeMax ~

`movie search + shared favorites`

Search any movie and pull live results straight from TMDB: posters, release dates, overviews, all of it. Anyone can browse the community favorites list, but you need an account to actually save or remove movies from it.

- **API:** [TMDB](https://www.themoviedb.org/)
- **Auth:** browsing is open to everyone, saving/removing requires login

## Accounts

Register or log in if you want the personalized stuff ~ saving EyeMax favorites, tracking your own Ponsai habits. I'm the admin, which for now just means I have full access and eventually some moderation tools once there's anything to moderate.

## What's coming next

- A card game with complex logic... More detail later!

## Tech stack

| Layer     | Technology                         |
| --------- | ---------------------------------- |
| Framework | ASP.NET Core MVC (.NET 10)         |
| Language  | C#                                 |
| Frontend  | Razor Views, Bootstrap, custom CSS |
| Database  | SQLite (EF Core)                   |
| Hosting   | Azure App Service (Linux, B1 tier) |

## Project structure

```
zuroWa/
├── src/
   ├── zuroWa.Core/              # domain, data, business logic
   │   ├── Data/                 # AppDbContext, EF Core migrations
   │   ├── Domain/
   │   │   ├── EyeMax/           # Movie, TmdbMovie entities
   │   │   ├── Ponsai/           # Habit, HabitLog entities
   │   │   └── ZicZacZu/         # Game entity
   │   └── Logic/
   │       ├── EyeMax/           # EyeMaxTmdbService, EyeMaxFavoriteService
   │       ├── Ponsai/           # PonsaiService, HabitsWithStreak
   │       └── ZicZacZu/         # ZicZacZuService
   └── zuroWa.Web/               # the actual MVC app
       ├── Controllers/          # AccountController, EyeMaxController,
       │                         # FavoritesController, HomeController,
       │                         # PonsaiController, ZicZacZuController
       ├── Views/
          ├── Home/              # portfolio homepage
          ├── Account/           # login, register
          ├── EyeMax/            # search views
          ├── Favorites/         # shared favorites list
          ├── Ponsai/            # habit tracker views
          └── ZicZacZu/          # game lobby + session views
```

## Running it locally

You'll need the [.NET 10 SDK](https://dotnet.microsoft.com/download) and a free [TMDB API key](https://www.themoviedb.org/settings/api) if you want EyeMax to actually pull movie data.

Clone it:

```bash
git clone https://github.com/zurozira/zuroWa.git
cd zuroWa
```

Drop your TMDB key into `src/zuroWa.Web/appsettings.Development.json`:

```json
{
    "EyeMax": {
        "TmdbApiKey": "your_api_key_here"
    }
}
```

Apply migrations:

```bash
dotnet ef database update \
  --project src/zuroWa.Core \
  --startup-project src/zuroWa.Web
```

Run it:

```bash
dotnet run --project src/zuroWa.Web
```

Then open `https://localhost:5001` and poke around.
