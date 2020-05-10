FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["IdentitySystem.API.csproj", "."]
RUN dotnet restore "IdentitySystem.API.csproj"
WORKDIR "/src"
COPY . .
RUN dotnet build "IdentitySystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentitySystem.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet IdentitySystem.API.dll