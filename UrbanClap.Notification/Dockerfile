#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UrbanClap.Notification/UrbanClap.NotificationService.csproj", "UrbanClap.Notification/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
RUN dotnet restore "UrbanClap.Notification/UrbanClap.NotificationService.csproj"
COPY . .
WORKDIR "/src/UrbanClap.Notification"
RUN dotnet build "UrbanClap.NotificationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrbanClap.NotificationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrbanClap.NotificationService.dll"]
