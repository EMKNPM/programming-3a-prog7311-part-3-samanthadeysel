FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
# Updated to expose your custom HTTP and HTTPS ports
EXPOSE 5186
EXPOSE 7169

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TechMove_Logistics/TechMove_Logistics.csproj", "TechMove_Logistics/"]
COPY ["TechMoves_WebAPI/TechMoves_WebAPI.csproj", "TechMoves_WebAPI/"]
RUN dotnet restore "./TechMove_Logistics/TechMove_Logistics.csproj"
COPY . .
WORKDIR "/src/TechMove_Logistics"
RUN dotnet build "./TechMove_Logistics.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechMove_Logistics.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechMove_Logistics.dll"]
