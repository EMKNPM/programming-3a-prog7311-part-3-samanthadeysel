FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
# Updated to expose your custom HTTP and HTTPS ports
EXPOSE 5297
EXPOSE 7292

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TechMoves_WebAPI/TechMoves_WebAPI.csproj", "TechMoves_WebAPI/"]
RUN dotnet restore "./TechMoves_WebAPI/TechMoves_WebAPI.csproj"
COPY . .
WORKDIR "/src/TechMoves_WebAPI"
RUN dotnet build "./TechMoves_WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechMoves_WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechMoves_WebAPI.dll"]
