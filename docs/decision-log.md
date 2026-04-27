# Decision Log

## 2026-04-26
**Decision:** Use `zuroWa` as the main solution/app name and `EyeMax` as the movie feature/module.

**Why:** The app will grow into a broader portfolio/personal platform, so the top-level name should not be movie-specific.

**Impact:** Future features can be added under `zuroWa` without renaming the whole solution.

---

## 2026-04-26
**Decision:** Organize the solution using a `src/` folder.

**Why:** This is a common structure for growing .NET projects and keeps source code separate from docs and other repo files.

**Impact:** Projects now live under `src/zuroWa.Core` and `src/zuroWa.Web`.

---

## 2026-04-26
**Decision:** Use project-to-project references instead of DLL references.

**Why:** Both projects live in the same solution/repository, so project references are cleaner and easier to maintain.

**Impact:** `zuroWa.Web` references `zuroWa.Core` directly through `ProjectReference`.

---

## 2026-04-26
**Decision:** Make EyeMax movie search TMDB API-first.

**Why:** Search results come from TMDB, so storing all search data locally would add unnecessary complexity and duplication.

**Impact:** Local DB should only store app-owned data such as favorites.

---

## 2026-04-26
**Decision:** Separate local movie entities from TMDB response models.

**Why:** External API data and local persistence data have different responsibilities and shapes.

**Impact:** `Movie` remains local/app-owned, while `TmdbMovie` represents TMDB data.
