#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UrbanClap.ServiceProvider/UrbanClap.ServiceProvider.csproj", "UrbanClap.ServiceProvider/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
RUN dotnet restore "UrbanClap.ServiceProvider/UrbanClap.ServiceProvider.csproj"
COPY . .
WORKDIR "/src/UrbanClap.ServiceProvider"
RUN dotnet build "UrbanClap.ServiceProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrbanClap.ServiceProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrbanClap.ServiceProvider.dll"]
