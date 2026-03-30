# Cloud App — laboratorium 5

Aplikacja z listą zadań: backend REST (ASP.NET Core 8, EF Core, PostgreSQL), frontend (React, Vite, TypeScript).

**Repozytorium:** [github.com/Pok1s/Cloud-App-zadania-lab-5-](https://github.com/Pok1s/Cloud-App-zadania-lab-5-)

**README na GitHubie (do artefaktu 5.5):**  
https://github.com/Pok1s/Cloud-App-zadania-lab-5-/blob/main/README.md

## Wymagania

- [.NET SDK 8](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (LTS)
- [Docker](https://www.docker.com/) (Compose)

## Uruchomienie z Dockerem

```bash
docker compose up -d --build
```

- API (Swagger): `http://localhost:5000/swagger`
- PostgreSQL: port `5432`
- Dane bazy są mapowane na katalog **`pgdata/`** w folderze projektu (bind mount). Po **`docker compose down -v`** dane **nie znikają** — folder `pgdata/` zostaje na dysku (w przeciwieństwie do named volume, który `-v` usuwa).

## Uruchomienie lokalne (API + UI)

1. Baza: `docker compose up -d db`
2. Backend: `cd backend/CloudApp.Api && dotnet run` (domyślnie `http://localhost:5030`)
3. Frontend: `cd frontend && npm install && npm run dev` (`http://localhost:5173`)

Przy API z Dockera ustaw w `frontend/.env`: `VITE_API_URL=http://localhost:5000`.

## API (przykłady)

- `GET /api/Tasks` — lista zadań (DTO)
- `GET /api/Tasks/{id}` — pojedyncze zadanie
- `POST /api/Tasks` — body: `{ "title": "...", "description": "..." }`

Odpowiedzi wyłącznie jako **TaskReadDto** (bez encji EF). Test zapisu/odczytu: Swagger, plik `backend/CloudApp.Api/CloudApp.Api.http` lub `curl`.

## Migracje EF

Katalog `backend/CloudApp.Api/Migrations/` — historia zmian schematu (`dotnet ef migrations add ...`).

## Struktura

- `backend/CloudApp.Api` — Web API, `Controllers/TasksController.cs`, `Migrations/`
- `frontend` — React (dodawanie zadań bez Swaggera)
- `docker-compose.yml` — Postgres + API

## Klonowanie

```bash
git clone https://github.com/Pok1s/Cloud-App-zadania-lab-5-.git
cd Cloud-App-zadania-lab-5-
```

## Checklista — Artefakt 05 (PDF)

| Punkt | Co zrobić / gdzie |
|--------|-------------------|
| **5.1** | `GetAll` / `GetById` mapują na `TaskReadDto`; kod w `backend/CloudApp.Api/Controllers/TasksController.cs`. Screeny: build + test POST/GET (Swagger lub `.http`). |
| **5.2** | Trwałość: dane w `pgdata/`; scenariusz z PDF: `docker compose down -v`, potem `docker compose up -d --build`, UI nadal widzi dane. Screeny: przed/po. |
| **5.3** | Folder `Migrations/` w backendzie. |
| **5.4** | Dodanie zadania tylko przez React (`npm run dev`), bez Swaggera. Screen formularza + lista. |
| **5.5** | `git push` + screen README na GitHubie (link wyżej). |
