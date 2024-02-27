FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskManager.BackgroundJob/TaskManager.BackgroundJob.csproj", "TaskManager.BackgroundJob/"]
RUN dotnet restore "TaskManager.BackgroundJob/TaskManager.BackgroundJob.csproj"
COPY . .
WORKDIR "/src/TaskManager.BackgroundJob"
RUN dotnet build "TaskManager.BackgroundJob.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskManager.BackgroundJob.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.BackgroundJob.dll"]
