# Traffic Visualizer
An application visualizes traffic conditions in Finland.
## Backend
### Technologies
  - .NET 8.0, ASP.NET Core Web API
  - Redis
  - MongoDb
  - SignalR

### Requirements

  - Redis instance running
  - MongoDb instance running

### Instructions

How to run docker container
  1. Rename `example.env` to `.env`
  2. (Optional) To make `Feedback` send issues to Github/Gitlab set `AccessToken`, `BaseUrl` and `ProjectId` in `appsettings.json`
  3. (Optional) To make `SignUp` send `EmailVerificationToken` to email address and `Username` and `Password` in `appsettings.json`
  4. Open command line and run command `docker compose -f docker-compose.yml up -d --build` in directory where `docker-compose.yml` is located

## Frontent
### Technologies
  - React
  - React Router DOM,
  - React Query
  - Tailwind, ShadCN
  - Leaflet

### Requirements
  - Backend instance running

### Instructions
How to run in `dev` mode

  1. Rename `example.env` to `.env`
  2. Open terminal and run command `npm install`
  2. Run command `npm run dev`
  
How to run docker container

  1. Rename `example.env` to `.env`
  2. Open command line and run command `npm run prod:windows`
