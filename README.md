# Traffic Visualizer
An application visualizes traffic conditions in Finland.

Video:  
[![Example](https://img.youtube.com/vi/VR0w8zUlHQQ/0.jpg)](https://www.youtube.com/watch?v=l5toTQrJ1rY&list=PLwfyMEDWsaKNkNC93vcW9WIPF-7pGf-Z1)  

[Docs](https://core-it-ff-2024-t03-b1ad05b9c7830bfeafccb9e427f246fee9ef7cef83d.pages.labranet.jamk.fi/)
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
  3. To make `Feedback` send issues to Github/Gitlab set `AccessToken`, `BaseUrl` and `ProjectId` in `appsettings.json`
  4. To make `SignUp` send `EmailVerificationToken` to email address and `Username` and `Password` in `appsettings.json`
  5. Open command line and run command `docker compose -f docker-compose.yml up -d --build` in directory where `docker-compose.yml` is located

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

## Features
- [x] Display real-time data on the map
- [x] Station details 
- [x] Compare different LAM stations side by side
- [x] Securely authenticate user accounts
- [x] Save favorite LAM stations to user account
- [x] Search location by name
