#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AppSyncServer/AppSyncServer.csproj", "AppSyncServer/"]
COPY ["Sync.Utils/Sync.Utils.csproj", "Sync.Utils/"]
COPY ["Sync/Sync.csproj", "Sync/"]
COPY ["Sync.Repository/Sync.Repository.csproj", "Sync.Repository/"]
COPY ["Sync.Core/Sync.Core.csproj", "Sync.Core/"]
RUN dotnet restore "AppSyncServer/AppSyncServer.csproj"
COPY . .
WORKDIR "/src/AppSyncServer"
RUN dotnet build "AppSyncServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppSyncServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sync_server.dll"]