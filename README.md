# Cloud App — laboratorium 5

Aplikacja z listą zadań: backend REST (ASP.NET Core 8, EF Core, PostgreSQL), frontend (React, Vite, TypeScript).

**Repozytorium:** [github.com/Pok1s/Cloud-App-zadania-lab-5-](https://github.com/Pok1s/Cloud-App-zadania-lab-5-)

## Wymagania

- [.NET SDK 8](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (LTS)
- [Docker](https://www.docker.com/) (Compose)

## Uruchomienie z Dockerem

```bash
docker compose up -d --build
```

- API (Swagger): `http://localhost:5000/swagger`
- Baza PostgreSQL: port `5432`, dane w named volume `postgres_data` — przetrwają `docker compose down` (bez flagi `-v`).

## Uruchomienie lokalne (API + UI)

1. Baza: `docker compose up -d db`
2. Backend: `cd backend/CloudApp.Api && dotnet run` (domyślnie `http://localhost:5030`)
3. Frontend: `cd frontend && npm install && npm run dev` (`http://localhost:5173`)

Zmienna `VITE_API_URL` w `frontend/.env` wskazuje adres API (np. `http://localhost:5030` lub `http://localhost:5000` przy API z Dockera).

## API (przykłady)

- `GET /api/Tasks` — lista zadań (DTO)
- `GET /api/Tasks/{id}` — pojedyncze zadanie
- `POST /api/Tasks` — body: `{ "title": "...", "description": "..." }`

Plik `backend/CloudApp.Api/CloudApp.Api.http` zawiera żądania do testów w IDE.

## Struktura

- `backend/CloudApp.Api` — Web API, migracje EF w `Migrations/`
- `frontend` — aplikacja React
- `docker-compose.yml` — Postgres + API

## Klonowanie

```bash
git clone https://github.com/Pok1s/Cloud-App-zadania-lab-5-.git
cd Cloud-App-zadania-lab-5-
```
