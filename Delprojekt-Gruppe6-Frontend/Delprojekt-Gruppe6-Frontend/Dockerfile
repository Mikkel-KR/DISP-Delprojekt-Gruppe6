#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Delprojekt-Gruppe6-Frontend/Delprojekt-Gruppe6-Frontend.csproj", "Delprojekt-Gruppe6-Frontend/"]
RUN dotnet restore "Delprojekt-Gruppe6-Frontend/Delprojekt-Gruppe6-Frontend.csproj"
COPY . .
WORKDIR "/src/Delprojekt-Gruppe6-Frontend"
RUN dotnet build "Delprojekt-Gruppe6-Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Delprojekt-Gruppe6-Frontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delprojekt-Gruppe6-Frontend.dll"]