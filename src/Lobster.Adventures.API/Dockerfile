FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet restore 

WORKDIR "/src/src/Lobster.Adventures.API"
RUN dotnet build "Lobster.Adventures.API.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "Lobster.Adventures.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lobster.Adventures.API.dll"]
