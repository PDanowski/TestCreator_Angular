# TestCreator
Test creator web app (.NET 10 / ASP.NET Core and Angular 21)

Used languages, technologies and frameworks

Backend:

    ASP.NET Core / .NET 10
    EntityFramework Core 10
    SignalR
    Mapster
    NUnit
    Moq
    
Frontend:
    
    Angular 21
    RxJS
    Bootstrap 5 (+ Bootswatch)
    LESS
    Font-Awesome

## Requirements

- .NET SDK 10.x
- Node.js 20.19+ (LTS recommended)
- npm 11+

## Local Build & Test

Backend:

```bash
dotnet restore ./TestCreator/TestCreator.sln
dotnet build ./TestCreator/TestCreator.sln -c Release --no-restore
dotnet test ./TestCreator/TestCreator.sln -c Release --no-build
```

Frontend:

```bash
cd ./TestCreator/TestCreator.WebApp/ClientApp
npm ci
npm run build
npm run test -- --watch=false --browsers=ChromeHeadless
```

## GitHub Actions CI

- Backend pipeline: `.github/workflows/dotnet.yml`
  - Runs restore/build/test on .NET 10 for pushes and PRs to `master`/`main`.
- Frontend pipeline: `.github/workflows/angular.yml`
  - Runs `npm ci`, Angular build, and headless Chrome tests on Node 24 for pushes and PRs to `master`/`main`.

