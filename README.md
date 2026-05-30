# zuroWa

**zuro's Web Apps** is my personal portfolio of web applications built with ASP.NET Core MVC.

**Live:** [https://zurowa.azurewebsites.net](https://zurowa.azurewebsites.net)

---

## What is zuroWa?

zuroWa is my portfolio platform where each section showcases a different web app, all built and maintained by myself. New apps get added over time, so check back!

---

## Apps

### Movie Search
Search any movie title and get live results powered by the TMDB API - including posters, release dates, and overviews.

- **Route:** `/Search`
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
| Database | SQLite |
| Hosting | Azure App Service (Linux, Free tier) |

---

## Project Structure

```
zuroWa/
├── zuroWa.Core/     # Shared models and services
└── zuroWa.Web/      # ASP.NET Core MVC app
    ├── Controllers/
    ├── Views/
    │   ├── Home/    # Portfolio homepage
    │   └── Search/  # Movie Search app
    └── appsettings.json
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

3. Run:
   ```bash
   cd src/zuroWa.Web
   dotnet run
   ```

4. Open `https://localhost:5001`
