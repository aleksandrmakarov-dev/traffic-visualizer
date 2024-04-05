FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY ["TukkoTrafficVisualizer.API/TukkoTrafficVisualizer.API.csproj","TukkoTrafficVisualizer.API/"]
COPY ["TukkoTrafficVisualizer.Infrastructure/TukkoTrafficVisualizer.Infrastructure.csproj", "TukkoTrafficVisualizer.Infrastructure/"]
COPY ["TukkoTrafficVisualizer.Database/TukkoTrafficVisualizer.Database.csproj", "TukkoTrafficVisualizer.Database/"]
COPY ["TukkoTrafficVisualizer.Cache/TukkoTrafficVisualizer.Cache.csproj", "TukkoTrafficVisualizer.Cache/"]
COPY ["TukkoTrafficVisualizer.Core/TukkoTrafficVisualizer.Core.csproj", "TukkoTrafficVisualizer.Core/"]

RUN dotnet restore "TukkoTrafficVisualizer.API/TukkoTrafficVisualizer.API.csproj"
COPY . .
WORKDIR "/src/TukkoTrafficVisualizer.API"

RUN dotnet build "TukkoTrafficVisualizer.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "TukkoTrafficVisualizer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "TukkoTrafficVisualizer.API.dll" ]
